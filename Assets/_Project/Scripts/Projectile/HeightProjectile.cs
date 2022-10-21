using System;
using System.Collections;
using System.Collections.Generic;
using Codice.CM.Client.Differences;
using UnityEditor.UIElements;
using UnityEngine;

public class HeightProjectile : Projectile {

    [SerializeField] private float _verticalVelocity;
    [SerializeField] private float _gravity = 9.81f * 2f;
    private float _height => _bodyTf.localPosition.y;

    public event Action OnGroundHit;

    void Update() {
        _bodyTf.localPosition -= new Vector3(0, _verticalVelocity * Time.deltaTime);
        
        _verticalVelocity += _gravity * Time.deltaTime;
        if (_height+transform.localPosition.y <= transform.localPosition.y) {
            OnGroundHit?.Invoke();
            // perhaps change this to a function or something like bounce()
            _verticalVelocity = -_verticalVelocity;
        }
    }

    public void Launch(Vector3 targetPos, float timeToReach) {
        // calculates horizontal velocity base on time
        Vector2 displacement = targetPos - transform.localPosition;
        float speed = displacement.magnitude / timeToReach;
        // Vector2 horizontalVelocity =  displacement / timeToReach;
        base.Launch(displacement.normalized, speed);

        // calculates vertical velocity base on time and existing height
        float verticalVelocity = (_height - _gravity * timeToReach * timeToReach * 0.5f) / timeToReach;
        // Debug.Log("Vertical velo" + verticalVelocity);
        _verticalVelocity = verticalVelocity;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
