using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealthDamageable {
    [SerializeField] private int _maxHealth = 100; 
    private HealthSystem _healthSystem;

    public void Awake() {
        _healthSystem = new HealthSystem(_maxHealth);
        _healthSystem.OnDeath += Death;
    }

    public void Damage(int damageAmount) {
        _healthSystem.Damage(damageAmount);
        Debug.Log(_healthSystem.Health);
    }

    
    public void Death() {
        // change this to IEnumerator if implementing a bunch of animation stuff that requires timing
        Debug.Log("Enemy has died");
        Destroy(gameObject);
    }
}
