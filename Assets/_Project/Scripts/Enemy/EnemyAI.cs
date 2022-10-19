using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour {
    
    // reference to enemy state
    // update the state base on movement ai
    private enum State {
        Spawn,
        Chase,
        Attack,
        Death
    }
    private State _state = State.Spawn;

    private EnemyMovement _enemyMovement;
    private Enemy _enemyStats;
    
    // attack stuff
    [SerializeField] private Transform _projectileTf;
    [SerializeField] private LayerMask _projectileCollisionLayers;
    
    // setup for chase
    [SerializeField] private Transform _attackTarget;
    [SerializeField] private float _attackDistance = 5f;

    private void Awake() {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update() {
        

        switch (_state) {
            case State.Spawn:
                // _enemyMovement.Stop();
                _state = State.Chase;
                break;
            case State.Chase:
                Vector3 displacement = _attackTarget.position - transform.position;
        
                // if within attack distance
                if (displacement.sqrMagnitude <= _attackDistance * _attackDistance) {
                    _enemyMovement.Stop();
                    Attack();
                }
                else {
                    _enemyMovement.Chase(_attackTarget);
                }
                break;
            case State.Death:
                // dead stuff here
                break;
        }

        if (_attackTimer > 0) {
            _attackTimer -= Time.deltaTime;
        }
    }


    [Header("Attack Properties")]
    private float _attackTimer = 0;

    [SerializeField] private float _attackRate = 1f;
    
    private void Attack() {
        if (_attackTimer <= 0) {
            Vector2 attackDir = (_attackTarget.position - transform.position).normalized;
            AttackSystem.ShootProjectile(_projectileTf, 0, transform.position, attackDir,15f, _projectileCollisionLayers);
            _attackTimer = _attackRate;
        }
    }

    private void CheckAttackDistance() {
        Vector3 displacement = _attackTarget.position - transform.position;
        
        // if within attack distance
        if (displacement.sqrMagnitude <= _attackDistance * _attackDistance) {
            _state = State.Attack;
            _enemyMovement.Stop();
        }
    }
    
    public void SetTarget(Transform target) {
        // if (target == null)
            // _rb2D.velocity = Vector2.zero;
        _attackTarget = target;

        // get random point around target
        // int points = 16;
        // float rad = Mathf.PI * 2 * Random.Range(0, points) / points;
        // _followPoint = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)) * Random.Range(2, _directChaseDistance-5);
    }

}
