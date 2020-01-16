using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : EntityController
{
    private bool canTakeDamages = true;
    [SerializeField] float invulnerabilityDuration = 2.0f;

    private void Awake()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ennemi"))
        {
            TakeDamages(1);
        }
    }

    public override void TakeDamages(int damage)
    {
        if (!canTakeDamages)
            return;

        PlayerArmorController armorController = GetComponent<PlayerArmorController>();

        if (!armorController.ArmorEquipped)
            Dies();
        else
            Hurted();
    }

    private void Hurted()
    {
        DisableDamages();
        Invoke("EnableDamages", invulnerabilityDuration);
        CallOnHurted();
    }

    public override void Dies()
    {
        CallOnDies();
        //Destroy(gameObject);
    }


    private void EnableDamages()
    {
        canTakeDamages = true;
        Physics.IgnoreLayerCollision(9, 10, false);
    }

    private void DisableDamages()
    {
        canTakeDamages = false;
        Physics.IgnoreLayerCollision(9, 10, true);
    }
}
