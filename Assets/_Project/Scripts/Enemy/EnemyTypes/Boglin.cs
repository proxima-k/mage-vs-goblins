using UnityEngine;
using Random = UnityEngine.Random;

public class Boglin : Enemy {
    private enum State {
        Spawn,
        Rest,
        Move,
        Attack1,    // circular projectile
        Attack2,    // target projectile
        // Attack3,    // height projectiles / jump itself?
        Healing,    // spawn towers that heal boss after a time, also spawn enemies to distract players
        Death       // drop a skull 
    }
    private State _state = State.Spawn;
    private BotLocomotion _locomotion;

    // attack abilities
    [SerializeField] private BoglinAbility1 ability1;
    [SerializeField] private BoglinAbility2 ability2;
    [SerializeField] private BoglinAbility3 ability3;
    
    // processing/decision making properties
    [SerializeField] private float _restDuration = 3f;
    private float _restTimer;

    [SerializeField] private int damageTaken = 0;
    [SerializeField] private int _damageThresholdToSummon = 600;
    [SerializeField] private float _attackDistance = 4f;
    
    protected override void Awake() {
        base.Awake();
        _locomotion = GetComponent<BotLocomotion>();
        _healthSystem.OnDamage += damage => { damageTaken += damage; };
        _healthSystem.OnDeath += () => {
            PlayerInputManager.I.SetPlayerInput(false);
            GameStateUIManager.I.TriggerGameWin();
        };
    }
    
    private void Update() {
        switch (_state) {
            case State.Spawn:
                _state = State.Move;
                _restTimer = _restDuration;
                break;
            case State.Rest:
                if (_restTimer > 0) {
                    _restTimer -= Time.deltaTime;
                }
                else {
                    _state = State.Move;
                }
                break;
            case State.Move:
                _locomotion.MoveTowards(_targetTf.position);
                if ((_targetTf.position - transform.position).sqrMagnitude <= _attackDistance * _attackDistance) {
                    _locomotion.Stop();
                    _state = (State) Random.Range(3, 5);
                }
                break;
            case State.Attack1:
                if (_currentRoutine == null)
                    _currentRoutine = StartCoroutine(
                        ability1.TriggerAbility(transform, ChangeToRestState));
                break;
            case State.Attack2:
                if (_currentRoutine == null)
                    _currentRoutine = StartCoroutine(
                        ability2.TriggerAbility(transform, ChangeToRestState));
                break;
            case State.Healing:
                if (_currentRoutine == null)
                    _currentRoutine = StartCoroutine(
                        ability3.TriggerAbility(transform, ChangeToRestState));
                break;
        }
        
        
        if (damageTaken >= _damageThresholdToSummon) {
            if (_currentRoutine != null) {
                StopCoroutine(_currentRoutine);
                _currentRoutine = null;
            }
            _state = State.Healing;
            damageTaken -= _damageThresholdToSummon;
            _locomotion.Stop();
        }
        // Debug.Log(_state);
    }
    

    private void ChangeToRestState() {
        _currentRoutine = null;
        _state = State.Rest;
        _restTimer = _restDuration;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }
}
