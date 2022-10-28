using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class Boglin : Enemy {
    private enum State {
        Spawn,
        Process,
        Attack1,    // circular projectile
        Attack2,    // target projectile
        Attack3,    // height projectiles / jump itself?
        Healing,    // spawn towers that heal boss after a time, also spawn enemies to distract players
        Death       // drop a skull 
    }
    private State _state = State.Spawn;
    private BotMovement _movement;

    // general
    [SerializeField] private Transform _projectilePf;
    
    [Header("Attack 1 Properties")]
    [SerializeField] private float _atk1Duration = 10f;
    [SerializeField] private float _atk1Pause = .3f;
    [SerializeField] private int _atk1Damage = 15; 
    [SerializeField] private int _atk1ProjectilePerPulse = 2;
    [SerializeField] private float _rotateSpeed = 10f;
    [SerializeField] private LayerMask _damageLayers;

    // processing/decision making properties
    [SerializeField] private float _processDuration = 5f;
    private float _processTimer;
    
    private Coroutine _currentRoutine;
    private const float TAU = Mathf.PI * 2;

    protected override void Awake() {
        base.Awake();
        _movement = GetComponent<BotMovement>();
    }
    
    private void Update() {
        switch (_state) {
            case State.Spawn:
                _state = State.Process;
                _processTimer = _processDuration;
                break;
            case State.Process:
                if (_processTimer > 0) {
                    _processTimer -= Time.deltaTime;
                }
                else { // DECISION MAKING
                    // when health has been damage for an amount, go into healing state.
                    // otherwise random between attacks
                    // _state = (State)Random.Range(2, 5);
                    _state = State.Attack1;
                    _processTimer = _processDuration;
                }
                break;
            case State.Attack1:
                if (_currentRoutine == null)
                    _currentRoutine = StartCoroutine(Attack1());
                break;
            case State.Attack2:
                if (_currentRoutine == null)
                    _currentRoutine = StartCoroutine(Attack2());
                break;
            case State.Attack3:
                if (_currentRoutine == null)
                    _currentRoutine = StartCoroutine(Attack3());
                Debug.Log("Attack 3");
                break;
            case State.Healing:
                break;
            case State.Death:
                break;
        }
    }

    private IEnumerator Attack1() {
        Debug.Log("Attack 1");
        float timer = _atk1Duration;

        float pauseTimer = 0;
        float rad = 0;
        float _desiredRad = 0;
        Vector3 dir = Vector3.zero;
        while (timer > 0) {
            // loop through fire count, and shoot projectile
            if (pauseTimer <= 0) {
                for (int i = 0; i < _atk1ProjectilePerPulse; i++) {
                    _desiredRad = rad + TAU * i / _atk1ProjectilePerPulse;
                    dir = new Vector3(Mathf.Cos(_desiredRad), Mathf.Sin(_desiredRad));
                    AttackUtils.ShootProjectile(_projectilePf, _atk1Damage, transform.position, dir, 5f, _damageLayers, true);
                }

                pauseTimer = _atk1Pause;
            }
            else {
                pauseTimer -= Time.deltaTime;
            }

            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
            rad += Time.deltaTime * _rotateSpeed;
        }
        yield return new WaitForSeconds(_atk1Pause);
        
        _currentRoutine = null;
        _state = State.Process;
    }
    
    private IEnumerator Attack2() {
        Debug.Log("Attack 2");
        yield return new WaitForSeconds(1);
        
        _currentRoutine = null;
        _state = State.Process;
    }
    
    private IEnumerator Attack3() {
        Debug.Log("Attack 3");
        yield return new WaitForSeconds(1);
        
        _currentRoutine = null;
        _state = State.Process;
    }
}
