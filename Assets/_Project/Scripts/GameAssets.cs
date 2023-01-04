using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour {
    private static GameAssets _i;

    public static GameAssets i {
        get {
            if (_i == null)
                _i = (Instantiate(Resources.Load("Prefabs/GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    private void Awake() {
        _i = this;
    }

    public Transform DamagePopupPrefab;
    public Transform DefaultProjectilePrefab;
    public Transform FlashAnimationPrefab;
    public Transform DangerMarkPrefab;
}
