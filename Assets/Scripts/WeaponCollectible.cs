using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollectible : Collectible
{
    [SerializeField] GameObject newWeaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            CollidedWithPlayer(other.gameObject);
    }

    protected override void CollidedWithPlayer(GameObject player)
    {
        SimplePlayerWeaponController weaponController = player.GetComponent<SimplePlayerWeaponController>();
        if (weaponController)
        {
            weaponController.WeaponPrefab = newWeaponPrefab;
        }
        base.CollidedWithPlayer(player);
    }
}
