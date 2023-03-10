using System;
using System.Collections;
using AbilitySystem;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilitySystem/Boglin Attack 2")]
public class BoglinAbility2 : Ability {
    public Transform _projectilePf;
    // phase 1
    public int _atkDmg1 = 15;
    public float _projSpeed1 = 15f;
    
    // phase 2 
    public int _atkDmg2 = 10;
    public float _projSpeed2 = 12.5f;
    [Range(0f,Mathf.PI)]
    public float spread = 0.57f;
    public int _projCount = 5;

    public LayerMask _damageLayers;

    // private const float TAU = Mathf.PI * 2;
    
    public override IEnumerator TriggerAbility(Transform abilityCaster, Action callback = null) {
        // Debug.Log("Ability 2");
        DangerMarkPopup.Create(abilityCaster.position);
        Transform targetTf = abilityCaster.GetComponent<Enemy>().GetTarget();
        yield return new WaitForSeconds(1f);
        
        // PHASE 1: launch 2 individual projectile
        Vector3 targetDir = (targetTf.position - abilityCaster.position).normalized;
        AttackUtils.ShootProjectile(_projectilePf, _atkDmg1, abilityCaster.position, targetDir, _projSpeed1, _damageLayers, true);
        yield return new WaitForSeconds(.3f);
        targetDir = (targetTf.position - abilityCaster.position).normalized;
        AttackUtils.ShootProjectile(_projectilePf, _atkDmg1, abilityCaster.position, targetDir, _projSpeed1, _damageLayers, true);
        yield return new WaitForSeconds(.5f);
        
        // PHASE 2: launch a single attack with the assigned number of projectile
        targetDir = (targetTf.position - abilityCaster.position).normalized;
        float rad = Mathf.Atan2(targetDir.y, targetDir.x) - spread;
        
        float radTest = Mathf.Atan2(targetDir.y, targetDir.x);
        Vector3 targetPos = new Vector3(Mathf.Cos(radTest), Mathf.Sin(radTest));
        Vector3 targetPos1 = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
        Debug.Log(targetPos);
        Debug.DrawLine(abilityCaster.position, targetPos*10f+abilityCaster.position, Color.cyan, 10f);
        Debug.DrawLine(abilityCaster.position, targetPos1*10f+abilityCaster.position, Color.cyan, 10f);
        
        
        for (int i = 0; i < _projCount; i++) {
            targetDir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
            AttackUtils.ShootProjectile(_projectilePf, _atkDmg2, abilityCaster.position, targetDir, _projSpeed2, _damageLayers, true);

            // (projectile count - 1) means the number of intervals between shooting directions
            rad += spread * 2 / (_projCount-1);
        }
        // todo: add camera shake
        
        yield return null;
        callback?.Invoke();
    }
}
