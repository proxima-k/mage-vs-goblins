using AbilitySystem;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Orb")]
public class Orb : ScriptableObject {

    public string OrbName;
    public Sprite Icon;
    public string Description;
    
    public Ability AttackAbility;
    public Ability UltimateAbility;

    [SerializeField] private int[] UpgradeCosts = new int[4];
    private int _currLevelIndex = 0;
    // private bool _isUnlocked = false;

    public int Level => _currLevelIndex;
    
    // Should be used together with UpgradeOrb()
    public bool CanUpgrade() {
        return _currLevelIndex < UpgradeCosts.Length;
    }
    
    public bool UpgradeOrb(CurrencySystem currencySystem) {
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
    
    public string GetOrbInfo() {
        string info =
            Description + "\n" +
            AttackAbility.GetAttributesInfo() + "\n" +
            UltimateAbility.GetAttributesInfo() + "\n";
        if (_currLevelIndex == UpgradeCosts.Length)
            info += "Max Level";
        else {
            info += $"Upgrade Cost: {UpgradeCosts[_currLevelIndex]}";
        }

        return info;
    }
}
