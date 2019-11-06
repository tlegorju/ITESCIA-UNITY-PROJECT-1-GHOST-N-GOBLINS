using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_TranslationSpeed;
    [SerializeField] float m_GravityForce;
    //[SerializeField] float m_RotationSpeed;
    Transform m_Transform;
    Rigidbody m_Rigidbody;
   
    float distToGround;
    CapsuleCollider m_CapsuleCollider;
    float m_CapsuleHeight;
    bool isStanding = true;

    private void Awake()
    {
        m_Transform = transform;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        distToGround = m_CapsuleCollider.bounds.extents.y;
        m_CapsuleHeight = m_CapsuleCollider.height;
    }

    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, /*distToGround +*/ 0.1f);
    }

    private void Update()
    {
       
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //m_CapsuleCollider.isTrigger = true;
            m_Rigidbody.AddForce(new Vector3(0, m_GravityForce, 0));
           
            Physics.IgnoreLayerCollision(8, 9,true);
            Debug.Log(Physics.GetIgnoreLayerCollision(8, 9));

        }
        if (Input.GetButtonDown("Crouch") && isStanding)
        {
            isStanding = false;
            m_CapsuleCollider.height = m_CapsuleHeight / 2;
            m_CapsuleCollider.center = new Vector3(0, 0, 0);

        }
        if (Input.GetButtonUp("Crouch") && !isStanding)
        {
            m_CapsuleCollider.height = m_CapsuleHeight;
            m_CapsuleCollider.center = new Vector3(0, 1, 0);
            isStanding = true;
        }
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Transform.forward * m_TranslationSpeed * Time.fixedDeltaTime * hInput);
        if(m_Rigidbody.velocity.y < 0)
        {
            Physics.IgnoreLayerCollision(8, 9, false);
            Debug.Log(Physics.GetIgnoreLayerCollision(8, 9));
        }
        if (!IsGrounded())
        {
            m_Rigidbody.AddForce(Physics.gravity * 2*(m_Rigidbody.mass * m_Rigidbody.mass));
              
        }

        //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * Quaternion.AngleAxis(m_RotationSpeed * Time.fixedDeltaTime * hInput, m_Transform.up));
    }
    
        
    

}
