using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    // setup variables
        // collision layers
        // has a height (composition)
        
    private void OnCollisionEnter2D(Collision2D col) {
        // if the collided object has IDamageable, then damage it
        Destroy(gameObject);
    }
}