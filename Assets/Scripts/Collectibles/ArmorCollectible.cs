using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorCollectible : Collectible
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            CollidedWithPlayer(other.gameObject);
    }

    protected override void CollidedWithPlayer(GameObject player)
    {
        PlayerArmorController armorController = player.GetComponent<PlayerArmorController>();
        if(armorController)
        {
            armorController.Equip();
        }
        base.CollidedWithPlayer(player);
    }
}
