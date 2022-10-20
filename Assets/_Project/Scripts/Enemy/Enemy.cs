using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {
    [SerializeField] private int _maxHealth = 100; 
    private HealthSystem _healthSystem;
    // private EnemyAI _ai;
    
    public void Awake() {
        _healthSystem = new HealthSystem(_maxHealth);
        _healthSystem.OnDeath += () => { StartCoroutine(Death()); };
    }

    public void Start() {
        // _ai = GetComponent<EnemyAI>();
    }
    
    public void Damage(int damageAmount) {
        _healthSystem.Damage(damageAmount);
        Debug.Log(_healthSystem.Health);
        DamagePopup.Create(damageAmount, transform.position + Vector3.up);
    }
    
    public IEnumerator Death() {
        // change this to IEnumerator if implementing a bunch of animation stuff that requires timing
        // blink for a few times
        // _ai.SetTarget(null);
        // disable colliders
        GetComponent<Collider2D>().enabled = false;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float blinkPeriod = 0.05f;
        
        for (int i = 0; i < 3; i++) {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkPeriod);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkPeriod);
        }
        
        LootDropper.Instance.DropCurrency(2, transform.position);
        Destroy(gameObject);
    }
}
