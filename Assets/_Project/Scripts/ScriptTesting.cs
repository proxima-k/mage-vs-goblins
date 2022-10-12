using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Ability;

[ExecuteInEditMode]
public class ScriptTesting : MonoBehaviour {
    // [SerializeField] private float rayDistance = 10f;
    // [SerializeField] private float rayWidth = 0.5f;
    // [SerializeField] private float angle = 45f;
    [SerializeField] private Transform _playerTf;
    [SerializeField] private int _enemySpawns = 5;
    [SerializeField] private Transform _enemyTf;
    [SerializeField] private Rect spawnField = new Rect(Vector2.zero, Vector2.one);
    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            for (int i = 0; i < _enemySpawns; i++) {
                Vector2 spawnPos = new Vector2(Random.Range(-spawnField.width/2, spawnField.width/2),Random.Range(-spawnField.height/2, spawnField.height/2));
                EnemyLocomotion enemy = Instantiate(_enemyTf, spawnPos, Quaternion.identity).GetComponent<EnemyLocomotion>();
                enemy.SetTarget(_playerTf);
            }
        }
        // Matrix4x4 matrix4X4 = Matrix4x4.Rotate(Quaternion.Euler(0,0,angle));
        // Vector3 resultPos = matrix4X4 * Vector3.right;
        // Debug.DrawLine(Vector3.zero, resultPos);
        
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(Vector2.zero, spawnField.size);
    }
}
