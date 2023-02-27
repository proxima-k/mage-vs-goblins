using UnityEngine;

public class OnFlashAnimationEnd : StateMachineBehaviour {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Destroy(animator.gameObject);
    }
}
