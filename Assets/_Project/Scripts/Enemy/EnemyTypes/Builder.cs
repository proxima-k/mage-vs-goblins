using System;
using System.Collections;
using Codice.Client.Common.Connection;
using UnityEngine;

public class Builder : Enemy {
    private enum State {
        Spawn,
        Roam,
        Build,
        Death
    }
    private State _state = State.Spawn;
    private BotMovement _movement;

    // building properties
    [SerializeField] private Transform _towerPf;
    [SerializeField] private float _buildCooldown = 20f;
    private float _buildTimer;
    
    protected override void Awake() {
        base.Awake();
        _movement = GetComponent<BotMovement>();
    }

    private void Update() {
        switch (_state) {
            case State.Spawn:
                // spawn animation or something
                _state = State.Roam;
                break;
            case State.Roam:
                // perhaps take cover if it has a tower
                // if builder doesn't have a tower, then run away from player
                if (_targetTf != null) {
                    _movement.Chase(_targetTf);
                }
                else {
                    _movement.Stop();
                }


                if (_buildTimer <= 0) {
                    Debug.Log("Building AHHHHHHHHHH");
                    _state = State.Build;
                }
                break;
            case State.Build:
                // finds a location and 
                // builds a tower or several
                // requires a reference to the tower enemy type
                
                // build enumerator
                StartCoroutine(BuildTower());
                break;
        }

        if (_state != State.Build && _buildTimer > 0) {
            _buildTimer -= Time.deltaTime;
        }
    }

    private IEnumerator BuildTower() {
        Debug.Log("Building a tower");
        Instantiate(_towerPf, transform.position, Quaternion.identity);
        _buildTimer = _buildCooldown;
        _state = State.Roam;
        yield return null;
    }
}
