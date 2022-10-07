using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ProjectileOrb")]
public class ProjectileOrb : Orb {
    public Transform projectileTf;
    public float projectileSpeed = 30f;
    public LayerMask collisionLayers;

    public override void Shoot(Vector3 origin, Vector3 targetDir) {
        AttackSystem.ShootProjectile(projectileTf, damage, origin, targetDir,projectileSpeed, collisionLayers);
    }
}
