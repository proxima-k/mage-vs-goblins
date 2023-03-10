using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "AbilitySystem/Fireball Ultimate Ability")]
public class FireballUltimate : Ability {
    public Transform projectileTf;
    public float timeToReach = 1f;
    public float radius = 2f;
    public int damage = 10;
    public float damageRadius = 5;
    public int fireballCount = 6;
    public LayerMask damageLayers;
    
    public override IEnumerator TriggerAbility(Transform abilityCaster, Action callback = null) {
        Vector3 targetPos = Vector3.zero;
        Vector3 origin = abilityCaster.position;
        
        List<HeightProjectile> projectiles = new List<HeightProjectile>();
        float TAU = Mathf.PI * 2;
        
        // setting up projectiles for launching
        for (int i = 0; i < fireballCount; i++) {
            GameObject fireballInstance = new GameObject(projectileTf.name);
            HeightProjectile heightProjectile = fireballInstance.AddComponent<HeightProjectile>();
            heightProjectile.Setup(projectileTf, abilityCaster.position, 10, 0);
            heightProjectile.OnGroundHit += () => {
                Transform explosionInstance = Instantiate(GameAssets.i.FlashAnimationPrefab, heightProjectile.transform.position, Quaternion.Euler(0,0,Random.Range(0, 90))).transform;
                explosionInstance.localScale *= damageRadius*2;
                CinemachineShake.Instance.ScreenShake(5, 0.2f);
                heightProjectile.DestroyProjectile();
                AttackUtils.CastAOE(damage, heightProjectile.transform.position, damageRadius, damageLayers);
            };
            projectiles.Add(heightProjectile);
        }

        // launch them to the air (caster's position)
        foreach (var projectile in projectiles) {
            projectile.Launch(abilityCaster.position, timeToReach);
        }
        yield return new WaitForSeconds(timeToReach / 2);

        // launch again to selected location foreach projectile
        int angleCounter = 0;
        foreach (var projectile in projectiles) {
            float rad = TAU * angleCounter / fireballCount;
            targetPos = origin + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))*radius;
            
            // todo: give this a random time offset
            projectile.Launch(targetPos, timeToReach / 2);
            angleCounter++;
        }

        yield return null;
    }
}
