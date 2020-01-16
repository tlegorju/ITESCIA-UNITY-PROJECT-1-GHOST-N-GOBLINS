using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    [SerializeField] float timeBeforeActivation;
    [SerializeField] float fallSpeed;
    [SerializeField] float resetTime = 10.0f;

    Vector3 originPosition;
    Transform m_transform;
    Rigidbody m_Rigibody;

    bool falling = false;

    void Awake()
    {
        m_transform = transform;
        m_Rigibody = GetComponent<Rigidbody>();
        originPosition = transform.position;
    }
    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer.Equals(9) && !falling)
        {
            StartCoroutine(ActivatePlatform());
            StartCoroutine(ResetPlatform());
        }
        StartCoroutine(ResetPlatform());

    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.layer.Equals(9))
        {

            StartCoroutine(ResetPlatform());
        }

    }

    IEnumerator ActivatePlatform()
    {
        falling = true;
        yield return new WaitForSeconds(timeBeforeActivation);
        Debug.Log("Activation");
        m_Rigibody.useGravity = true;
        m_Rigibody.isKinematic = false;
         m_Rigibody.AddForce(new Vector3(0, -fallSpeed, 0));
        

    }

    IEnumerator ResetPlatform()
    {
        yield return new WaitForSeconds(timeBeforeActivation+resetTime);
        Debug.LogError("ResetPlatform");
        m_Rigibody.isKinematic = true;
        m_Rigibody.useGravity = false;
        m_Rigibody.MovePosition(originPosition+ m_transform.up*fallSpeed*Time.deltaTime);
        StopAllCoroutines();
        yield return null;
        falling = false;
    }

    public bool IsAboveGround()
    {
        return transform.position.y >= 0;
    }
    private void FixedUpdate()
    {
       // Debug.Log(IsAboveGround());
       // if (!IsAboveGround())
       // {
           
<<<<<<< HEAD
            //StartCoroutine(ResetPlatform());
=======
           
>>>>>>> 27a93d140064e117dd6494974b5adde025a37748
        //}
    }
}
