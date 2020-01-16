using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpearWeaponController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GetComponent<Rigidbody>().AddForce(transform.forward * translationSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
