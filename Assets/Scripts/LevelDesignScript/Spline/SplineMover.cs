using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineMover : MonoBehaviour
{
    public GameObject m_objectToMove; // the object we want to move along the spline
    public SplineComponent m_spline; // the spline which is going to set the path for the object
    public float secondsToWait;

    Coroutine MoveCoroutine; // a coroutine we gonna use to return the movement of the platform
    private Transform oTransform; // the transform of the moving object

    bool isClosed; // is the spline a loop ?

    bool isLaunched; // is a coroutine is launched ?

    
    private void Awake()
    {
        m_objectToMove.transform.position = m_spline.GetPoint(0); // we set our object's position to the first point of the spline
        oTransform = m_objectToMove.transform;
        isClosed = m_spline.closed;
    }
    IEnumerator MovePlatformForward()
    {
        
        isLaunched = true;

        yield return new WaitForSeconds(secondsToWait); // we wait in order to let the player jump on the platform 

        //if we want to increase the speed we need to change the value used to increase 
        for (float i = 0f; i <= 1f; i += 0.004f)
        {
            MoveCoroutine = StartCoroutine(MovingPlatform(i));
            yield return MoveCoroutine;
        }
        isLaunched = false;
    }
    IEnumerator MovePlatformBackward()
    {
        isLaunched = true;
        yield return new WaitForSeconds(secondsToWait);
        for (float i = 1; i > 0; i -= 0.004f)
        {
            MoveCoroutine = StartCoroutine(MovingPlatform(i));
            yield return MoveCoroutine;
        }
        isLaunched = false;
    }

    IEnumerator MovingPlatform(float splinePointPosition)
    {
        while (oTransform.position != m_spline.GetPoint(splinePointPosition))
        {
            oTransform.position = Vector3.MoveTowards(oTransform.position, m_spline.GetPoint(splinePointPosition), Time.deltaTime);
        }
        yield return null;
    }
    private void Update()
    {
        CheckSplineStatus();
    }
    private void CheckSplineStatus()
    {
        if (isClosed)
        {
            if (IsVectorEqual(oTransform.position, m_spline.GetPoint(0), 0.01f) && !isLaunched)
            {
                StartCoroutine(MovePlatformForward());
            }
        }
        else
        {
            if (IsVectorEqual(oTransform.position, m_spline.GetPoint(1), 0.1f) && !isLaunched)
            {
                StartCoroutine(MovePlatformBackward());
            }
            if (IsVectorEqual(oTransform.position, m_spline.GetPoint(0), 0.1f) && !isLaunched)
            {
                StartCoroutine(MovePlatformForward());
            }
        }
    }
    public bool IsVectorEqual(Vector3 a, Vector3 b, float error)
    {
        return Vector3.SqrMagnitude(a - b) < error;
    }
}
