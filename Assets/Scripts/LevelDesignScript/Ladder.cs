using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider ladderBoxCollider;

    void Awake()
    {
        ladderBoxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<SimplePlayerInput>().CanClimb = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<SimplePlayerInput>().CanClimb = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    // Update is called once per frame

}
