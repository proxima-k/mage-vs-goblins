using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyLocomotion : MonoBehaviour {
    
    // reference to enemy state
    // update the state base on movement ai
    private enum State {
        Follow,
        DirectChase,
        Attack
    }
    private State _state = State.Follow;
    
    // attack stuff
    [SerializeField] private Transform _projectileTf;
    [SerializeField] private LayerMask _projectileCollisionLayers;
    
    // movement properties
    [SerializeField] private float _maxSpeed = 5f;
    // [Range(0f, 5f)]
    [SerializeField] private float _maxSteeringForce = 5;
    private Rigidbody2D _rb2D;

    // setup for chase
    [SerializeField] private Transform _chaseTarget;
    [SerializeField] private float _directChaseDistance = 10f;
    [SerializeField] private float _attackDistance = 5f;
    private Vector3 _followPoint;

    private void Awake() {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (_chaseTarget != null) {
            float sqrDistance = (_chaseTarget.position - transform.position).sqrMagnitude;
            float sqrChaseDistance = _directChaseDistance * _directChaseDistance;
            float sqrAttackDistance = _attackDistance * _attackDistance;
            switch (_state) {
                case State.Follow:
                    if (sqrDistance < sqrChaseDistance)
                        _state = State.DirectChase;
                    if (sqrDistance < sqrAttackDistance)
                        _state = State.Attack;
                    
                    Seek();
                    break;
                case State.DirectChase:
                    if (sqrDistance >= sqrChaseDistance)
                        _state = State.Follow;
                    else if (sqrDistance < sqrAttackDistance)
                        _state = State.Attack;
                    
                    Chase();
                    break;
                case State.Attack:
                    if (sqrDistance > sqrAttackDistance)
                        _state = State.DirectChase;
                        
                    Attack();
                    _rb2D.velocity = Vector2.zero;
                    
                    break;
            }
        }
        
        // attack timer
        if (_attackTimer > 0) {
            _attackTimer -= Time.deltaTime;
        }
    }

    [Header("Attack Properties")]
    private float _attackTimer = 0;

    [SerializeField] private float _attackRate = 1f;
    
    private void Attack() {
        if (_attackTimer <= 0) {
            Vector2 attackDir = (_chaseTarget.position - transform.position).normalized;
            AttackSystem.ShootProjectile(_projectileTf, 0, transform.position, attackDir,10f, _projectileCollisionLayers);
            _attackTimer = _attackRate;
        }
    }
    
    public void SetTarget(Transform target) {
        // if (target == null)
            // _rb2D.velocity = Vector2.zero;
        _chaseTarget = target;
        if (target == null) {
            _rb2D.velocity = Vector2.zero;
            return;
        }
        
        // get random point around target
        int points = 16;
        float rad = Mathf.PI * 2 * Random.Range(0, points) / points;
        _followPoint = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)) * Random.Range(2, _directChaseDistance-5);
    }

    public void Chase() {
        Vector2 chaseDir = (_chaseTarget.position - transform.position).normalized;
        _rb2D.velocity = chaseDir * _maxSpeed;
    }
    
    public void Seek() {
        if (_chaseTarget != null) {
            Vector2 displacement = (_chaseTarget.position + _followPoint) - transform.position;
            Vector2 steer = Vector2.zero;
            Vector2 velocity = _rb2D.velocity;
            // seek
            steer += SteerTowards(_rb2D.velocity, displacement)*0.01f;
            velocity += steer;

            float speed = velocity.magnitude;
            speed = Mathf.Clamp(speed, 2f, _maxSpeed);
            
            velocity = velocity.normalized * speed;
            _rb2D.velocity = velocity;

            // transform.right = _rb2D.velocity;
        }
        else {
            _rb2D.velocity = Vector2.zero;
        }
    }

    // returns a steering force
    private Vector2 SteerTowards(Vector2 currentVector, Vector2 targetVector) {
        Vector2 steeringForce = (targetVector.normalized * _maxSpeed) - currentVector;
        return Vector2.ClampMagnitude(steeringForce, _maxSteeringForce);
    }
    
}
