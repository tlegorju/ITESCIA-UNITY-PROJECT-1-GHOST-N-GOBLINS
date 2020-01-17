using System;
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

    public event Action OnCrouch = delegate { };
    public event Action OnStandUp = delegate { };
    private bool isStanding = true;

    [SerializeField] CapsuleCollider standingCapsule;
    [SerializeField] CapsuleCollider crouchedCapsule;

    /// TEMP
    public float velocity=0;


    /// TEMP
    private void Awake()
    {
        playerInput = GetComponent<SimplePlayerInput>();
        rigidbody = GetComponent<Rigidbody>();

        squaredRigidbodyMass = rigidbody.mass * rigidbody.mass;

        standingCapsule.enabled = true;
        crouchedCapsule.enabled = false;
    }

    private void Start()
    {
        playerInput.OnJump += Jump;
        playerInput.OnClimbLadder += ClimbLadder;
        playerInput.OnCrouch += OnCrouch;
        playerInput.OnStandUp += OnStandUp;

        SimplePlayerController playerController = GetComponent<SimplePlayerController>();
        if (playerController)
        {
            playerController.OnHurted += ThrowPlayerBack;
            playerController.OnDies += DisableScript;
        }
    }

    


    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateDirection();

        //float velocity = playerInput.Horizontal * walkSpeed * Time.fixedDeltaTime;
        velocity = playerInput.Horizontal * walkSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + Vector3.forward * velocity);
        //rigidbody.velocity = new Vector3(0,0,velocity);
        UpdateVelocity(velocity);
        
        if (!IsGrounded())
        {
            rigidbody.AddForce(Physics.gravity * 3 * (squaredRigidbodyMass));
            isStanding = true;
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
    private void ClimbLadder()
    {
        if (!IsGrounded())
        {
            rigidbody.isKinematic = true;
        }

        transform.Translate(Vector3.up * playerInput.Vertical * walkSpeed*2 * Time.fixedDeltaTime);
    }

    public bool IsGrounded()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z);
        Debug.DrawRay(origin, -Vector3.up, Color.red, .1f);
        return Physics.Raycast(origin, -Vector3.up, 0.5f);
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

    private void DisableScript()
    {
        this.enabled = false;
    }

    private void Crouch()
    {
        if(isStanding && IsGrounded())
        {
            crouchedCapsule.enabled = true;
            standingCapsule.enabled = false;
            OnCrouch();
            isStanding = false;
        }
    }

    private void StandUp()
    {
        if(!isStanding && IsGrounded())
        {
            crouchedCapsule.enabled = false;
            standingCapsule.enabled = true;
            OnStandUp();
            isStanding = true;
        }
    }
}
