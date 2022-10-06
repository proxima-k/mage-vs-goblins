using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTesting : MonoBehaviour {
    [SerializeField] private Ability playerShoot;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            playerShoot.CastAbility();
        }
    }
}
