using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour {
    [Header("UI stuff")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameObject InstructionUI;
    [SerializeField] private GameObject Shop;
    
    [SerializeField] private Transform _playerTf;
    [SerializeField] private Rect spawnField = new Rect(Vector2.zero, Vector2.one);
    [SerializeField] private Transform[] _enemyTypes;

    [Header("Level Layout")]
    [SerializeField] private Level[] _levels;
    private Coroutine _gameCoroutine;
    private int _currentEnemyCount = 0;

    private bool _gameHasStarted;

    private void Start() {
        GameStateUIManager.I.OnPlayerWin += StopGame;
        _levelText.text = "";
    }

    private void Update() {
        if (!_gameHasStarted && Input.GetKeyDown(KeyCode.Alpha3) && _gameCoroutine == null) {
            InstructionUI.SetActive(false);
            Shop.SetActive(false);
            StartGame();
        }
    }

    private void StartGame() {
        _gameCoroutine = StartCoroutine(StartLevel());
        // GameManager.Instance.Save();
    }
    
    public void StopGame() {
        StopCoroutine(_gameCoroutine);
    }
    
    public IEnumerator StartLevel() {
        // level iteration
        foreach (var level in _levels) {
            int currentLevelIndex = Array.IndexOf(_levels, level);
            if (currentLevelIndex == _levels.Length-1)
                _levelText.text = $"Final Boss: Boglin";
            else
                _levelText.text = $"Level {currentLevelIndex+1}";
                // _levelText.text = $"Final Boss: Boglin";
            
            foreach (var wave in level.Waves) {
                    // checking wave type
                    if (wave.WaitForPreviousEnemiesAreDead)
                        while (_currentEnemyCount > 0) { yield return null; }
                    
                    yield return new WaitForSeconds(wave.SpawnAfterSeconds);
                    
                    // spawning enemies after the wait type has ended
                    SpawnEnemies(_enemyTypes[wave.EnemyIndex], wave.SpawnAmount);
            }
            while (_currentEnemyCount > 0) {
                yield return null;
            }
        }

        yield return null;
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
