using System.Collections;
using UnityEngine;

public class Charger : Enemy {
    private enum State {
        Spawn,
        Chase,
        Charge,
        Death
    }
    private State _state = State.Spawn;
    private BotLocomotion _botLocomotion;
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
        _botLocomotion = GetComponent<BotLocomotion>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        switch (_state) {
            case State.Spawn:
                _state = State.Chase;
                break;
            case State.Chase:
                _botLocomotion.MoveTowards(_targetTf.position);
                
                // if within range, attack
                if (_chargeTimer <= 0) {
                    if ((_targetTf.position - transform.position).sqrMagnitude <= _attackDistance * _attackDistance) {
                        _state = State.Charge;
                    }
                }
                break;
            case State.Charge:
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
        _botLocomotion.Stop();
        DangerMarkPopup.Create(transform.position);
        
        yield return new WaitForSeconds(_pauseBeforeChargeDuration);

        float initialSteer = _botLocomotion._maxSteeringForce;
        _botLocomotion._maxSteeringForce *= _steerMultiplier;
        float initialSpeed = _botLocomotion._maxMoveSpeed;
        _botLocomotion._maxMoveSpeed = _chargeSpeed;
        _botLocomotion.CanSlow(false);
        _botLocomotion.CanAvoid(false);

        _hasHitPlayer = false;
        
        float timer = _chargeDuration;
        CinemachineShake.Instance.ScreenShake(1f, _chargeDuration);
        
        // actual charging part
        while (timer > 0) {
            _botLocomotion.MoveTowards(_targetTf.position);

            if (_botLocomotion.TargetIsBehind()) {
                _botLocomotion._maxSteeringForce = 0.05f;
                break;
            }
            yield return new WaitForFixedUpdate();
            timer -= Time.fixedDeltaTime;
        }
        
        // slows down after passing player or a duration
        _botLocomotion.MoveTowards(transform.position + (Vector3)_rb2D.velocity.normalized * _botLocomotion._slowRadius * 2);
        _botLocomotion.CanSlow(true);
        yield return new WaitForSeconds(2f); // charger slowing down to rest

        _botLocomotion._maxMoveSpeed = initialSpeed;
        _botLocomotion._maxSteeringForce = initialSteer;
        _botLocomotion.CanAvoid(true);
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
    
#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
        // Gizmos.DrawLine(transform.position, transform.position+(Vector3)_rb2D.velocity);
    }
#endif
}
