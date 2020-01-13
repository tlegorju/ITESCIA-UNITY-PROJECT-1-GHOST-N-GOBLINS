﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 30f;
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] private float throwBackForce = 30f;

    private new Rigidbody rigidbody;

    private SimplePlayerInput playerInput;

    private bool facingRight = true;

    private float squaredRigidbodyMass;


    public event Action OnJump = delegate { };
    public event Action OnLand = delegate { };
    private bool wasGrounded = false;

    public event Action OnStartRunning = delegate { };
    public event Action OnStopRunning = delegate { };
    private float previousVelocity = 0;


    private void Awake()
    {
        playerInput = GetComponent<SimplePlayerInput>();
        rigidbody = GetComponent<Rigidbody>();

        squaredRigidbodyMass = rigidbody.mass * rigidbody.mass;
    }

    private void Start()
    {
        playerInput.OnJump += Jump;

        SimplePlayerController playerController = GetComponent<SimplePlayerController>();
        if (playerController)
            playerController.OnHurted += ThrowPlayerBack;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateDirection();

        float velocity = playerInput.Horizontal * walkSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + Vector3.forward * velocity);
        UpdateVelocity(velocity);
        
        if (!IsGrounded())
        {
            rigidbody.AddForce(Physics.gravity * 3 * (squaredRigidbodyMass));

        }
        else if(!wasGrounded)
        {
            OnLand();
            wasGrounded = true;
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            OnJump();
            wasGrounded = false;
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.5f);
    }

    private void ThrowPlayerBack()
    {
        Vector3 force = Vector3.up - Vector3.forward;
        force = force.normalized * throwBackForce;
        rigidbody.AddForce(force, ForceMode.Impulse);
    }

    private void UpdateDirection()
    {
        if(playerInput.Horizontal < -0.1 && facingRight)
        {
            rigidbody.MoveRotation(Quaternion.Euler(0, 180, 0));
        }
        else if(playerInput.Horizontal > 0.1 && !facingRight)
        {
            rigidbody.MoveRotation(Quaternion.Euler(0, 0, 0));
        }
        facingRight = !facingRight;
    }

    private void UpdateVelocity(float velocity)
    {
        if(Mathf.Abs(velocity) < .1f && Mathf.Abs(previousVelocity) > .1f)
        {
            OnStopRunning();
        }
        else if(Mathf.Abs(velocity) > .1f && Mathf.Abs(previousVelocity) < .1f)
        {
            OnStartRunning();
        }
        previousVelocity = velocity;
    }
}
