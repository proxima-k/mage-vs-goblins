using System.Collections;
using UnityEngine;

namespace AbilitySystem {
    public class Ability : ScriptableObject {
        public virtual IEnumerator TriggerAbility(Transform abilityCaster) {
            yield return null;
        }

        public virtual void CancelAbility() {

        }
    }
}
