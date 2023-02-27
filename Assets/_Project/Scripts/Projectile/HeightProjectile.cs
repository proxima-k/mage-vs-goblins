using System;
using UnityEngine;

public class HeightProjectile : Projectile {

    [SerializeField] private float _verticalVelocity;
    [SerializeField] private float _gravity = 9.81f * 2f;
    private float _height => _bodyTf.localPosition.y;

    public event Action OnGroundHit;

    private void Update() {
        // gravity acting on projectile
        _bodyTf.localPosition -= new Vector3(0, _verticalVelocity * Time.deltaTime);
        _verticalVelocity += _gravity * Time.deltaTime;
        
        // ground hit checking
        if (_height+transform.localPosition.y <= transform.localPosition.y) {
            OnGroundHit?.Invoke();
            
            // cause the projectile to bounce if nothing is called on ground hit
            _verticalVelocity = -_verticalVelocity;
        }
    }

    public void Launch(Vector3 targetPos, float timeToReach) {
        // calculates horizontal velocity base on time
        Vector2 displacement = targetPos - transform.localPosition;
        float speed = displacement.magnitude / timeToReach;
        // uses the base projectile script's function to move horizontally
        base.Launch(displacement.normalized, speed);

        // calculates vertical velocity base on time and existing height
        float verticalVelocity = (_height - _gravity * timeToReach * timeToReach * 0.5f) / timeToReach;
        _verticalVelocity = verticalVelocity;
    }
    
}
