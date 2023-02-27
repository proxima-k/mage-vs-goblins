using UnityEngine;

public class PlayerSkillManager : MonoBehaviour {
    [SerializeField] private Orb _currOrb;

#if UNITY_EDITOR
    [SerializeField] private bool canReset;
#endif
    
    private bool _canInput;

    private void Awake() {
        PlayerInputManager.I.OnSetPlayerInput += canInput => {
            _canInput = canInput;
        };
    }

    private void Update() {
        if (!_canInput) return;
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(_currOrb.AttackAbility.TriggerAbility(transform));
        }

        if (Input.GetMouseButtonDown(1)) {
            StartCoroutine(_currOrb.UltimateAbility.TriggerAbility(transform));
        }

#if UNITY_EDITOR
        if (canReset && Input.GetKeyDown(KeyCode.E)) {
            _currOrb.ResetOrbLevel();
        }
#endif
    }

    public void EquipOrb(Orb orb) {
        _currOrb = orb;
    }
}
