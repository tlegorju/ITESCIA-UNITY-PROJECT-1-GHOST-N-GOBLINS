using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorControllerScript : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EntityController entityController = GetComponent<EntityController>();
        if(entityController)
        {
            entityController.OnHurted += TriggerHurted;
            entityController.OnDies += TriggerDeath;
        }
        ArmorController armorController = GetComponent<ArmorController>();
        if(armorController)
        {
            armorController.OnEquip += OnArmorEquipped;
            armorController.OnUnequip += OnArmorUnequipped;
        }
        SimplePlayerMovement playerMovement = GetComponent<SimplePlayerMovement>();
        if(playerMovement)
        {
            playerMovement.OnJump += TriggerJump;
            playerMovement.OnLand += OnLand;
            playerMovement.OnStartRunning += OnRun;
            playerMovement.OnStopRunning += OnStopRunning;
        }
        SimplePlayerInput playerInput = GetComponent<SimplePlayerInput>();
        if(playerInput)
        {
            playerInput.OnThrow += TriggerThrow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TriggerDeath()
    {
        animator.SetTrigger("Dies");
    }

    private void TriggerHurted()
    {
        animator.SetTrigger("Hurted");
    }

    private void TriggerThrow()
    {
        animator.SetTrigger("Throw");
    }

    private void TriggerJump()
    {
        animator.SetTrigger("Jump");
        animator.SetBool("Grounded", false);
    }

    private void OnLand()
    {
        animator.SetBool("Grounded", true);
    }

    private void OnArmorEquipped()
    {
        animator.SetBool("InArmor", true);
    }

    private void OnArmorUnequipped()
    {
        animator.SetBool("InArmor", false);
    }

    private void OnRun()
    {
        animator.SetBool("IsRunning", true);
    }

    private void OnStopRunning()
    {
        animator.SetBool("IsRunning", false);
    }
}
