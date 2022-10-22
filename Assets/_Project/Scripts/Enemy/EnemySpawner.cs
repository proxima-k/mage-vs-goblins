using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class EnemySpawner : MonoBehaviour {
    [SerializeField] private Transform _playerTf;
    [SerializeField] private int _enemySpawns = 5;
    [SerializeField] private Transform _enemyTf;
    [SerializeField] private Rect spawnField = new Rect(Vector2.zero, Vector2.one);
    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            for (int i = 0; i < _enemySpawns; i++) {
                Vector2 spawnPos = new Vector2(Random.Range(-spawnField.width/2, spawnField.width/2),Random.Range(-spawnField.height/2, spawnField.height/2));
                EnemyAI enemy = Instantiate(_enemyTf, spawnPos, Quaternion.identity).GetComponent<EnemyAI>();
                enemy.SetTarget(_playerTf);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(Vector2.zero, spawnField.size);
    }
}