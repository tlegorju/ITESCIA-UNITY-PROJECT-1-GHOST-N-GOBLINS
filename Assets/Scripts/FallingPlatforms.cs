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
        StartCoroutine(activatePlatform());
    }

    private void OnCollisionExit(Collision collision)
    {
        StopCoroutine(activatePlatform());
    }
    IEnumerator activatePlatform()
    {
        
        yield return new WaitForSeconds(timeBeforeActivation);
        m_Rigibody.useGravity = true;
        m_Rigibody.isKinematic = false;
    }

}
