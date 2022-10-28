using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem;
using Microsoft.Win32.SafeHandles;
using Proxima_K.Utils;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilitySystem/Shoot Ray Ability")]
public class ShootRayAbility : Ability {
    public Transform rayTf;
    public LayerMask damageLayers;
    public float rayDistance = 16f;
    public float rayDuration = 3f;
    public Attribute<float> rayWidth = new Attribute<float>("Ray Width",new List<float>{0.5f});

    public Attribute<int> damage = new Attribute<int>("Damage", new List<int>{5});
    public Attribute<float> timeTillNextPulse = new Attribute<float>("Pulse interval",new List<float>{0.5f});

    public override IEnumerator TriggerAbility(Transform abilityCaster) {
        float timer = rayDuration;
        float damageTimer = 0;
        PlayerLocomotion playerLocomotion = abilityCaster.GetComponent<PlayerLocomotion>();
        float initialSpeed = playerLocomotion.MoveSpeed;
        playerLocomotion.UpdateSpeed(initialSpeed*0.3f);
        playerLocomotion.SetDash(false);

        Transform rayInstance = Instantiate(rayTf, abilityCaster.position, Quaternion.identity);
        SpriteRenderer raySprite = rayInstance.GetComponent<SpriteRenderer>();
        raySprite.size = new Vector2(rayDistance, rayWidth.Value*2);
        Vector3 targetDir = ((Vector3) PK.GetMouseWorldPosition2D(Camera.main) - abilityCaster.position).normalized;
        rayInstance.right = targetDir;
        
        while (timer > 0) {
            // add interpolation for rotating direction
            targetDir = ((Vector3) PK.GetMouseWorldPosition2D(Camera.main) - abilityCaster.position).normalized;
            targetDir = Vector3.Lerp(rayInstance.right, targetDir, 0.02f);
            rayInstance.position = abilityCaster.position;
            rayInstance.right = targetDir;
            
            if (damageTimer <= 0) {
                AttackUtils.ShootRay(damage.Value, abilityCaster.position, targetDir, rayWidth.Value, rayDistance, debugRay:true, collisionLayers:damageLayers);
                damageTimer = timeTillNextPulse.Value;
            }
            else {
                damageTimer -= Time.deltaTime;
            }
            
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        playerLocomotion.UpdateSpeed(initialSpeed);
        playerLocomotion.SetDash(true);
        Destroy(rayInstance.gameObject);
    }

    public override void UpgradeAbility() {
        rayWidth.NextLevel();
        damage.NextLevel();
        timeTillNextPulse.NextLevel();
    }

    public override void ResetAbilityLevel() {
        rayWidth.ResetLevel();
        damage.ResetLevel();
        timeTillNextPulse.ResetLevel();
    }
}
