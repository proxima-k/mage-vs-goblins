using System;
using System.Collections;
using AbilitySystem;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilitySystem/Boglin Attack 2")]
public class BoglinAttack2 : Ability {
    public Transform _projectilePf;

    public override IEnumerator TriggerAbility(Transform abilityCaster, Action callback = null) {
        Debug.Log("Attack 1");
        yield return null;
        callback?.Invoke();
    }
}
