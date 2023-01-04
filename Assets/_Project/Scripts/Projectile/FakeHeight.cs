using System;
using UnityEngine;

public class FakeHeight : Projectile {
    // horizontal position
    // vertical position (transform of object)
    [SerializeField] private Transform _bodyTf;
    [SerializeField] private float _gravity = 9.81f;
    private float _currHeight;

    private float _currVelocity;
    public event Action OnGroundHit;

    private void Update() {
        // update height value
        _bodyTf.localPosition += new Vector3(0, _currVelocity * Time.deltaTime);

        // checks if object hits the ground
        CheckGroundHit();
    }

    public void Launch(float timeToReach, Vector3 targetPos) {
        // initializes the starting vertical velocity
        _currVelocity = _gravity * timeToReach/2;
        
        // move horizontally to target position within time
        // Move((targetPos - transform.position)/timeToReach);
    }

    private void CheckGroundHit() {
        // if the object hits the ground
        if (_bodyTf.position.y < transform.position.y) {
            _bodyTf.position = transform.position;
            _currVelocity = 0;
            OnGroundHit?.Invoke();
        } 
        // if the object is in air, then keep pulling it down
        else if (_bodyTf.position.y > transform.position.y) {
            _currVelocity -= _gravity * Time.deltaTime;
        }
    }
}
