using UnityEngine;

public class BotLocomotion : MonoBehaviour {
    
    public float _minMoveSpeed = 2f;
    public float _maxMoveSpeed = 5f;
    
    // avoidance behavior properties
    public float _avoidRadius = 1f;
    public LayerMask _avoidLayers;

    // weighted variables
    public float avoidanceWeight = 1f;
    public float seekWeight = 1f;
    
    // steering properties
    public float _maxSteeringForce = 0.05f;
    [Range(0f,1f)]
    public float _lerp = 0.8f;

    public float _slowRadius = 1f;
    
    private Rigidbody2D _rb2D;
    private Vector3 _targetPos;
    private bool _canMove;
    private bool _canSlow = true;
    private bool _canAvoid = true;
    public void CanSlow(bool canSlow) => _canSlow = canSlow;
    public void CanAvoid(bool canAvoid) => _canAvoid = canAvoid;

    private SpriteRenderer _spriteRenderer;
    public bool flipSprite;
    
    private void Awake() {
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        if (_canMove) {
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
            
            // if position is close to target, then slow down
            
            if (_canSlow && (_targetPos - transform.position).sqrMagnitude < _slowRadius * _slowRadius)
                desiredVelocity *= (_targetPos - transform.position).magnitude / _slowRadius;

            _rb2D.velocity = Vector2.Lerp(_rb2D.velocity, desiredVelocity, _lerp);
            // _rb2D.velocity = desiredVelocity;
            
            if (_spriteRenderer != null)
                UpdateFacingDirection();
        }
    }

    public void MoveTowards(Vector3 position) {
        _canMove = true;
        _targetPos = position;
    }
    
    public void Stop() {
        _canMove = false;
        _rb2D.velocity = Vector2.zero;
    }

    // gets a float based on the dot product of current direction and direction towards target
    // this is to see if the value is positive or negative.
    public bool TargetIsBehind() {
        Vector3 towardsTargetDir = _targetPos - transform.position;
        return Vector3.Dot(towardsTargetDir.normalized, _rb2D.velocity.normalized) < 0;
    }
    
    // returns the most desirable direction base on adding target seeking and obstacle avoiding directions
    private Vector3 GetDesiredDirection() {
        Vector2 seekDir = (_targetPos - transform.position).normalized * seekWeight;
        Vector2 avoidDir = Vector2.zero;
        if (_canAvoid)
            avoidDir = GetAvoidanceVector() * avoidanceWeight;

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

    private void UpdateFacingDirection() {
        _spriteRenderer.flipX = _rb2D.velocity.x > 0 ? !flipSprite : flipSprite;
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _avoidRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_targetPos, _slowRadius);
        // if (_target != null && _rb2D != null)
        //     Gizmos.DrawLine(transform.position, transform.position + (Vector3)_rb2D.velocity);
    }
}
