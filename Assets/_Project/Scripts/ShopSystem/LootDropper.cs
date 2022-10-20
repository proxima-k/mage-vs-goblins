using System;
using System.Collections;
using System.Collections.Generic;
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
        Transform _currencyPickupInstance = Instantiate(_currencyPickupPf, position, Quaternion.identity);
        CurrencyPickupable pickupable = _currencyPickupInstance.gameObject.AddComponent<CurrencyPickupable>();
        pickupable.Setup(amount);
    }
}
