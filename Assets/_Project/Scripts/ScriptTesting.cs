using UnityEngine;
using AbilitySystem;

[ExecuteInEditMode]
public class ScriptTesting : MonoBehaviour {
    private Material healthBarMat;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake() {

    }

    private void Update() {

    }

    #if UNITY_EDITOR
    private void OnDrawGizmos() {
    }
    #endif
}
