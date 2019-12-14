using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerWeaponController : MonoBehaviour
{
    [SerializeField] private Rigidbody weaponPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 300f;


    private void Awake()
    {
        GetComponent<SimplePlayerInput>().OnThrow += ThrowWeapon;
    }
    
    private void ThrowWeapon()
    {
        Rigidbody throwedWeapon = Instantiate(weaponPrefab, throwPoint.position, throwPoint.rotation) as Rigidbody;
        throwedWeapon.AddForce(throwedWeapon.transform.forward*throwForce, ForceMode.Impulse);
    }
}
