using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Ability")]
public class Ability : ScriptableObject {
    public string Name;
    public Sprite Icon;

    public virtual void CastAbility() {
        Debug.Log("Base class cast ability function");
    }
}

