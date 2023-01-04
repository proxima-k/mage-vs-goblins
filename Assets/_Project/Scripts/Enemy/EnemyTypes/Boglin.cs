using UnityEngine;
using Random = UnityEngine.Random;

public class Boglin : Enemy {
    private enum State {
        Spawn,
        Process,
        Move,
        Attack1,    // circular projectile
        Attack2,    // target projectile
        // Attack3,    // height projectiles / jump itself?
        Healing,    // spawn towers that heal boss after a time, also spawn enemies to distract players
        Death       // drop a skull 
    }
    private State _state = State.Spawn;
    private BotMovement _movement;

    // attack abilities
    [SerializeField] private BoglinAttack1 _attack1;
    [SerializeField] private BoglinAttack2 _attack2;


    // processing/decision making properties
    [SerializeField] private float _processDuration = 3f;
    private float _processTimer;
    
    protected override void Awake() {
        base.Awake();
        _movement = GetComponent<BotMovement>();
    }
    
    private void Update() {
        switch (_state) {
            case State.Spawn:
                _state = State.Process;
                _processTimer = _processDuration;
                break;
            case State.Process:
                if (_processTimer > 0) {
                    _processTimer -= Time.deltaTime;
                }
                else { // DECISION MAKING
                    // when health has been damage for an amount, go into healing state.
                    // otherwise random between attacks
                    _state = (State)Random.Range(3, 4);
                    // _state = State.Attack1;
                    _processTimer = _processDuration;
                }
                break;
            case State.Move:
                break;
            case State.Attack1:
                if (_currentRoutine == null)
                    _currentRoutine = StartCoroutine(
                        _attack1.TriggerAbility(transform, ChangeToProcessState));
                break;
            case State.Attack2:
                if (_currentRoutine == null)
                    _currentRoutine = StartCoroutine(
                        _attack2.TriggerAbility(transform, ChangeToProcessState));
                break;
            case State.Healing:
                break;
            case State.Death:
                break;
        }
        
    }
    

    private void ChangeToProcessState() {
        _currentRoutine = null;
        _state = State.Process;
    }
}
