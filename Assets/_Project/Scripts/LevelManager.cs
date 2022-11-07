using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private static LevelManager _instance;

    public static LevelManager Instance {
        get {
            if (_instance == null) {
                GameObject gameObj = new GameObject();
                _instance = gameObj.AddComponent<LevelManager>();
            }

            return _instance;
        }
    }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public static void ChangeScene(int index) {
        SceneManager.LoadScene(index);
    }

    public static void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public static void QuitGame() {
        Application.Quit();
    }
    
}
