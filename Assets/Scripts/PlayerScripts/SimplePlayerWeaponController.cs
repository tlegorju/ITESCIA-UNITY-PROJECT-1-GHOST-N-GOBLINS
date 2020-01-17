using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerWeaponController : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    public GameObject WeaponPrefab { get { return weaponPrefab; } set { weaponPrefab = value; } }
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 300f;

    [SerializeField] private float throwRate = .2f;
    private float nextTimeThrow = 0;

    public event Action OnThrow = delegate{};

    private void Awake()
    {
        GetComponent<SimplePlayerInput>().OnThrow += ThrowWeapon;
    }
    
    private void ThrowWeapon()
    {
        if (Time.time < nextTimeThrow)
            return;

        GameObject throwedWeapon = Instantiate(weaponPrefab, throwPoint.position, throwPoint.rotation) as GameObject;
        OnThrow();
        nextTimeThrow = Time.time + throwRate;
    }
}
