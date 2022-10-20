using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CurrencyPickupable : MonoBehaviour {
    private int _currencyAmount;

    public void Setup(int currencyAmount) {
        _currencyAmount = currencyAmount;
        
        Rigidbody2D rb2D = gameObject.AddComponent<Rigidbody2D>();
        rb2D.isKinematic = true;
        
        Collider2D collider2D = GetComponentInChildren<Collider2D>();
        collider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        // perhaps change to one that checks for anyone that can pickup stuff
        if (col.TryGetComponent(out IPickuper pickuper)) {
            pickuper.PickupCurrency(_currencyAmount);
            Destroy(gameObject);
        }
    }
}
