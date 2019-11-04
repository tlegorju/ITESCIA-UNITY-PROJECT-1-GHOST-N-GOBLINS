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
    CapsuleCollider m_capsuleCollider;

    private void Awake()
    {
        m_Transform = transform;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        distToGround = m_capsuleCollider.bounds.extents.y;
    }

    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Transform.forward * m_TranslationSpeed * Time.fixedDeltaTime * hInput);
        if (Input.GetKeyUp(KeyCode.Space)&& IsGrounded())
        {
            m_Rigidbody.AddForce(new Vector3(0, 500, 0));
            m_Rigidbody.mass = 500 ;
        }
        m_Rigidbody.mass = 1;
        
        //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * Quaternion.AngleAxis(m_RotationSpeed * Time.fixedDeltaTime * hInput, m_Transform.up));
    }
}
