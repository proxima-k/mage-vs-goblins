using System.Collections;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer.Operations;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RayOrb")]

public class RayOrb : Orb {
    // public Sprite raySprite;
    public Transform rayTf;
    public float rayThickness = 0.5f;
    public float rayDistance = 16f;
    public float damagePauseDuration = 0.5f;
    public int damageTimes = 3;

    public override IEnumerator Shoot(Vector3 origin, Vector3 targetDir) {
        // todo: will need to get mouse position and player position to make the ray follow player
        
        Transform rayInstance = Instantiate(rayTf, origin, Quaternion.identity);
        rayInstance.right = targetDir;
        SpriteRenderer raySprite = rayInstance.GetComponent<SpriteRenderer>();
        raySprite.size = new Vector2(rayDistance, rayThickness*2);
        
        for (int i = 0; i < damageTimes; i++) {
            AttackSystem.ShootRay(damage, origin, targetDir, rayThickness, rayDistance, debugRay:true);
            yield return new WaitForSeconds(damagePauseDuration);
        }
        
        Destroy(rayInstance.gameObject);
    }
}
