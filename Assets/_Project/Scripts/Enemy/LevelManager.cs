using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class EnemySpawner : MonoBehaviour {
    [SerializeField] private Transform _playerTf;
    [SerializeField] private Rect spawnField = new Rect(Vector2.zero, Vector2.one);
    // array of waves
    // each wave i get to choose which enemy type I wanna spawn and how many of them
    [SerializeField] private Transform[] _enemyTypes;

    [Header("Level Layout")]
    [SerializeField] private Level[] _levels;
    private Coroutine _currentWave;
    private int _currentWaveEnemyCount = 0;
    private int _currentLevel = -1;


    public IEnumerator StartGame() {
        _currentLevel++;
        if (_currentWaveEnemyCount <= 0) {
            foreach (var wave in _levels[_currentLevel].Waves) {
                if (wave.StartWaveType == EnemyWave.WaitType.Time) {
                    yield return new WaitForSeconds(wave.SpawnAfterSeconds);
                    
                }
                else {
                    
                }
            }
            // iterate through all waves
            // wait for seconds if required
        }

        yield return null;
    }
    
    public void NextWave() {
        if (_currentWaveEnemyCount <= 0) {
            // spawn next wave
        }
    }

    public void StopWave() {
        StopCoroutine(_currentWave);
    }

    private void EnemyDied() {
        _currentWaveEnemyCount--;
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(Vector2.zero, spawnField.size);
    }
}

[Serializable]
public class Level {
    [SerializeField] private EnemyWave[] _waves;
    public EnemyWave[] Waves => _waves;
}

[Serializable]
public class EnemyWave {
    public enum WaitType {
        Time,
        AllPreviousEnemyDead
    }

    [SerializeField] private WaitType _startWaveType;
    [SerializeField] private int _enemyIndex;
    [SerializeField] private float _spawnAfterSeconds;
    [SerializeField] private int _spawnAmount;

    public WaitType StartWaveType => _startWaveType;
    public int EnemyIndex => _enemyIndex;
    public float SpawnAfterSeconds => _spawnAfterSeconds;
    public int SpawnAmount => _spawnAmount;

    public IEnumerator StartWave() {
        
    }
}
