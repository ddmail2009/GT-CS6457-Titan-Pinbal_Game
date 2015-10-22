/**
 * Team Titan
 *
 * PoHsien Wang
 */

using UnityEngine;
using System.Collections;

public class CharacterStateVoice : StateMachineBehaviour
{
	public AudioClip[] clips;
	public float globalProbability = 1f;
	public float[] individualProbabilities = {1f};

	AudioSource aud;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (Random.value > globalProbability) {
			return;
		}

		for (int i = 0; i < clips.Length; ++i) {
			if (Random.value < individualProbabilities [i]) {
				// FIXME: why can they not to share same AudioSource?
				if (!aud) {
					aud = animator.GetComponents<AudioSource> () [1];
				}

				aud.PlayOneShot (clips [i], 1.0f);
			}
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
