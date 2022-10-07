using System;
using Proxima_K.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerSkills : MonoBehaviour {
    private Camera _cam;
    [SerializeField] private Transform _fireballTf;
    [SerializeField] private float _fireballSpeed = 20f;

    [Header("Laser Properties")]
    [SerializeField] private bool _visualizeLaserArea;
    [SerializeField] private float _laserDistance = 5f;
    [SerializeField] private float _laserWidth = 2f;
    [SerializeField] private float _laserEndPointOffset = 0.1f;

    private void Start() {
        _cam = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // ShootFireball();
            ShootLaser();
        }
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

    private void ShootLaser() {
        // Gets direction and angle for box cast
        Vector2 targetDir = (PK.GetMouseWorldPosition2D(_cam) - (Vector2) transform.position).normalized;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        
        // todo: resolve the endpoint offset since it doesn't allow for value of 0
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(_laserEndPointOffset,_laserWidth), angle, targetDir, _laserDistance);
        
        foreach (var hit in hits) {
            if(hit.transform.TryGetComponent(out IHealthDamageable healthDamageable))
                healthDamageable.Damage(5);
        }
    }

    private void OnDrawGizmos() {
        if (_visualizeLaserArea) {
            Vector3 origin = transform.position;
            Vector3 targetPos = PK.GetMouseWorldPosition2D(Camera.main);
            Vector3 targetDir = (targetPos - origin).normalized;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

            Gizmos.color = Color.green;
            Gizmos.matrix = Matrix4x4.TRS(origin + targetDir * _laserDistance * 0.5f, Quaternion.Euler(0, 0, angle),
                Vector3.one);
            Gizmos.DrawWireCube(Vector2.zero, new Vector3(2 * _laserEndPointOffset + _laserDistance, _laserWidth));
        }
    }
}
