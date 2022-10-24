using System;
using System.Collections;
using System.Collections.Generic;
using Codice.CM.Client.Differences.Graphic;
using UnityEngine;

public class BotMovement : MonoBehaviour {
    private Transform _target;
    [SerializeField] private float _minMoveSpeed = 2f;
    [SerializeField] private float _maxMoveSpeed = 5f;
    
    // avoidance behavior properties
    [SerializeField] private float _avoidRadius = 1f;
    [SerializeField] private LayerMask _avoidLayers;

    // weighted variables
    [SerializeField] private float avoidanceWeight = 1f;
    [SerializeField] private float seekWeight = 1f;
    
    // steering properties
    [SerializeField] private float _maxSteeringForce = 0.05f;
    [Range(0f,1f)]
    [SerializeField] private float _lerp = 0.8f;
    
    private Rigidbody2D _rb2D;
    
    private void Awake() {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (_target != null) {
            // get a desired velocity
            // get current velocity
            // get a steering force
            // add steering force to current velocity
            // clamp targeted velocity
            // apply targeted velocity to rigidbody's velocity
            Vector2 desiredDir = GetDesiredDirection();

            // possibly multiplying by another small value
            Vector2 steeringForce = SteerTowards(_rb2D.velocity, desiredDir);
            Vector2 desiredVelocity = _rb2D.velocity + steeringForce;

            // clamping velocity values to prevent moving too far
            // unable to use Vector2.ClampMagnitude since it only clamps the max magnitude
            float speed = Mathf.Clamp(desiredVelocity.magnitude, _minMoveSpeed, _maxMoveSpeed);
            desiredVelocity = desiredVelocity.normalized * speed;

            _rb2D.velocity = Vector2.Lerp(_rb2D.velocity, desiredVelocity, _lerp);
            // _rb2D.velocity = desiredVelocity;
        }
        else {
            // _rb2D.velocity = Vector2.zero;
        }

    }

    public void Chase(Transform target) {
        _target = target;
    }

    public void Stop() {
        _rb2D.velocity = Vector2.zero;
        _target = null;
    }
    
    // returns the most desirable direction base on adding target seeking and obstacle avoiding directions
    private Vector3 GetDesiredDirection() {
        Vector2 seekDir = (_target.position - transform.position).normalized * seekWeight;
        Vector2 avoidDir = GetAvoidanceVector() * avoidanceWeight;

        Vector2 desiredDir = (seekDir + avoidDir).normalized;
        
        return desiredDir;
    }

    // returns a final vector based on all the obstacles within the detection radius of this object
    private Vector3 GetAvoidanceVector() {
        Vector3 finalVector = Vector2.zero;
        
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(transform.position, _avoidRadius, _avoidLayers);
        foreach (var collider in colliders2D) {
            if (collider.transform != transform)
                finalVector += (transform.position - collider.transform.position).normalized;
        }

        return finalVector.normalized;
    }

    // returns a steering force
    private Vector3 SteerTowards(Vector3 currentVelocity, Vector3 desiredDir) {
        Vector3 steeringForce = desiredDir * _maxMoveSpeed - currentVelocity;
        return Vector3.ClampMagnitude(steeringForce, _maxSteeringForce);
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _avoidRadius);
        if (_target != null && _rb2D != null)
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)_rb2D.velocity);
    }
}
