using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IShopCustomer {
    [SerializeField] private CurrencySystem _currencySystem;
    private HealthSystem _healthSystem;

    [SerializeField] private int _startingCurrency = 100;
    [SerializeField] private int _maxHealth = 100;

    private void Awake() {
        _currencySystem = new CurrencySystem(_startingCurrency);
        _healthSystem = new HealthSystem(_maxHealth);
    }

    public CurrencySystem GetCurrencySystem() {
        return _currencySystem;
    }
}
