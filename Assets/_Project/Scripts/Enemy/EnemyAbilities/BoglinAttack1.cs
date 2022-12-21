using System;
using System.Collections;
using AbilitySystem;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilitySystem/Boglin Attack 1")]
public class BoglinAttack1 : Ability {
    public Transform _projectilePf;
    
    public float _atkDuration = 10f;
    public float _atkPause = .3f;
    public int _atkDamage = 15; 
    public int _atkProjectilePerPulse = 2;
    public float _rotateSpeed = 10f;
    public LayerMask _damageLayers;
    
    private const float TAU = Mathf.PI * 2;

    public override IEnumerator TriggerAbility(Transform abilityCaster, Action callback = null) {
        Debug.Log("Attack 1");
        float timer = _atkDuration;
        // abilityCaster.position += Vector3.right * 3f;
        
        // move to a location
        // start casting the ability
        
        float pauseTimer = 0;
        float rad = 0;
        float _desiredRad = 0;
        Vector3 dir = Vector3.zero;
        while (timer > 0) {
            // loop through fire count, and shoot projectile
            if (pauseTimer <= 0) {
                for (int i = 0; i < _atkProjectilePerPulse; i++) {
                    _desiredRad = rad + TAU * i / _atkProjectilePerPulse;
                    dir = new Vector3(Mathf.Cos(_desiredRad), Mathf.Sin(_desiredRad));
                    AttackUtils.ShootProjectile(_projectilePf, _atkDamage, abilityCaster.position, dir, 5f, _damageLayers, true);
                }

                pauseTimer = _atkPause;
            }
            else {
                pauseTimer -= Time.deltaTime;
            }

            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
            rad += Time.deltaTime * _rotateSpeed;
        }
        yield return new WaitForSeconds(_atkPause);
        callback?.Invoke();
    }
}
