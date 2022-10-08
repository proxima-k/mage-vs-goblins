using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor.UIElements;
using UnityEngine;


public class PlayerLocomotion : MonoBehaviour {
    
    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementInput;
    [SerializeField] private float _moveSpeed = 10f;
    
    private bool _isDashing;
    [SerializeField] private float _dashDuration = 0.3f;
    [SerializeField] private float _dashSpeed = 30f;
    private float _dashTimer;
    private Vector2 _dashDir;
    
    void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.y = Input.GetAxisRaw("Vertical");

        if (!_isDashing && Input.GetKeyDown(KeyCode.Space)) {
            _isDashing = true;
            _dashTimer = _dashDuration;
            _dashDir = _movementInput.normalized;
        }

        if (_isDashing) {
            _dashTimer -= Time.deltaTime;
            _rigidbody2D.velocity = _dashDir * _dashSpeed;
            if (_dashTimer <= 0)
                _isDashing = false;
        }
        else {
            Move();
        }
    }

    void Move() {
        _rigidbody2D.velocity = _movementInput.normalized * _moveSpeed;
    }

    public void UpdateSpeed(float speed) {
        _moveSpeed = speed;
    }
}
