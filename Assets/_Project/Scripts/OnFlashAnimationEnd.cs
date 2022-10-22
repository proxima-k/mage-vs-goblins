using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFlashAnimationEnd : StateMachineBehaviour {
    // public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Destroy(animator.gameObject, stateInfo.length);
    // }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Destroy(animator.gameObject);
    }
}
