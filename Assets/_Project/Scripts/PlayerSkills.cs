using System;
using Proxima_K.Utils;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerSkills : MonoBehaviour {
    private Camera _cam;
    [SerializeField] private Transform _fireballTf;
    [SerializeField] private float _fireballSpeed = 20f;

    private void Start() {
        _cam = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
            ShootFireball();
    }

    private void ShootFireball() {
        // spawn from stated location
        Transform fireballInstance = Instantiate(_fireballTf, transform.position, Quaternion.identity);
        
        // add component of rigidbody if it doesn't have one
        Rigidbody2D rb2D = fireballInstance.gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        // rb2D.bodyType = RigidbodyType2D.Kinematic;
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2D.gravityScale = 0;

        // add velocity/force towards direction
        Vector2 targetDir = (PK.GetMouseWorldPosition2D(_cam) - (Vector2)transform.position).normalized;
        rb2D.velocity = targetDir * _fireballSpeed;
    }
}
