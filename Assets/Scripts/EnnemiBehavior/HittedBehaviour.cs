using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedBehaviour : StateMachineBehaviour
{
    [SerializeField] float hurtedDuration = 1.0f;
    [SerializeField] float flashDuration = .1f;
    private float nextTimeFlash;
    private bool flashing = false;
    [SerializeField] Material flashingMaterial;
    private Material originalMaterial;

    private Renderer[] renderers;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        renderers = animator.GetComponentsInChildren<Renderer>();
        originalMaterial = renderers[0].material;
        nextTimeFlash = Time.time;
        hurtedDuration += Time.time;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(hurtedDuration<=Time.time)
        {
            animator.SetTrigger("HittedStop");
            return;
        }

        if(Time.time > nextTimeFlash)
        {
            SetMaterialOnAllRenderers(flashing ? originalMaterial : flashingMaterial);
            flashing = !flashing;
            nextTimeFlash += flashDuration;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetMaterialOnAllRenderers(originalMaterial);
    }

    public void SetMaterialOnAllRenderers(Material mat)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material = mat;
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
