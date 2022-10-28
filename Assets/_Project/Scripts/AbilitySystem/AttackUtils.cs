using Proxima_K.Utils;
using UnityEngine;

public class AttackUtils {
    public static void ShootProjectile(Transform projectileTf, int damage, Vector3 origin, Vector3 targetDir, float projectileSpeed, LayerMask? collisionLayers=null, bool rotateProjectile = false) {
        if (collisionLayers == null)
            collisionLayers = ~0;
        
        // Transform fireballInstance = Object.Instantiate(projectileTf, origin, Quaternion.identity);
        GameObject fireballInstance = new GameObject(projectileTf.name);
        if (rotateProjectile)
            fireballInstance.transform.right = targetDir;
        // projectile properties setup
        // Projectile projectile = fireballInstance.gameObject.AddComponent<Projectile>();
        Projectile projectile = fireballInstance.AddComponent<Projectile>();
        projectile.Setup(projectileTf, origin, damage, (LayerMask)collisionLayers);
        projectile.OnCollision += () => projectile.DestroyProjectile();
        // projectile.Launch(PK.GetMouseWorldPosition2D(Camera.main), 0.7f);
        projectile.Launch(targetDir, projectileSpeed);
        
        // Object.Destroy(projectile.gameObject,5f);
        projectile.DestroyProjectile(5f);
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
            if(hit.transform.TryGetComponent(out IDamageable damageable))
                damageable.Damage(damage);
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

    public static void CastAOE(int damage, Vector3 origin, float aoeRadius, LayerMask? collisionLayers=null) {
        if (collisionLayers == null)
            collisionLayers = ~0;

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(origin, aoeRadius, (LayerMask)collisionLayers);
        foreach (var collider2D in collider2Ds) {
            if (collider2D.transform.TryGetComponent(out IDamageable damageable)) {
                damageable.Damage(damage);
            }
        }
    }
}
