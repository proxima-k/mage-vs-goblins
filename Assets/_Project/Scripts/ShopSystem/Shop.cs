using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    // player attributes
    // orb abilities attributes
    
    // ListOutAttributes
    public Orb[] OrbList;
    
    // UpgradeAttribute()
    // 
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            OrbList[0].UpgradeOrb();
        }
    }
    
    
}
