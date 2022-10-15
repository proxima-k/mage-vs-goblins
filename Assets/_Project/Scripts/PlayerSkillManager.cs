using Proxima_K.Utils;
using UnityEngine;
using AbilitySystem;

public class PlayerSkillManager : MonoBehaviour {
    [SerializeField] private Orb _currOrb;

    private void Start() {
    }

    private void Update() {
        // create a timer countdown for attack ability
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(_currOrb.AttackAbility.TriggerAbility(transform));
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            _currOrb.UpgradeOrb();
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            _currOrb.ResetOrbLevel();
        }
    }
}
