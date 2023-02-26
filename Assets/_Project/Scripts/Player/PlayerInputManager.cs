using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {
    private static PlayerInputManager _instance;
    public static PlayerInputManager I {
        get {
            if (_instance == null) {
                GameObject gameObj = new GameObject();
                _instance = gameObj.AddComponent<PlayerInputManager>();
            }
            return _instance;
        }
    }

    public event Action<bool> OnSetPlayerInput;

    private void Awake() {
        _instance = this;
    }

    private void Start() {
        SetPlayerInput(true);
    }

    public void SetPlayerInput(bool canInput) {
        OnSetPlayerInput?.Invoke(canInput);
    }
}
