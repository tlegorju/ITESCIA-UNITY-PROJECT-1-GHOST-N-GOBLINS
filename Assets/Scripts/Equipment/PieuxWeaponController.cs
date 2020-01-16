using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieuxWeaponController : WeaponController
{
    // Start is called before the first frame update
    public float throwSpeed;
    public float rotateSpeed;
    protected override void Start()
    {
        base.Start();
        GetComponent<Rigidbody>().velocity = throwSpeed * transform.forward;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotateSpeed * 10 * Time.deltaTime, 0, 0));
    }
}
