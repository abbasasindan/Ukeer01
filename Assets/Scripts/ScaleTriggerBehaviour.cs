using UnityEngine;

// This script MUST inherit from StateMachineBehaviour
// It is now empty because the scale correction is no longer needed.
public class ScaleTriggerBehaviour : StateMachineBehaviour
{
    // The OnStateEnter and OnStateExit methods remain but do nothing,
    // so they won't throw an error looking for the deleted SpriteScaleCorrector.
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // No action needed.
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // No action needed.
    }
}