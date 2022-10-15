using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Orb")]
public class Orb : ScriptableObject {

    public string OrbName;
    public Sprite Icon;
    public Ability AttackAbility;
    // public Ability UltimateAbility;
    
    public void UpgradeOrb() {
        AttackAbility.UpgradeAbility();
    }

    public void ResetOrbLevel() {
        AttackAbility.ResetAbilityLevel();
    }
}
