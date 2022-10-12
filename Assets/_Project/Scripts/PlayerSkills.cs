using Proxima_K.Utils;
using UnityEngine;
using AbilitySystem;

public class PlayerSkills : MonoBehaviour {
    private Camera _cam;
    [SerializeField] private Orb _currOrb;
    [SerializeField] private Ability fireballAbility;
    

    private void Start() {
        _cam = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(fireballAbility.TriggerAbility(transform));
        }
    }
}




/*[Header("Laser Properties")]
[SerializeField] private bool _visualizeLaserArea;
[SerializeField] private float _laserDistance = 5f;
[SerializeField] private float _laserWidth = 2f;
[SerializeField] private float _laserEndPointOffset = 0.1f;
private void OnDrawGizmos() {
    if (_visualizeLaserArea) {
        Vector3 origin = transform.position;
        Vector3 targetPos = PK.GetMouseWorldPosition2D(Camera.main);
        Vector3 targetDir = (targetPos - origin).normalized;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(origin + targetDir * _laserDistance * 0.5f, Quaternion.Euler(0, 0, angle),
            Vector3.one);
        Gizmos.DrawWireCube(Vector2.zero, new Vector3(2 * _laserEndPointOffset + _laserDistance, _laserWidth));
    }
}*/
