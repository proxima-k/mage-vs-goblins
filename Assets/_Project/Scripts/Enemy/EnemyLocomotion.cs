using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotion : MonoBehaviour {
    
    // reference to enemy state
    // update the state base on movement ai
    
    [SerializeField] private Transform _chaseTarget;
    [SerializeField] private float _moveSpeed = 5f;
    private Rigidbody2D _rb2D;

    private void Awake() {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Seek();
    }

    public void SetTarget(Transform target) {
        // if (target == null)
            // _rb2D.velocity = Vector2.zero;
        _chaseTarget = target;
    }
    
    // seek player
    // avoid player
    public void Seek() {
        if (_chaseTarget != null) {
            Vector2 displacement = _chaseTarget.position - transform.position;
            // limits the enemy f
            if (displacement.magnitude > 0.5) {
                Vector2 chaseDir = displacement.normalized;
                _rb2D.velocity = chaseDir * _moveSpeed;
            }
            else {
                _rb2D.velocity = Vector2.zero;
            }
        }
        else {
            _rb2D.velocity = Vector2.zero;
        }
    }
}
