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
    public float rayDuration = 3f;

    public int damage = 10;
    public float timeTillNextPulse = 0.5f;

    public override IEnumerator TriggerAbility(Transform abilityCaster) {
        float timer = rayDuration;
        float damageTimer = 0;
        
        Transform rayInstance = Instantiate(rayTf, abilityCaster.position, Quaternion.identity);
        SpriteRenderer raySprite = rayInstance.GetComponent<SpriteRenderer>();
        raySprite.size = new Vector2(rayDistance, rayThickness*2);
        
        while (timer > 0) {
            // add interpolation for rotating direction
            Vector3 targetDir = ((Vector3) PK.GetMouseWorldPosition2D(Camera.main) - abilityCaster.position).normalized;
            rayInstance.position = abilityCaster.position;
            rayInstance.right = targetDir;
            
            if (damageTimer <= 0) {
                AttackSystem.ShootRay(damage, abilityCaster.position, targetDir, rayThickness, rayDistance, debugRay:true);
                damageTimer = timeTillNextPulse;
            }
            else {
                damageTimer -= Time.deltaTime;
            }
            
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // for (int i = 0; i < damageTimes; i++) {
        //     yield return new WaitForSeconds(damagePauseDuration);
        // }
        
        Destroy(rayInstance.gameObject);
    }
}
