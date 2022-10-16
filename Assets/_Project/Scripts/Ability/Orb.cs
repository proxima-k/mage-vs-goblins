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

    public int[] UpgradeCosts = new int[3];
    private int _currLevelIndex = 0;
    // public Ability UltimateAbility;
    
    // Should be used together with UpgradeOrb()
    public bool CanUpgrade() {
        return _currLevelIndex < UpgradeCosts.Length;
    }
    
    public bool UpgradeOrb(CurrencySystem currencySystem) {
        // UpgradeCosts[_currLevelIndex]
        if (currencySystem.Spend(UpgradeCosts[_currLevelIndex])) {
            AttackAbility.UpgradeAbility();
            _currLevelIndex++;
            return true;
        }

        return false;
    }

    public void ResetOrbLevel() {
        AttackAbility.ResetAbilityLevel();
        _currLevelIndex = 0;
    }
}
