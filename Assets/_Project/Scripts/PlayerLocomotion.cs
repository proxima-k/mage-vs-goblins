using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour {
    
    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementInput;
    [SerializeField] private float _moveSpeed = 10f;
    
    void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.y = Input.GetAxisRaw("Vertical");

        _rigidbody2D.velocity = _movementInput * _moveSpeed;
    }
}
