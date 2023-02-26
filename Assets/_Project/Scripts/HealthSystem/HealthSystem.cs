using System;
using UnityEngine;

public class HealthSystem {
    public int Health => _health;
    public int MaxHealth => _maxHealth;
    public float HealthPercentage => (float)_health / _maxHealth;
    
    private int _health;
    private int _maxHealth;

    public event Action OnDeath;
    public event Action<int> OnDamage;
    
    public HealthSystem(int health, int? maxHealth = null) {
        _health = health;
        if (maxHealth != null) 
            _maxHealth = (int)maxHealth;
        else
            _maxHealth = health;
    }

    public void Heal(int amount) {
        _health += Mathf.Abs(amount);
        if (_health > _maxHealth) _health = _maxHealth;
    }

    public void Damage(int amount) {
        _health -= Mathf.Abs(amount);
        OnDamage?.Invoke(amount);
        if (_health <= 0) {
            _health = 0;
            OnDamage?.Invoke(amount);
            OnDeath?.Invoke();
        }
    }
    
    // public void UpgradeMaxHealth(int maxHealth) {
    //     _maxHealth = maxHealth;
    // }
}
