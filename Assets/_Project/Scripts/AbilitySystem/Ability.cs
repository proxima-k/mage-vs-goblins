using System;
using System.Collections;
using UnityEngine;

namespace AbilitySystem {
    public class Ability : ScriptableObject {
        // public float abilityCooldown = 1f;
        // protected bool canCastAbility = true;

        public virtual IEnumerator TriggerAbility(Transform abilityCaster, Action callback = null) {
            yield return null;
            callback?.Invoke();
        }

        public virtual void CancelAbility() {
        }

        public virtual void UpgradeAbility() {
        }

        public virtual void ResetAbilityLevel() {
        }

        public virtual string GetAttributesInfo() {
            return "";
        }
    }
}
