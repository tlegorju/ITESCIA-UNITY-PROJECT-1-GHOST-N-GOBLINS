using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 30f;
    [SerializeField] private float jumpForce = 30f;

    private new Rigidbody rigidbody;

    private SimplePlayerInput playerInput;
    //private float lastThrust = float.MinValue;

    //public event Action<float> ThrustChanged = delegate { };

    private void Awake()
    {
        playerInput = GetComponent<SimplePlayerInput>();
        rigidbody = GetComponent<Rigidbody>();

        playerInput.OnJump += Jump;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (lastThrust != playerInput.Vertical)
            ThrustChanged(playerInput.Vertical);

        lastThrust = playerInput.Vertical;*/

        //rigidbody.MoveRotation(Quaternion.Euler(0, 180 * (playerInput.Horizontal), 0));
        Debug.Log(IsGrounded());
        rigidbody.MovePosition(rigidbody.position + transform.forward * playerInput.Horizontal * walkSpeed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        if(IsGrounded())
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public bool IsGrounded()
    {
        return !Physics.Raycast(transform.position, -Vector3.up);
    }
}
