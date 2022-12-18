using System;
using System.Collections;
using UnityEngine;
using AbilitySystem;
using Proxima_K.Utils;

[CreateAssetMenu(menuName = "AbilitySystem/Shoot Projectile Ability")]
public class ShootProjectileAbility : Ability {
    // todo: think about the projectile script
    public Transform projectilePrefab; 
    public LayerMask collisionLayers;
    
    public Attribute<float> projectileSpeedAttr;
    private float projectileSpeed => projectileSpeedAttr.Value;
    public Attribute<int> projectileDamageAttr;
    private int projectileDamage=> projectileDamageAttr.Value;

    public override IEnumerator TriggerAbility(Transform abilityCaster, Action callback = null) {
        // canCastAbility = false;
        if (projectilePrefab == null)
            projectilePrefab = GameAssets.i.DefaultProjectilePrefab;
        
        Vector3 origin = abilityCaster.position;
        Vector2 targetDir = (PK.GetMouseWorldPosition2D(Camera.main) - (Vector2)origin).normalized;
        AttackUtils.ShootProjectile(projectilePrefab, projectileDamage, origin, targetDir, projectileSpeed, collisionLayers);

        yield return null;
        // yield return new WaitForSeconds(abilityCooldown);
        // canCastAbility = true;
    }

    public override void UpgradeAbility() {
        projectileSpeedAttr.NextLevel();
        projectileDamageAttr.NextLevel();
    }

    public override void ResetAbilityLevel() {
        projectileSpeedAttr.ResetLevel();
        projectileDamageAttr.ResetLevel();
    }

    public override string GetAttributesInfo() {
        return $"Damage: {projectileDamage} (+{projectileDamageAttr.GetNextLevelValue()-projectileDamage})\n" +
               $"Speed: {projectileSpeed} (+{projectileSpeedAttr.GetNextLevelValue()-projectileSpeed})";
    }
}
