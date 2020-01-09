using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyTowardsPlayerBehaviour : StateMachineBehaviour
{
    Vector3 targetPosition;

    private Rigidbody rigidbody;

    [SerializeField] private float FlyingSpeed = 40.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody = animator.GetComponent<Rigidbody>();

        targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody.MovePosition(Vector3.MoveTowards(rigidbody.position, targetPosition, FlyingSpeed * Time.deltaTime));
        if (Vector3.Distance(rigidbody.position, targetPosition) < .3f)
            animator.SetBool("FlyTowardsPlayer", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("CurrentTargetIndex", 0);
        animator.SetBool("ThrowFireballs", true);
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
