using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Player : MonoBehaviour, IShopCustomer, IDamageable, IPickuper {
    [SerializeField] private CurrencySystem _currencySystem;
    private HealthSystem _healthSystem;

    [SerializeField] private int _startingCurrency = 100;
    [SerializeField] private int _maxHealth = 100;

    [SerializeField] private Image _healthBar;
    private Material _healthBarMat;
    [SerializeField] private TextMeshProUGUI _healthText;
    
    private void Awake() {
        _healthBarMat = _healthBar.material;
        _currencySystem = new CurrencySystem(_startingCurrency);
        _healthSystem = new HealthSystem(_maxHealth);
        _healthSystem.OnDamage += UpdateHealthBar;
        // trigger game over scene
        // _healthSystem.OnDeath += () => { };
        UpdateHealthBar();
    }

    public CurrencySystem GetCurrencySystem() {
        return _currencySystem;
    }

    public void Damage(int damageAmount) {
        _healthSystem.Damage(damageAmount);
        DamagePopup.Create(damageAmount, transform.position + Vector3.up);
    }

    public void PickupCurrency(int amount) {
        _currencySystem.Earn(amount);
    }

    private void UpdateHealthBar() {
        _healthText.text = _healthSystem.Health.ToString();
        _healthBarMat.SetFloat("_Health", _healthSystem.HealthPercentage);
    }
    
}
