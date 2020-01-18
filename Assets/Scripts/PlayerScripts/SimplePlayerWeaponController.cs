using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerWeaponController : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private GameObject weaponPrefab;
    public GameObject WeaponPrefab { get { return weaponPrefab; } set { weaponPrefab = value; } }
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 300f;

    [SerializeField] private float throwRate = .2f;
    public AudioClip throwSound;
    private float nextTimeThrow = 0;

    public event Action OnThrow = delegate{};

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        GetComponent<SimplePlayerInput>().OnThrow += ThrowWeapon;
    }
    
    private void ThrowWeapon()
    {
        if (Time.time < nextTimeThrow)
            return;
        source.clip = throwSound;
        source.Play();
        GameObject throwedWeapon = Instantiate(weaponPrefab, throwPoint.position, throwPoint.rotation) as GameObject;
        OnThrow();
        nextTimeThrow = Time.time + throwRate;
    }
}
