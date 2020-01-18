using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyToTargetBehaviour : StateMachineBehaviour
{
    Vector3 targetPosition;

    private Rigidbody rigidbody;

    [SerializeField] private float FlyingSpeed = 20.0f;

    BossWeaponController weaponController;
    [SerializeField] float timeBetweenThrow = 1.5f;
    float nextTimeThrow;

    [SerializeField] float delayBeforeNext = 4.0f;
    float startTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startTime = Time.time;
        rigidbody = animator.GetComponent<Rigidbody>();
        weaponController = animator.GetComponent<BossWeaponController>();
        if (!weaponController)
        {
            Debug.Log("ThrowFireballs : no weapon controller found");
            return;
        }

        targetPosition = animator.GetComponent<BossController>().GetCurrentTargetPosition(animator.GetInteger("CurrentTargetIndex"));
    
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody.MovePosition(Vector3.MoveTowards(rigidbody.position, targetPosition, FlyingSpeed * Time.deltaTime));
        if(Vector3.Distance(rigidbody.position, targetPosition) < .3f)
            animator.SetBool("FlyToTarget", false);

        if (Time.time >= nextTimeThrow)
            Throw();

        if(Time.time >= startTime+delayBeforeNext)
            animator.SetBool("FlyToTarget", false);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("CurrentTargetIndex", animator.GetInteger("CurrentTargetIndex") + 1);
    }

    private void Throw()
    {
        weaponController.ThrowWeapon();
        nextTimeThrow = Time.time + timeBetweenThrow;
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
