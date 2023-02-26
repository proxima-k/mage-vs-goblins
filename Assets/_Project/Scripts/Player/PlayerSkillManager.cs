using UnityEngine;

public class PlayerSkillManager : MonoBehaviour {
    [SerializeField] private Orb _currOrb;
    [SerializeField] private bool canReset;

    private bool _canInput;

    private void Awake() {
        PlayerInputManager.I.OnSetPlayerInput += canInput => {
            _canInput = canInput;
        };
    }

    private void Update() {
        // create a timer countdown for attack ability
        if (!_canInput) return;
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(_currOrb.AttackAbility.TriggerAbility(transform));
            // AttackSystem.CastAOE(10, PK.GetMouseWorldPosition2D(Camera.main), aoeRadius);
        }

        if (Input.GetMouseButtonDown(1)) {
            StartCoroutine(_currOrb.UltimateAbility.TriggerAbility(transform));
        }

        if (canReset && Input.GetKeyDown(KeyCode.E)) {
            _currOrb.ResetOrbLevel();
        }
    }

    public void EquipOrb(Orb orb) {
        _currOrb = orb;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos() {
        // Gizmos.DrawWireSphere(PK.GetMouseWorldPosition2D(Camera.main), aoeRadius);
    }
    #endif
}
