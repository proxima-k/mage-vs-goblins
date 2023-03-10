using System.Collections;
using UnityEngine;

public class Ranger : Enemy {
    private enum State {
        Spawn,
        Chase,
        Attack,
        Death
    }
    private State _state = State.Spawn;
    private BotLocomotion _locomotion;

    [SerializeField] private Transform _projectilePf;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDistance = 10f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private LayerMask _damageLayers;

    private float _attackTimer;
    private Coroutine _attackRoutine;
    
    protected override void Awake() {
        base.Awake();
        _locomotion = GetComponent<BotLocomotion>();
    }

    private void Update() {
        switch (_state) {
            case State.Spawn:
                _state = State.Chase;
                break;
            case State.Chase:
                // idea: make ranger locate a position around player before shooting
                _locomotion.MoveTowards(_targetTf.position);

                // if within range, attack
                if (_attackTimer <= 0) {
                    if ((_targetTf.position - transform.position).sqrMagnitude <= _attackDistance * _attackDistance)
                        _state = State.Attack;
                }
                break;
            case State.Attack:
                if (_attackRoutine == null) {
                    _attackRoutine = StartCoroutine(Attack());
                }
                break;
            case State.Death:
                break;
        }

        if (_attackTimer > 0)
            _attackTimer -= Time.deltaTime;
    }

    private IEnumerator Attack() {
        _locomotion.Stop();
        DangerMarkPopup.Create(transform.position);
        
        yield return new WaitForSeconds(1f);
        Vector3 dir = (_targetTf.position - transform.position).normalized;
        AttackUtils.ShootProjectile(_projectilePf, _damage, transform.position, dir, _projectileSpeed, _damageLayers, true);

        _attackTimer = _attackCooldown;
        _attackRoutine = null;
        _state = State.Chase;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }
}
