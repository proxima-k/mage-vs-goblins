using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ShootProjectileAbility")]
public class ShootProjectileAbility : Ability {
    public Transform Projectile;

    public override void CastAbility() {
        Instantiate(Projectile, Vector2.zero, Quaternion.identity);
        // Projectile.gameObject.AddComponent<Projectile>();
        // get target direction
        // launch projectile in the target direction
    }
}
