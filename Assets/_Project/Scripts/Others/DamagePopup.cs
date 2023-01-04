using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class DamagePopup : MonoBehaviour {
    public static DamagePopup Create(int value, Vector3 origin) {
        DamagePopup damagePopup = Instantiate(GameAssets.i.DamagePopupPrefab, origin, Quaternion.identity).gameObject.AddComponent<DamagePopup>();
        damagePopup.Init(value);
        return damagePopup;
    }

    private Vector3 _moveDir;
    private TextMeshPro _textMesh;
    private Color _textColor;
    // private const float TEXT_START_SIZE = 20f;
    private float _defaultTextSize;
    private float _fontScaleSpeed = 15f;
    // timer
    private const float POPUP_DURATION = 0.75f;
    private float _disappearTimer;
    private float _disappearSpeed = 8f;
    
    private void Awake() {
        _textMesh = GetComponent<TextMeshPro>();
    }

    public void Init(int value) {
        _textMesh.text = value.ToString();
        _textColor = _textMesh.color;
        _defaultTextSize = _textMesh.fontSize;
        _textMesh.fontSize = _defaultTextSize+5;

        _moveDir = new Vector3(Random.Range(-0.5f, 0.5f), 1).normalized;
        _disappearTimer = POPUP_DURATION;
    }

    private void Update() {
        // popup movement
        transform.position += _moveDir * 5f * Time.deltaTime;
        _moveDir += Vector3.down * 2f * Time.deltaTime;

        if (_textMesh.fontSize > _defaultTextSize) {
            _textMesh.fontSize -= Time.deltaTime * _fontScaleSpeed;
        }
        
        _disappearTimer -= Time.deltaTime;
        if (_disappearTimer < 0) {
            _textColor.a -= Time.deltaTime * _disappearSpeed;
            _textMesh.color = _textColor;
            if (_textColor.a <= 0)
                Destroy(gameObject);
        }
    }
}
