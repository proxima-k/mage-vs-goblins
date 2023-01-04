using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : Enemy {
    private enum State {
        Spawn,
        Chase,
        Charge,
        Death
    }
    private State _state = State.Spawn;
    private BotMovement _botMovement;
    private Rigidbody2D _rb2D;

    [SerializeField] private int _chargeDamage = 8;
    [SerializeField] private LayerMask _damageLayers;
    [SerializeField] private Vector2 _boxColliderSize = Vector2.one;
    
    [SerializeField] private float _chargeCooldown = 8f;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private float _chargeSpeed = 8f;
    [SerializeField] private float _pauseBeforeChargeDuration = 1f;
    [SerializeField] private float _chargeDuration = 1f;
    
    [Range(0f,1f)]
    [SerializeField] private float _steerMultiplier = 1f;

    private float _chargeTimer;
    private bool _hasHitPlayer = true;

    protected override void Awake() {
        base.Awake();
        _botMovement = GetComponent<BotMovement>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        switch (_state) {
            case State.Spawn:
                _state = State.Chase;
                break;
            case State.Chase:
                _botMovement.MoveTowards(_targetTf.position);
                // if within range, set state to charge
                if (_chargeTimer <= 0) {
                    if ((_targetTf.position - transform.position).sqrMagnitude <= _attackDistance * _attackDistance) {
                        _state = State.Charge;
                    }
                }
                break;
            case State.Charge:
                // perhaps merge the coroutine to the distance checking and let this state be empty or something.
                if (_chargeTimer <= 0) {
                    StartCoroutine(Charge());
                    _chargeTimer = _chargeCooldown;
                }
                break;
            case State.Death:
                break;
        }

        ChargingCollisionCheck();
        
        if (_state != State.Charge && _chargeTimer > 0)
            _chargeTimer -= Time.deltaTime;
    }

    private IEnumerator Charge() {
        // release smoke or blink to indicate about to charge
        // wait for a short period
        _botMovement.Stop();
        // Transform damageMarkPopupTf = DamageMarkPopup.Create(transform.position).transform;
        // damageMarkPopupTf.SetParent(transform);
        DangerMarkPopup.Create(transform.position);
        
        yield return new WaitForSeconds(_pauseBeforeChargeDuration);

        float initialSteer = _botMovement._maxSteeringForce;
        _botMovement._maxSteeringForce *= _steerMultiplier;
        float initialSpeed = _botMovement._maxMoveSpeed;
        _botMovement._maxMoveSpeed = _chargeSpeed;
        _botMovement.CanSlow(false);
        _botMovement.CanAvoid(false);

        _hasHitPlayer = false;
        
        float timer = _chargeDuration;
        CinemachineShake.Instance.ScreenShake(1f, _chargeDuration);
        while (timer > 0) {
            _botMovement.MoveTowards(_targetTf.position);

            if (_botMovement.TargetIsBehind()) {
                _botMovement._maxSteeringForce = 0.05f;
                break;
            }
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
        }
        
        _botMovement.MoveTowards(transform.position + (Vector3)_rb2D.velocity.normalized * _botMovement._slowRadius * 2);
        _botMovement.CanSlow(true);
        yield return new WaitForSeconds(2f); // charger slowing down to rest

        _botMovement._maxMoveSpeed = initialSpeed;
        _botMovement._maxSteeringForce = initialSteer;
        _botMovement.CanAvoid(true);
        _state = State.Chase;
    }
    
    private void ChargingCollisionCheck() {
        if (_state != State.Charge || _hasHitPlayer) return;
        Collider2D col = Physics2D.OverlapBox(transform.position, _boxColliderSize, 0, _damageLayers);
        if (col != null && col.TryGetComponent(out IDamageable healthDamageable)) {
            Debug.Log($"Collide with {col.name}");
            healthDamageable.Damage(_chargeDamage);
            _hasHitPlayer = true;
        }
    }
    

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
        // Gizmos.DrawLine(transform.position, transform.position+(Vector3)_rb2D.velocity);
    }
}
