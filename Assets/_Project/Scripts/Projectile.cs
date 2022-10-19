using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    // setup variables
        // has a height (composition)
        // projectile type?
    
    private int _damage;
    private LayerMask _collisionLayers = ~0;

    public void SetDamage(int amount) {
        _damage = amount;
    }

    public void SetCollisionLayers(LayerMask collisionLayers) {
        _collisionLayers = collisionLayers;
    }

    private bool hasHit;
    private void OnTriggerEnter2D(Collider2D col) {
        // if the collided object has IDamageable, then damage it
        if (_collisionLayers == (_collisionLayers | (1 << col.gameObject.layer))) {
            if (!hasHit && col.gameObject.TryGetComponent(out IDamageable healthDamageable)) {
                healthDamageable.Damage(_damage);
                hasHit = true;
            }
            CinemachineShake.Instance.ScreenShake(5,0.1f);
            Destroy(gameObject);
        }
    }
}
