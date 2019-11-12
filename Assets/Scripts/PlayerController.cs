using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool canTakeDamages=true;

    [SerializeField] float m_TranslationSpeed;
    [SerializeField] float m_GravityForce;
    //[SerializeField] float m_RotationSpeed;
    Transform m_Transform;
    Rigidbody m_Rigidbody;
   
    float distToGround;
    CapsuleCollider m_CapsuleCollider;
    float m_CapsuleHeight;
    bool isStanding = true;

    bool lookingRight = true;

    ///WEAPON HANDLING
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate=.25f;
    private float timeNextThrow;
    [SerializeField] float throwForce = 1000.0f;
    [SerializeField] float angleThrowInRadian = 0.3f;
    ///WEAPON HANDLING

    ///DAMAGES HANDLING
    PlayerArmorController armorController;

    [SerializeField] float jumpOnDamagesForce=200.0f;
    /// DAMAGES HANDLING

    private void Awake()
    {
        m_Transform = transform;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_CapsuleCollider = GetComponent<CapsuleCollider>();

        armorController = GetComponent<PlayerArmorController>();

        timeNextThrow = Time.time;
    }
    // Start is called before the first frame update
    void Start()
    {
        distToGround = m_CapsuleCollider.bounds.extents.y;
        m_CapsuleHeight = m_CapsuleCollider.height;
    }

    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }

    private void Update()
    {
       
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //m_CapsuleCollider.isTrigger = true;
            m_Rigidbody.AddForce(new Vector3(0, m_GravityForce, 0));
           
            Physics.IgnoreLayerCollision(8, 9,true);

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
        if(Input.GetButtonDown("Fire1") && Time.time >= timeNextThrow)
        {
            FireProjectile();
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
        }
        if (!IsGrounded())
        {
            m_Rigidbody.AddForce(Physics.gravity * 2*(m_Rigidbody.mass * m_Rigidbody.mass));
              
        }

        UpdateFacingDirection();
        //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * Quaternion.AngleAxis(m_RotationSpeed * Time.fixedDeltaTime * hInput, m_Transform.up));
    }


    private void FireProjectile()
    {
        GameObject tmp = Instantiate(weaponPrefab, firePoint.position, Quaternion.identity) as GameObject;
        if(lookingRight)
        {
            tmp.GetComponent<Rigidbody>().AddForce(throwForce * new Vector3(0, Mathf.Sin(angleThrowInRadian), Mathf.Cos(angleThrowInRadian)));
        }
        else
        {
            tmp.GetComponent<Rigidbody>().AddForce(throwForce * new Vector3(0, Mathf.Sin(angleThrowInRadian), -Mathf.Cos(angleThrowInRadian)));
        }
        timeNextThrow = Time.time + fireRate;
    }

    private void UpdateFacingDirection()
    {
        float hInputRaw = Input.GetAxisRaw("Horizontal");
        if (hInputRaw > 0)
            lookingRight = true;
        else if(hInputRaw < 0)
            lookingRight = false;
    }

    public void TakeDamages()
    {
        if (!canTakeDamages)
            return;

        //lifePoint -= damages;
        //if (lifePoint <= 0)
        if(!armorController.ArmorEquipped)
            Dies();
        else
            armorController.Unequip();
        JumpOnDamages();
    }

    private void Dies()
    {
        Destroy(gameObject);
    }

    private void JumpOnDamages()
    {
        m_Rigidbody.AddForce(0, jumpOnDamagesForce, jumpOnDamagesForce* (lookingRight?-1:1)); ///Test if we have to throw player to the left or to the right
        StartCoroutine("FlashOnDamages");
    }

    IEnumerator FlashOnDamages()
    {
        int flashingCount = 8;
        float flashingInterval = .1f;
        bool displayModel = true;

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        EnableDamages(false);
        for (int counter = 0; counter < flashingCount; counter++)
        {
            displayModel = !displayModel;
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].enabled = displayModel;

            yield return new WaitForSeconds(flashingInterval);
        }
        EnableDamages(true);
    }

    private void EnableDamages(bool enable)
    {
        canTakeDamages = enable;
        Physics.IgnoreLayerCollision(9, 10, !enable);
    }
}
