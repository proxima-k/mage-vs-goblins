using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {
    // reference to shop code
    
    // block template
        // orb sprite and name
        // when clicked, show next upgrade stats
            // get list of attributes
    [SerializeField] private Transform slotPrefab;
    
    // create block for all upgradable attributes
        // get orbs
        // get abilities from orbs
        // get upgradable stats(attributes) from abilities
        // button upgrade
            // upgradeitem using interactor's currency
        
    // on awake
        // create the card from the template and assign anything
    private void Awake() {
        Button shopButton = slotPrefab.GetComponent<Button>();
    }

    // show(interactor)
    // hide()
}

// block template monobehavior?