using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Orb")]
public class Orb : ScriptableObject {
    
    [Range(0, 10000)]
    public int damage = 10;
    
    public virtual void Shoot(Vector3 origin, Vector3 targetDir) {
        
    }
}
