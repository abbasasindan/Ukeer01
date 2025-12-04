using UnityEngine;

public class ScaleTriggerBehaviour : StateMachineBehaviour
{
    private SpriteScaleCorrector scaleCorrector;

    // Called on the first frame of the animation state being entered (When walking starts)
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 1. Find the SpriteScaleCorrector component on the Player
        if (scaleCorrector == null)
        {
            // The animator is on the Player, so we get the component from the animator's GameObject.
            scaleCorrector = animator.GetComponent<SpriteScaleCorrector>();
        }

        if (scaleCorrector != null)
        {
            // 2. Call the public method to apply the scale fix
            scaleCorrector.ApplyWalkScale();
        }
        else
        {
            // If this appears, the SpriteScaleCorrector script is missing from the Player object.
            Debug.LogError("FATAL ERROR: SpriteScaleCorrector not found on Player object. Cannot apply scale fix.");
        }
    }

    // Called when the Animator finishes evaluating the current state (When walking stops/transitions to Idle)
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ensure the component is found before reverting
        if (scaleCorrector == null)
        {
            scaleCorrector = animator.GetComponent<SpriteScaleCorrector>();
        }
        
        if (scaleCorrector != null)
        {
            // Call the public method to revert the scale back to the Idle size
            scaleCorrector.RevertScale();
        }
    }
}