using System;
using UnityEngine;

[Serializable]
public class CurrencySystem {
    public int Currency => _currency;
    
    [SerializeField]private int _currency;

    public event Action OnCurrencySpend;
    
    public CurrencySystem(int currency = 0) {
        _currency = currency;
    }

    public void Earn(int amount) {
        _currency += Mathf.Abs(amount);
    }

    public bool Spend(int amount) {
        // not enough
        if (_currency - amount < 0) 
            return false;
        
        // enough currency
        _currency -= amount;
        return true;
    }
}
