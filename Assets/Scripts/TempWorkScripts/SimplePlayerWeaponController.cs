using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerWeaponController : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 300f;


    private void Awake()
    {
        GetComponent<SimplePlayerInput>().OnThrow += ThrowWeapon;
    }
    
    private void ThrowWeapon()
    {
        GameObject throwedWeapon = Instantiate(weaponPrefab, throwPoint.position, throwPoint.rotation) as GameObject;
    }
}
