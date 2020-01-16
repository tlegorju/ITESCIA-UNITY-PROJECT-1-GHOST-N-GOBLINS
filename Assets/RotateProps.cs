using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateProps : MonoBehaviour
{
    public float rotateSpeed;
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0,0,rotateSpeed*Time.deltaTime));
    }
}
