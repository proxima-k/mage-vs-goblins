using UnityEngine;
using UnityEngine.UI;

public class DashAbilityUI : MonoBehaviour {
    [SerializeField] private Transform dashChargeBarPf;
    [SerializeField] private Material[] _dashBarsMat;
    private int _dashMaxCharges;

    private float _previousDashChargeTank;
    private static readonly int dashEnergyMatPropID = Shader.PropertyToID("_DashEnergy");
    
    public void Initialize(int dashCharges) {
        _dashMaxCharges = dashCharges;
        _dashBarsMat = new Material[dashCharges];
        for (int i = 0; i < dashCharges; i++) {
            Transform dashBar = Instantiate(dashChargeBarPf, Vector3.zero, Quaternion.identity);
            dashBar.SetParent(transform);
            
            Image dashBarImage = dashBar.GetComponent<Image>();
            
            // material instancing
            _dashBarsMat[i] = Instantiate(dashBarImage.material);
            dashBarImage.material = _dashBarsMat[i];
        }

        _previousDashChargeTank = dashCharges;
    }

    public void UpdateDashChargeBar(float dashChargeTank) {
        int currentBarIndex = Mathf.FloorToInt(dashChargeTank);
        int previousBarIndex = Mathf.FloorToInt(_previousDashChargeTank);
        
        if (currentBarIndex != previousBarIndex) {
            int indexDifference = currentBarIndex - previousBarIndex;
            if (indexDifference < 0) {
                // some energy has been used
                if (previousBarIndex >= _dashMaxCharges)
                    previousBarIndex--;
                for (int i = previousBarIndex; i > currentBarIndex; i--) {
                    _dashBarsMat[i].SetFloat(dashEnergyMatPropID, 0);
                }
            } else {
                // some energy has been regenerated
                for (int i = previousBarIndex; i < currentBarIndex; i++) {
                    _dashBarsMat[i].SetFloat(dashEnergyMatPropID, 1f);
                }
            }
        }

        if (currentBarIndex >= _dashMaxCharges) return;
        // always update the current bar regardless of other changes
        _dashBarsMat[currentBarIndex].SetFloat(dashEnergyMatPropID, dashChargeTank-currentBarIndex);
        _previousDashChargeTank = dashChargeTank;
    }
}
