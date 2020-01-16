using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowFireBallsAroundBehaviour : StateMachineBehaviour
{
    BossWeaponController weaponController;
    [SerializeField] float delayBeforeThrow = 2.0f;
    float timeThrow;
    bool thrown;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        weaponController = animator.GetComponent<BossWeaponController>();
        if(!weaponController)
        {
            Debug.Log("ThrowFireballs : no weapon controller found");
            return;
        }
        timeThrow = Time.time + delayBeforeThrow;
        thrown = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timeThrow <= Time.time && !thrown)
        {
            weaponController.ThrowWeaponsAround();
            animator.SetBool("ThrowFireballs", false);
            thrown = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
