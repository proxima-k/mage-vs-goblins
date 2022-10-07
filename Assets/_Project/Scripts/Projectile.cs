using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    // setup variables
        // collision layers
        // has a height (composition)
        // projectile type?
    //
    
    private int _damage;
    private LayerMask _collisionLayers = ~0;
        
    public void SetDamage(int amount) {
        _damage = amount;
    }

    public void SetCollisionLayers(LayerMask collisionLayers) {
        _collisionLayers = collisionLayers;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        // if the collided object has IDamageable, then damage it
        if (col.gameObject.TryGetComponent(out IHealthDamageable healthDamageable))
            healthDamageable.Damage(_damage);
        Destroy(gameObject);
    }
}
