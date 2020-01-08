using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handle player Inputs

public class SimplePlayerInput : MonoBehaviour
{
    public float Horizontal { get; private set; }
    //public float Vertical { get; private set; }
    public bool Jump { get; private set; }
    public bool ThrowWeapon{ get; private set; }

    public event Action OnThrow = delegate { };
    public event Action OnJump = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        //Vertical = Input.GetAxis("Vertical");
        Jump = Input.GetButtonDown("Jump");
        if (Jump) {
            OnJump(); 
        }
        ThrowWeapon = Input.GetButtonDown("Fire1");
        if (ThrowWeapon)
            OnThrow();
    }
}
