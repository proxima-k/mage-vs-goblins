using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour {
    [SerializeField] private Player _player;

    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if (_instance == null) {
                GameObject gameObj = new GameObject();
                _instance = gameObj.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    private void Awake() {
        _instance = this;
        SaveSystem.Init();
    }

    private void Start() {
        Load();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.ChangeScene(0);
        }
    }

    public void Save() {
        SaveObject saveObject = new SaveObject() {
            currencyAmount = _player.GetCurrencySystem().Currency
        };
        string json = JsonUtility.ToJson(saveObject);
        
        SaveSystem.Save(json);
    }

    public void Load() {
        string json = SaveSystem.Load();

        if (json != null) {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);
            _player.GetCurrencySystem().SetAmount(saveObject.currencyAmount);
        }
    }

    private class SaveObject {
        public int currencyAmount;
    }
}
