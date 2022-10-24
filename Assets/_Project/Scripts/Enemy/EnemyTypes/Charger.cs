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
    private BotMovement _movement;
    private Rigidbody2D _rb2D;

    [SerializeField] private float _chargeCooldown = 8f;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private float _chargeSpeed = 8f;
    [SerializeField] private float _pauseBeforeChargeDuration = 1f;
    [SerializeField] private float _chargeDuration = 1f;

    private float _chargeTimer;
    // todo: change cooldown check to a coroutine check

    protected override void Awake() {
        base.Awake();
        _movement = GetComponent<BotMovement>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        switch (_state) {
            case State.Spawn:
                _state = State.Chase;
                break;
            case State.Chase:
                _movement.Chase(_targetTf);
                // if within range, set state to charge
                if (_chargeTimer <= 0) {
                    if ((_targetTf.position - transform.position).sqrMagnitude <= _attackDistance * _attackDistance) {
                        _movement.Stop();
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
        yield return new WaitForSeconds(_pauseBeforeChargeDuration);
        
        // get target position
        // calculate velocity
        Vector3 chargeDir = (_targetTf.position - transform.position).normalized;
        // set rigidbody velocity
        _rb2D.velocity = chargeDir * _chargeSpeed;
        
        // todo: make this charging still follow player but adjust the steering 
        // todo: actually I think i can just change this whole section into just changing the movement properties
        yield return new WaitForSeconds(_chargeDuration);
        // wait for seconds(charge duration)
            // in the meantime, maybe shake the camera or something to show that the mob is charging
        // set velocity to very low
        // set state back to chase
        _rb2D.velocity = chargeDir * 0.5f;
        yield return new WaitForSeconds(0.5f);
        _state = State.Chase;

    }
    
    private void OnTriggerEnter2D(Collider2D col) {
        // deal damage to player when charging (use a boolean to check for charging)
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }
}
