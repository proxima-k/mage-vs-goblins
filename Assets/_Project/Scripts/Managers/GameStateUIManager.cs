using System;
using UnityEngine;

public class GameStateUIManager : MonoBehaviour {
    private static GameStateUIManager _instance;
    public static GameStateUIManager I {
        get {
            if (_instance == null) {
                GameObject gameObj = new GameObject();
                _instance = gameObj.AddComponent<GameStateUIManager>();
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject _deathUI;
    [SerializeField] private GameObject _winUI;

    public event Action OnPlayerWin;

    private void Awake() {
        _instance = this;
    }

    public void TriggerGameWin() {
        OnPlayerWin?.Invoke();
        _winUI.SetActive(true);
        GameManager.Instance.Save();
    }

    public void TriggerGameOver() {
        _deathUI.SetActive(true);
        GameManager.Instance.Save();
    }
}
