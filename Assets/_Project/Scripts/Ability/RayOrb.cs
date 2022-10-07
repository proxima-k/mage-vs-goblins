using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RayOrb")]

public class RayOrb : Orb {
    public float rayThickness = 0.5f;
    public float rayDistance = 16f;

    public override void Shoot(Vector3 origin, Vector3 targetDir) {
        AttackSystem.ShootRay(damage, origin, targetDir, rayThickness, rayDistance, debugRay:true);
    }
}
