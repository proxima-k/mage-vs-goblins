using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {
    public event Action OnEnemyDeath;
    [SerializeField] protected int _maxHealth = 100; 
    protected HealthSystem _healthSystem;
    [SerializeField] protected Transform _targetTf;
    [SerializeField] protected int _currencyDrop;
    [SerializeField] protected bool _flipSprite;

    private Material _healthBarMat;

    // for state checks use
    protected Coroutine _currentRoutine;

    protected virtual void Awake() {
        _healthSystem = new HealthSystem(_maxHealth);
        _healthSystem.OnDamage += UpdateHealthBar;
        _healthSystem.OnDeath += () => {
            OnEnemyDeath?.Invoke();
            StartCoroutine(Death());
        };
        _healthBarMat = transform.Find("HealthBar").GetComponent<MeshRenderer>().material; // find child
        UpdateHealthBar();
    }

    public void Damage(int damageAmount) {
        _healthSystem.Damage(damageAmount);
        Debug.Log(_healthSystem.Health);
        DamagePopup.Create(damageAmount, transform.position + Vector3.up);
    }
    
    protected virtual IEnumerator Death() {
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
        
        if (_currencyDrop > 0)
            LootDropper.Instance.DropCurrency(_currencyDrop, transform.position);
        // unsure if this return is needed
        yield return null;
        Destroy(gameObject);
    }

    public virtual void SetTarget(Transform targetTf) {
        _targetTf = targetTf;
    }

    public Transform GetTarget() {
        return _targetTf;
    }
    

    private void UpdateHealthBar() {
        _healthBarMat.SetFloat("_Health", _healthSystem.HealthPercentage);
    }
}
