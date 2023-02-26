using UnityEngine;

public class Tower : Enemy {
    private enum State {
        Spawn,
        Shoot,
        Death
    }
    private State _state;

    [SerializeField] private Transform _projectilePf;
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _fireRate = 1.5f;
    [SerializeField] private int _fireCount = 3;
    [SerializeField] private float _rotateSpeed = 10f;
    private float _fireTimer;
    [SerializeField] private LayerMask _damageLayers;

    private float _currentRad;
    private const float TAU = Mathf.PI * 2;
    
    void Update() {
        switch (_state) {
            case State.Spawn:
                //setup
                _state = State.Shoot;
                break;
            case State.Shoot:
                // shooting algorithm
                // keep track of angle of shooting
                // add 
                if (_fireTimer <= 0) {
                    Shoot();
                    _fireTimer = _fireRate;
                }
                break;
            case State.Death:
                // play death stuff
                break;
        }

        if (_fireTimer > 0)
            _fireTimer -= Time.deltaTime;
        
        // rotate aim angle
        if (_currentRad >= TAU) _currentRad -= TAU;
        _currentRad += Time.deltaTime * _rotateSpeed;
    }

    private void Shoot() {
        // for loop with angle, projectile count per shot
        for (int i = 0; i < _fireCount; i++) {
            float rad = TAU * i / _fireCount + _currentRad;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
            AttackUtils.ShootProjectile(_projectilePf, _damage, transform.position, dir, 5f, _damageLayers);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(_currentRad), Mathf.Sin(_currentRad))*5f);
    }
}
