using System;
using Proxima_K.Utils;
using UnityEngine;
using AbilitySystem;

public class PlayerSkillManager : MonoBehaviour {
    [SerializeField] private Orb _currOrb;
    [SerializeField] private float aoeRadius = 3f;
    
    private void Update() {
        // create a timer countdown for attack ability
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(_currOrb.AttackAbility.TriggerAbility(transform));
            // AttackSystem.CastAOE(10, PK.GetMouseWorldPosition2D(Camera.main), aoeRadius);
        }

        if (Input.GetMouseButtonDown(1)) {
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            _currOrb.ResetOrbLevel();
        }
    }

    public void EquipOrb(Orb orb) {
        _currOrb = orb;
    }

    private void OnDrawGizmos() {
        // Gizmos.DrawWireSphere(PK.GetMouseWorldPosition2D(Camera.main), aoeRadius);
    }
}
