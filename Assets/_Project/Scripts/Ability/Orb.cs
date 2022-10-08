using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Orb")]
public class Orb : ScriptableObject {
    
    [Range(0, 1000)]
    public int damage = 10;
    
    public virtual IEnumerator Shoot(Vector3 origin, Vector3 targetDir) {
        yield return null;
    }
}
