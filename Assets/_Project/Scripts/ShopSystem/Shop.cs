using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    // player attributes
    // orb abilities attributes
    
    // ListOutAttributes
    public Orb[] OrbList;

    // keeps reference to a shop interactor

    private void Awake() {
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            // check orb upgrade cost
            // if cost < interactor's currency, then upgrade orb
            if (OrbList[0].CanUpgrade())
                if (!OrbList[0].UpgradeOrb(new CurrencySystem(100)))
                    Debug.Log("Not enough currency");
            else {
                Debug.Log("capped");
            }
        }
    }
    
    
}
