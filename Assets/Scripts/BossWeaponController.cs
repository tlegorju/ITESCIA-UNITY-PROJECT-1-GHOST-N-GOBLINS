using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponController : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;

    [SerializeField] int nbFireballsMax = 5;

    private void Awake()
    {
        
    }

    public void ThrowWeapon()
    {
        GameObject throwedWeapon = Instantiate(weaponPrefab, transform.position, transform.rotation) as GameObject;
    }

    public void ThrowWeaponsAround()
    {
        for(int i=0; i<nbFireballsMax; i++)
        {
            int rotation = - (360 / (i + 1));
            Debug.Log(rotation);
            GameObject throwedWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.Euler(rotation, 0, 0)) as GameObject;
        }
    }
}
