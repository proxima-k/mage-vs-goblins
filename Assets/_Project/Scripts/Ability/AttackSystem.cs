using UnityEditor;
using UnityEngine;

public class AttackSystem {
    public static void ShootProjectile(Transform projectileTf, Vector3 origin, Vector3 targetDir, float projectileSpeed) {
        Transform fireballInstance = Object.Instantiate(projectileTf, origin, Quaternion.identity);
        
        // perhaps have a projectile script for collision layers
        
        // add component of rigidbody if it doesn't have one
        Rigidbody2D rb2D = fireballInstance.gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        // rb2D.bodyType = RigidbodyType2D.Kinematic;
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2D.gravityScale = 0;

        // add velocity/force towards direction
        rb2D.velocity = targetDir * projectileSpeed;
    }

    public static void ShootRay(Vector3 origin, Vector3 targetDir, float rayThickness, float rayDistance, LayerMask collisionLayers, bool debugRay=false) {
        // Gets direction and angle for box cast
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        // To cast box from the left side instead of center
        Vector3 offsetOrigin = origin + targetDir;
        
        RaycastHit2D[] hits = Physics2D.BoxCastAll(offsetOrigin, new Vector2(1,rayThickness), angle, targetDir, rayDistance-1);
        
        foreach (var hit in hits) {
            if(hit.transform.TryGetComponent(out IHealthDamageable healthDamageable))
                healthDamageable.Damage(5);
        }

        if (debugRay) {
            Debug.DrawLine(offsetOrigin, offsetOrigin+targetDir*(rayDistance-1), Color.green, 5f);
        }
    }
}

