using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScriptTesting : MonoBehaviour {
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private float rayWidth = 0.5f;
    [SerializeField] private float angle = 45f;
    
    private void Update() {
        // Matrix4x4 matrix4X4 = Matrix4x4.Rotate(Quaternion.Euler(0,0,angle));
        // Vector3 resultPos = matrix4X4 * Vector3.right;
        // Debug.DrawLine(Vector3.zero, resultPos);
    }
}
