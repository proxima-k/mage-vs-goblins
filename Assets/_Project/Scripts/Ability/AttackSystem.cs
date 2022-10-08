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
        rb2D.isKinematic = true;
        // rb2D.gravityScale = 0;

        // add velocity/force towards direction
        rb2D.velocity = targetDir * projectileSpeed;
        
        Object.Destroy(fireballInstance.gameObject,5f);
    }

    public static void ShootRay(int damage, Vector3 origin, Vector3 targetDir, float rayThickness, float rayDistance, LayerMask? collisionLayers=null, bool debugRay=false) {
        if (collisionLayers == null)
            collisionLayers = ~0;
    
        // Gets direction and angle for box cast
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        // To cast box from the left side instead of center
        float boxLength = 1;
        Vector3 offsetOrigin = origin + targetDir*(boxLength*0.5f);
        
        RaycastHit2D[] hits = Physics2D.BoxCastAll(offsetOrigin, new Vector2(boxLength,rayThickness*2), angle, targetDir, rayDistance-boxLength, (int) collisionLayers);
        
        foreach (var hit in hits) {
            if(hit.transform.TryGetComponent(out IDamageable healthDamageable))
                healthDamageable.Damage(damage);
        }

        if (debugRay) {
            Matrix4x4 matrix4X4 = Matrix4x4.Rotate(Quaternion.Euler(0,0,angle));
            Vector3 upperCorner = matrix4X4 * Vector3.up * rayThickness;
            Vector3 lowerCorner = matrix4X4 * Vector3.down * rayThickness;
            upperCorner += origin;
            lowerCorner += origin;

            Vector3 upperEnd = upperCorner + targetDir * rayDistance;
            Vector3 lowerEnd = lowerCorner + targetDir * rayDistance;
            
            Debug.DrawLine(upperCorner, upperEnd, Color.green, 5f);
            Debug.DrawLine(lowerCorner, lowerEnd, Color.green, 5f);
            Debug.DrawLine(upperEnd, lowerEnd, Color.red, 5f);
        }
    }
}

