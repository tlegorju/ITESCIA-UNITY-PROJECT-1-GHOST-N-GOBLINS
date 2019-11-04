using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingTowardsTargetBehavior : StateMachineBehaviour
{
    EnnemiController controller;
    Transform target;
    Rigidbody rigidbody;

    [SerializeField] float walkingSpeed;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody = animator.GetComponent<Rigidbody>();
        controller = animator.GetComponent<EnnemiController>();
        target = controller.Player;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (target == null)
            return;

        Vector3 direction;
        if (rigidbody.position.z < target.transform.position.z)
            direction = new Vector3(0, 0, 1);
        else
            direction = new Vector3(0, 0, -1);

        rigidbody.MovePosition(rigidbody.position + direction * walkingSpeed * Time.fixedDeltaTime);
        rigidbody.MoveRotation(Quaternion.LookRotation(direction));
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
