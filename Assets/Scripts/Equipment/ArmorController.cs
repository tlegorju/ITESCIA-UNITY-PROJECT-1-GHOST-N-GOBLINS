using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorController : MonoBehaviour
{
    [SerializeField] Vector3 localPosition;
    [SerializeField] Vector3 localRotation;
    [SerializeField] Vector3 localScale;

    public bool displayRot = false;

    public void AttachArmor(Transform parent)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            //Change layer collision
            rb.isKinematic = true;

            transform.parent = parent;
            transform.localPosition = localPosition;
            transform.localRotation = Quaternion.Euler(localRotation);
            transform.localScale = localScale;
        }
    }

    public void DetachArmor(Vector3 impulse)
    {
        transform.parent = null;
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb!=null)
        {
            //Change layer collision
            rb.isKinematic = false;
            rb.AddForce(impulse);
        }
        StartCoroutine(FlashArmor());
    }

    IEnumerator FlashArmor()
    {
        float timeStopFlashing = Time.time + 3.0f;
        float flashingInterval = .1f;
        bool displayModel = true;

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        while(Time.time < timeStopFlashing)
        {
            displayModel = !displayModel;
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].enabled = displayModel;

            yield return new WaitForSeconds(flashingInterval);
        }

        Destroy(gameObject);
    }
}
