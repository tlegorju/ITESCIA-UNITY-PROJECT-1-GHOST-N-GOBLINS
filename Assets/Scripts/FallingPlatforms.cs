using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    [SerializeField] float timeBeforeActivation;
    [SerializeField] float fallSpeed;

    Vector3 originPosition;
    Transform m_transform;
    Rigidbody m_Rigibody;

    void Awake()
    {
        m_transform = transform;
        m_Rigibody = GetComponent<Rigidbody>();
        originPosition = transform.position;
    }
    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer.Equals(9))
        {
            StartCoroutine(ActivatePlatform());
        }
      
        
    }

    

    IEnumerator ActivatePlatform()
    {
        
        yield return new WaitForSeconds(timeBeforeActivation);
        Debug.Log("Activation");
        m_Rigibody.useGravity = true;
        m_Rigibody.isKinematic = false;
         m_Rigibody.AddForce(new Vector3(0, -fallSpeed, 0));
        yield return null;

    }

    IEnumerator ResetPlatform()
    {
        yield return new WaitForSeconds(timeBeforeActivation+3);
        Debug.LogError("ResetPlatform");
        m_Rigibody.isKinematic = true;
        m_Rigibody.useGravity = false;
        m_Rigibody.MovePosition(originPosition+ m_transform.up*fallSpeed*Time.deltaTime);
        StopAllCoroutines();
        yield return null;
    }

    public bool IsAboveGround()
    {
        return transform.position.y >= 0;
    }
    private void FixedUpdate()
    {
        Debug.Log(IsAboveGround());
       // if (!IsAboveGround())
       // {
           
            StartCoroutine(ResetPlatform());
        //}
    }
}
