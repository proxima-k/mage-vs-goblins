using System.Collections;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer.Operations;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RayOrb")]

public class RayOrb : Orb {
    // public Sprite raySprite;
    public float rayThickness = 0.5f;
    public float rayDistance = 16f;

    public override IEnumerator Shoot(Vector3 origin, Vector3 targetDir) {
        // Sprite rayTf = Instantiate(raySprite, origin, Quaternion.identity) ;
        yield return new WaitForSeconds(0.7f);
        AttackSystem.ShootRay(damage, origin, targetDir, rayThickness, rayDistance, debugRay:true);
        // Destroy(rayTf);
    }
}
