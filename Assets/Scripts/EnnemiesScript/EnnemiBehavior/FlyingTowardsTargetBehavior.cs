using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTowardsTargetBehavior : StateMachineBehaviour
{
    EnnemiController controller;
    Transform target;
    Rigidbody rigidbody;

    [SerializeField] float walkingSpeed;
    Vector3 direction;
    [SerializeField] float updateRate = 1.5f;
    private Vector3 targetPosition;

    bool goingForward = true;

    Coroutine updateCoroutine;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody = animator.GetComponent<Rigidbody>();
        controller = animator.GetComponent<EnnemiController>();
        target = controller.Player;

        //updateCoroutine = controller.StartCoroutine(UpdateDirection());
        UpdateFlyingDirection();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (target == null)
            return;

        if (CheckIfReached())
            UpdateFlyingDirection();

        //rigidbody.MovePosition(rigidbody.position + direction * walkingSpeed * Time.fixedDeltaTime);
        rigidbody.MovePosition(Vector3.MoveTowards(rigidbody.position, targetPosition, walkingSpeed * Time.fixedDeltaTime));
        rigidbody.MoveRotation(Quaternion.RotateTowards(rigidbody.rotation,
                                                        Quaternion.LookRotation((targetPosition-rigidbody.position).normalized),
                                                        180*Time.fixedDeltaTime));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.StopCoroutine(updateCoroutine);
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

    private bool CheckIfReached()
    {
        if ((goingForward && rigidbody.position.z < targetPosition.z + .05f
                        && rigidbody.position.z > targetPosition.z - .05f)
          || (!goingForward && rigidbody.position.y < targetPosition.y + .05f
                        && rigidbody.position.y > targetPosition.y - .05f))
        {
            goingForward = !goingForward;
            return true;
        }
        return false;
    }

    private void UpdateFlyingDirection()
    {
        if (goingForward)
        {
            targetPosition = new Vector3(rigidbody.position.x, rigidbody.position.y, target.transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(rigidbody.position.x, target.transform.position.y, rigidbody.position.z);
        }
    }

    IEnumerator UpdateDirection()
    {
        while (true)
        {
            targetPosition = target.transform.position;
            if (direction.z != 0)
            {
                if (rigidbody.position.y < targetPosition.y)
                    direction = new Vector3(0, 1, 0);
                else
                    direction = new Vector3(0, -1, 0);
            }
            else
            {
                if (rigidbody.position.z < targetPosition.z)
                    direction = new Vector3(0, 0, 1);
                else
                    direction = new Vector3(0, 0, -1);
            }

            yield return new WaitForSeconds(updateRate);
        }
    }
}
