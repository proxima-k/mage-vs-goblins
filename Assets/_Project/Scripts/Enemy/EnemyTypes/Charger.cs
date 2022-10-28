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

    [SerializeField] private float _chargeCooldown = 8f;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private float _chargeSpeed = 8f;
    [SerializeField] private float _pauseBeforeChargeDuration = 1f;
    [SerializeField] private float _chargeDuration = 1f;
    
    [Range(0f,1f)]
    [SerializeField] private float _steerMultiplier = 1f;

    private float _chargeTimer;
    // todo: change cooldown check to a coroutine check

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
                
                // todo: during cooldown, maybe lower the movement speed to allow player to breath
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

        if (_state != State.Charge && _chargeTimer > 0)
            _chargeTimer -= Time.deltaTime;
    }

    private IEnumerator Charge() {
        // release smoke or blink to indicate about to charge
        // wait for a short period
        _botMovement.Stop();
        yield return new WaitForSeconds(_pauseBeforeChargeDuration);

        float initialSteer = _botMovement._maxSteeringForce;
        _botMovement._maxSteeringForce *= _steerMultiplier;
        float initialSpeed = _botMovement._maxMoveSpeed;
        _botMovement._maxMoveSpeed = _chargeSpeed;
        _botMovement.CanSlow(false);
        _botMovement.CanAvoid(false);
        
        float timer = _chargeDuration;
        CinemachineShake.Instance.ScreenShake(1f);
        while (timer > 0) {
            _botMovement.MoveTowards(_targetTf.position);

            if (_botMovement.TargetIsBehind()) {
                _botMovement._maxSteeringForce = 0.05f;
                break;
            }
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
        }

        // in the meantime, maybe shake the camera or something to show that the mob is charging
        
        // todo: do some math on reducing speed and stuff
        // while (_botMovement._maxMoveSpeed > initialSpeed) {
        //     _botMovement._maxMoveSpeed -= Time.deltaTime * 5f;
        //     yield return new WaitForEndOfFrame();
        // }
        // slowly slow down speed?
        CinemachineShake.Instance.ScreenShake(0f);
        _botMovement.MoveTowards(transform.position + (Vector3)_rb2D.velocity.normalized * _botMovement._slowRadius * 2);
        _botMovement.CanSlow(true);
        yield return new WaitForSeconds(2f); // charger slowing down to rest

        _botMovement._maxMoveSpeed = initialSpeed;
        _botMovement._maxSteeringForce = initialSteer;
        _botMovement.CanAvoid(true);
        _state = State.Chase;
    }
    
    private void OnTriggerEnter2D(Collider2D col) {
        // deal damage to player when charging (use a boolean to check for charging)
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
        Gizmos.DrawLine(transform.position, transform.position+(Vector3)_rb2D.velocity);
    }
}