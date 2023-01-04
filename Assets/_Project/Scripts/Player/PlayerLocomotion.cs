using Proxima_K.Utils;
using UnityEngine;


public class PlayerLocomotion : MonoBehaviour {
    
    private Rigidbody2D _rb2D;
    private Vector2 _movementInput;
    [SerializeField] private float _moveSpeed = 10f;
    public float MoveSpeed => _moveSpeed;
    
    [Header("Dash Properties")]
    private bool _isDashing;
    [SerializeField] private float _dashDuration = 0.3f;
    [SerializeField] private float _dashSpeed = 30f;
    private float _dashTimer;
    private Vector2 _dashDir;
    private bool _canDash = true;

    [SerializeField] private int _maxDashCharges = 3;
    [SerializeField] private float _dashChargeTank;
    [SerializeField] private float _dashRechargeSpeed = 1;
    [SerializeField] private DashAbilityUI _dashAbilityUI;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Vector3 _mousePos;
    private int _isRunningHash;
    private bool _canInput;
    
    void Awake() {
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _isRunningHash = Animator.StringToHash("IsRunning");
        
        PlayerInputManager.I.OnSetPlayerInput += canInput => {
            _canInput = canInput;
            if (!canInput)
                _movementInput = Vector2.zero;
        };
        
        _dashChargeTank = _maxDashCharges;
        if (_dashAbilityUI != null)
            _dashAbilityUI.Initialize(_maxDashCharges);
    }

    void Update() {
        if (_canInput) {
            _mousePos = PK.GetMouseWorldPosition2D(Camera.main);
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");

            if (_dashChargeTank >= 1  && !_isDashing && Input.GetKeyDown(KeyCode.Space) && _canDash) {
                _isDashing = true;
                _dashTimer = _dashDuration;
                _dashDir = _movementInput.normalized;

                UseDashCharge();
            }
        }
        
        if (_isDashing) {
            _dashTimer -= Time.deltaTime;
            _rb2D.velocity = _dashDir * _dashSpeed;
            if (_dashTimer <= 0)
                _isDashing = false;
        }
        else {
            Move();
        }
        RechargeDash();
        
        
        // Visuals
        if (_animator != null) {
            if (_movementInput != Vector2.zero)
                _animator.SetBool(_isRunningHash, true);
            else
                _animator.SetBool(_isRunningHash, false);
        }

        if ((_mousePos - transform.position).x >= 0) 
            _spriteRenderer.flipX = false;
        else
            _spriteRenderer.flipX = true;
    }

    void Move() {
        _rb2D.velocity = _movementInput.normalized * _moveSpeed;
    }

    public void UpdateSpeed(float speed) {
        _moveSpeed = speed;
    }

    public void SetDash(bool canDash) {
        _canDash = canDash;
    }

    private void UseDashCharge() {
        _dashChargeTank--;
        _dashAbilityUI.UpdateDashChargeBar(_dashChargeTank);
    }
    
    private void RechargeDash() {
        // if percentage reaches 1, add 1 to current charges
        if (_dashChargeTank >= _maxDashCharges) return;
        _dashChargeTank += Time.deltaTime * _dashRechargeSpeed;
        if (_dashAbilityUI!=null)
            _dashAbilityUI.UpdateDashChargeBar(_dashChargeTank);
    }
}
