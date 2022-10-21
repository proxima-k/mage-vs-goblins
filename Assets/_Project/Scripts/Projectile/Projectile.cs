using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    protected int _damage;
    protected LayerMask _collisionLayers = ~0;
    protected Transform _bodyTf;
    protected Rigidbody2D _rb2D;
    
    public void Setup(Transform projectilePf, Vector2 origin, int damage, LayerMask collisionLayers) {
        _damage = damage;
        _collisionLayers = collisionLayers;
        transform.position = origin;
        
        // physics setup
        // if (!gameObject.TryGetComponent(out Rigidbody2D rb2D)) {
        _bodyTf = Instantiate(projectilePf, transform);
        _bodyTf.localPosition = Vector3.zero;
        
        _rb2D = gameObject.AddComponent<Rigidbody2D>();
        // }
        _rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _rb2D.isKinematic = true;
    }

    // make movement and collision setup separate
    public void Launch(Vector2 dir, float speed) {
        _rb2D.velocity = dir * speed;
    }

    protected bool hasHit;
    protected void OnTriggerEnter2D(Collider2D col) {
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
