using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] private Player _player;
    private IShopCustomer _shopCustomer;
    [SerializeField] private ShopUI _shopUI;

    private void Start() {
        _shopCustomer = _player.GetComponent<IShopCustomer>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            // disable player movement
            _shopUI.Show(_shopCustomer);
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
            _shopUI.Hide();
            // enable player movement
    }
    
    
}
