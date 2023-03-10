using System;
using System.Collections;
using AbilitySystem;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "AbilitySystem/Boglin Ability 3")]
public class BoglinAbility3 : Ability {
    public EnemyTypeToSummon[] _enemyTypesToSummon;
    public float _spawnRadius;

    private const float TAU = Mathf.PI * 2;
    
    public override IEnumerator TriggerAbility(Transform abilityCaster, Action callback = null) {
        Transform target = abilityCaster.GetComponent<Enemy>().GetTarget();

        // summoning mobs to distract player
        float radius, radian;
        Vector3 summonPos;
        foreach (var enemyType in _enemyTypesToSummon) {
            int lowerRange = enemyType.SummonAmountRange.x;
            int upperRange = enemyType.SummonAmountRange.y;
            for (int i = 0; i < Random.Range(lowerRange, upperRange+1); i++) {
                radian = Random.Range(0, TAU);
                radius = Random.Range(0, _spawnRadius);
                // todo: check for out of map range
                summonPos = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian))*radius + abilityCaster.position;
                
                Enemy enemy = Instantiate(enemyType.EnemyTypeTf, summonPos, Quaternion.identity).GetComponent<Enemy>();
                enemy.SetTarget(target);

                yield return new WaitForSeconds(0.5f);
            }
        }
        
        // after summoning, wait for an amount of time before engaging in attack again
        yield return new WaitForSeconds(6f);
        callback?.Invoke();
    }

}
[Serializable]
public class EnemyTypeToSummon {
    [SerializeField] private Transform _enemyTypeTf;
    [SerializeField] private Vector2Int _summonAmountRange;
    public Transform EnemyTypeTf => _enemyTypeTf;
    public Vector2Int SummonAmountRange => _summonAmountRange;
}