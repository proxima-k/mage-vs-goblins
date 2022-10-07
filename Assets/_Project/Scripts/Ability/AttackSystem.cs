using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class AttackSystem {
    public static void ShootProjectile(Transform projectileTf, int damage, Vector3 origin, Vector3 targetDir, float projectileSpeed, LayerMask? collisionLayers=null) {
        if (collisionLayers == null)
            collisionLayers = ~0;
        
        Transform fireballInstance = Object.Instantiate(projectileTf, origin, Quaternion.identity);

        // projectile properties setup
        Projectile projectile = fireballInstance.gameObject.AddComponent<Projectile>();
        projectile.SetDamage(damage);
        projectile.SetCollisionLayers((LayerMask)collisionLayers);
        
        // projectile physics setup
        Rigidbody2D rb2D = fireballInstance.gameObject.AddComponent<Rigidbody2D>();
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2D.gravityScale = 0;

        // add velocity/force towards direction
        rb2D.velocity = targetDir * projectileSpeed;
    }

    public static void ShootRay(int damage, Vector3 origin, Vector3 targetDir, float rayThickness, float rayDistance, LayerMask? collisionLayers=null, bool debugRay=false) {
        if (collisionLayers==null)
            collisionLayers = ~0;
    
        // Gets direction and angle for box cast
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        // To cast box from the left side instead of center
        Vector3 offsetOrigin = origin + targetDir;
        
        RaycastHit2D[] hits = Physics2D.BoxCastAll(offsetOrigin, new Vector2(1,rayThickness), angle, targetDir, rayDistance-1, (int) collisionLayers);
        
        foreach (var hit in hits) {
            if(hit.transform.TryGetComponent(out IHealthDamageable healthDamageable))
                healthDamageable.Damage(damage);
        }

        if (debugRay) {
            Debug.DrawLine(offsetOrigin, offsetOrigin+targetDir*(rayDistance-1), Color.green, 5f);
        }
    }
}

