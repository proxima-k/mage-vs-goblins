using UnityEngine;
using UnityEngine.PlayerLoop;

public class DangerMarkPopup : MonoBehaviour {
    public static DangerMarkPopup Create(Vector3 origin) {
        DangerMarkPopup dangerMarkPopup = Instantiate(GameAssets.i.DangerMarkPrefab, origin, Quaternion.identity).gameObject.AddComponent<DangerMarkPopup>();
        dangerMarkPopup.Init();
        return dangerMarkPopup;
    }

    private void Init() {
        transform.position += new Vector3(Random.Range(-0.3f, 0.3f), 0.5f);
    }
}
