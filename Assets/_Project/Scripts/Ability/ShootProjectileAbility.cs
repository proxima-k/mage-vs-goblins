using System;
using System.Collections;
using UnityEngine;
using AbilitySystem;
using Proxima_K.Utils;
using UnityEditor;

[CreateAssetMenu(menuName = "AbilitySystem/Shoot Projectile Ability")]
public class ShootProjectileAbility : Ability {
    // todo: think about the projectile script
    public Transform projectilePrefab; 
    public LayerMask collisionLayers;
    
    public Attribute<float> projectileSpeedAttr;
    private float projectileSpeed => projectileSpeedAttr.Value;
    public Attribute<int> projectileDamageAttr;
    private int projectileDamage=> projectileDamageAttr.Value;
    
    public override IEnumerator TriggerAbility(Transform abilityCaster) {
        if (projectilePrefab == null)
            projectilePrefab = GameAssets.i.DefaultProjectilePrefab;
        
        Vector3 origin = abilityCaster.position;
        Vector2 targetDir = (PK.GetMouseWorldPosition2D(Camera.main) - (Vector2)origin).normalized;
        AttackSystem.ShootProjectile(projectilePrefab, projectileDamage, origin, targetDir, projectileSpeed, collisionLayers);

        yield return null;
    }
}
