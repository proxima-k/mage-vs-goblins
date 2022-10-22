using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    protected int _damage;
    protected LayerMask _collisionLayers = ~0;
    protected Transform _bodyTf;
    protected Rigidbody2D _rb2D;
    protected ParticleSystem _particleSystem;

    public event Action OnCollision;
    
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

        _particleSystem = _bodyTf.GetComponentInChildren<ParticleSystem>();
    }

    // make movement and collision setup separate
    public void Launch(Vector2 dir, float speed) {
        _rb2D.velocity = dir * speed;
    }

    public void DestroyProjectile(float timeToDestroy=0) {
        StartCoroutine(TriggerDestroy(timeToDestroy));
    }

    protected IEnumerator TriggerDestroy(float timeToDestroy) {
        yield return new WaitForSeconds(timeToDestroy);
        if (_particleSystem != null) {
            _particleSystem.transform.SetParent(null, false);
            var m = _particleSystem.main;
            m.stopAction = ParticleSystemStopAction.Destroy;
            _particleSystem.Stop();
        }
        Destroy(gameObject);
    }
    
    private bool hasHit;
    private void OnTriggerEnter2D(Collider2D col) {
        // if the collided object has IDamageable, then damage it
        if (_collisionLayers == (_collisionLayers | (1 << col.gameObject.layer))) {
            if (!hasHit && col.gameObject.TryGetComponent(out IDamageable healthDamageable)) {
                healthDamageable.Damage(_damage);
                hasHit = true;
            }
            OnCollision?.Invoke();
            // CinemachineShake.Instance.ScreenShake(5,0.1f);
            // DestroyProjectile();
        }
    }
}
