using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {
    private static SceneManager _instance;

    public static SceneManager Instance {
        get {
            if (_instance == null) {
                GameObject gameObj = new GameObject();
                _instance = gameObj.AddComponent<SceneManager>();
            }

            return _instance;
        }
    }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public static void ChangeScene(int index) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public static void ChangeScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public static void QuitGame() {
        Application.Quit();
    }
    
}
