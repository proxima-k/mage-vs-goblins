using System;
using System.Collections;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour {
    [SerializeField] private Transform _playerTf;
    [SerializeField] private Rect spawnField = new Rect(Vector2.zero, Vector2.one);
    // array of waves
    // each wave i get to choose which enemy type I wanna spawn and how many of them
    [SerializeField] private Transform[] _enemyTypes;

    [Header("Level Layout")]
    [SerializeField] private Level[] _levels;
    private Coroutine _gameCoroutine;
    private int _currentEnemyCount = 0;

    private void Start() {
        _gameCoroutine = StartCoroutine(StartGame());
    }

    public IEnumerator StartGame() {
        // level iteration
        foreach (var level in _levels) {
            while (_currentEnemyCount > 0) {
                yield return null;
            }
            foreach (var wave in level.Waves) {
                    // checking wave type
                    if (wave.WaitForPreviousEnemiesAreDead)
                        while (_currentEnemyCount > 0) { yield return null; }
                    
                    yield return new WaitForSeconds(wave.SpawnAfterSeconds);
                    
                    // spawning enemies after the wait type has ended
                    SpawnEnemies(_enemyTypes[wave.EnemyIndex], wave.SpawnAmount);
            }
        }

        yield return null;
    }

    public void StopWave() {
        StopCoroutine(_gameCoroutine);
    }
    
    public void SpawnEnemies(Transform enemyType, int enemyCount) {
        for (int i = 0; i < enemyCount; i++) {
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnField.size.x / -2, spawnField.size.x / 2),
                Random.Range(spawnField.size.y / -2, spawnField.size.y / 2));
            Enemy newEnemy = Instantiate(enemyType, spawnPos, Quaternion.identity).GetComponent<Enemy>();
            newEnemy.SetTarget(_playerTf);
            _currentEnemyCount++;
            newEnemy.OnEnemyDeath += EnemyDied;
        }
    }

    private void EnemyDied() {
        _currentEnemyCount--;
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(Vector2.zero, spawnField.size);
    }
}

[Serializable]
public class Level {
    [SerializeField] private string LevelLabel;
    [SerializeField] private EnemyWave[] _waves;
    
    public EnemyWave[] Waves => _waves;
}

[Serializable]
public class EnemyWave {

    [SerializeField] private bool _waitForPreviousEnemiesAreDead;
    [SerializeField] private int _enemyIndex;
    [SerializeField] private float _spawnAfterSeconds;
    [SerializeField] private int _spawnAmount;

    public bool WaitForPreviousEnemiesAreDead => _waitForPreviousEnemiesAreDead;
    public int EnemyIndex => _enemyIndex;
    public float SpawnAfterSeconds => _spawnAfterSeconds;
    public int SpawnAmount => _spawnAmount;
}
