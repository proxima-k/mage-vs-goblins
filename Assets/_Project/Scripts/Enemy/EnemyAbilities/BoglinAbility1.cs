using System;
using System.Collections;
using AbilitySystem;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilitySystem/Boglin Attack 1")]
public class BoglinAbility1 : Ability {
    public Transform _projectilePf;
    
    public float _atkDuration = 8f;
    public float _atkPause = 0.15f;
    public int _atkDamage = 15;
    public int _atkProjectilePerPulse = 2;
    public float _rotateSpeed = 2f;
    public float _projectileSpeed = 10f;
    public LayerMask _damageLayers;
    
    private const float TAU = Mathf.PI * 2;

    public override IEnumerator TriggerAbility(Transform abilityCaster, Action callback = null) {
        Debug.Log("Ability 1");
        DangerMarkPopup.Create(abilityCaster.position);
        yield return new WaitForSeconds(1f);
        // abilityCaster.position += Vector3.right * 3f;
        
        // move to a location
        // start casting the ability
        
        float timer = _atkDuration;
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
                    AttackUtils.ShootProjectile(_projectilePf, _atkDamage, abilityCaster.position, dir, _projectileSpeed, _damageLayers, true);
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
