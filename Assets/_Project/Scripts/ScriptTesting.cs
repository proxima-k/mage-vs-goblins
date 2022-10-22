using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using AbilitySystem;

[ExecuteInEditMode]
public class ScriptTesting : MonoBehaviour {
    [SerializeField]
    private Vector2 explosionCount;

    private Vector3[] explosionPositions;
    
    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.F)) {
            for (int i = 0; i < explosionCount.x; i++) {
                for (int j = 0; j < explosionCount.y; j++) {
                    Vector2 fraction = new Vector2(i / explosionCount.x, j / explosionCount.y);
                    // explosionPositions = 
                    // Random.Range()
                }
            }
        }
        // Matrix4x4 matrix4X4 = Matrix4x4.Rotate(Quaternion.Euler(0,0,angle));
        // Vector3 resultPos = matrix4X4 * Vector3.right;
        // Debug.DrawLine(Vector3.zero, resultPos);
        
    }

    private void OnDrawGizmos() {
    }
}
