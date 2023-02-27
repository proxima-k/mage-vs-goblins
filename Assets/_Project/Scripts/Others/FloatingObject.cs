using UnityEngine;

public class FloatingObject : MonoBehaviour {
    
    [SerializeField] private float _floatHeight = 0.2f;
    [SerializeField] private float _floatSpeed = 2.5f;
    private float defaultHeight;

    private void Start() {
        defaultHeight = transform.localPosition.y;
    }

    private void Update() {
        Vector3 pos = transform.localPosition;
        float heightShift = _floatHeight * Mathf.Sin(Time.time * _floatSpeed);
        transform.localPosition =  new Vector3(pos.x, defaultHeight + heightShift);   
    }
}
