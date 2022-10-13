using System.Collections;
using System.Collections.Generic;
using AbilitySystem;
using Microsoft.Win32.SafeHandles;
using Proxima_K.Utils;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilitySystem/Shoot Ray Ability")]
public class ShootRayAbility : Ability {
    // public Sprite raySprite;
    public Transform rayTf;
    // public Attribute<float> rayThickness = new Attribute<float>(new List<float>{0.5f});
    public float rayThickness = 0.5f;
    public float rayDistance = 16f;

    public int damage = 10;
    public float damagePauseDuration = 0.5f;
    public int damageTimes = 3;

    public override IEnumerator TriggerAbility(Transform abilityCaster) {
        // todo: will need to get mouse position and player position to make the ray follow player
        Vector3 origin = abilityCaster.position;
        
        // create a while loop here
        Vector3 targetDir = ((Vector3) PK.GetMouseWorldPosition2D(Camera.main) - origin).normalized;
        
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
