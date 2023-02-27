using UnityEngine;

public class LootDropper : MonoBehaviour {
    public static LootDropper Instance { get; private set; }
    [SerializeField] private Transform _currencyPickupPf;

    private void Awake() {
        if (Instance != null) 
            Destroy(gameObject);
        else {
            Instance = this;
        }
    }

    public void DropCurrency(int amount, Vector3 position) {
        Transform currencyPickupTf = Instantiate(_currencyPickupPf, position, Quaternion.identity);
        CurrencyPickupable pickupable = currencyPickupTf.gameObject.AddComponent<CurrencyPickupable>();
        pickupable.Setup(amount);
    }
}
