using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineMover : MonoBehaviour
{
    public GameObject m_objectToMove;
    public SplineComponent m_spline;
    // Update is called once per frame
    Coroutine MoveCoroutine;
    private Transform oTransform;
    bool isClosed;
    private void Awake()
    {
        m_objectToMove.transform.position = m_spline.GetPoint(0);
        oTransform = m_objectToMove.transform;
        isClosed = m_spline.closed;
    }
    private void Start()
    {
        StartCoroutine(MovePlatformForward());
    }
    IEnumerator MovePlatformForward()
    {
        //if we want to increase the speed we need to change the value used to increase i
        for (float i = 0f; i <= 1f; i += 0.004f)
        {

            MoveCoroutine = StartCoroutine(MovingPlatform(i));
            yield return MoveCoroutine;
        }

    }

    IEnumerator MovingPlatform(float splinePointPosition)
    {
        //Debug.Log(m_spline.GetPoint(splinePointPosition));
        // m_objectToMove.transform.position = m_spline.GetPoint(splinePointPosition);
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
            if (IsVectorEqual(oTransform.position, m_spline.GetPoint(0), 0.01f))
            {

                StartCoroutine(MovePlatformForward());
                
            }
        }
        else
        {
           if (IsVectorEqual(oTransform.position, m_spline.GetPoint(1), 0.1f))
            {
                // StopAllCoroutines();
                //Debug.Log("we arrived");
                StartCoroutine(MovePlatformBackward());
            }
            if (IsVectorEqual(oTransform.position, m_spline.GetPoint(0), 0.1f))
            {

                StartCoroutine(MovePlatformForward());
                /*for (float i = 0f; i <= 1f; i += 0.1f)
                {
                    MovePlat(i);    
                }*/
            }
        }
    }

    IEnumerator MovePlatformBackward()
    {
        //yield return new WaitForSeconds(1);
        for (float i = 1; i > 0; i -= 0.004f)
        {
            MoveCoroutine = StartCoroutine(MovingPlatform(i));
            yield return MoveCoroutine;
        }
    }

    public bool IsVectorEqual(Vector3 a, Vector3 b, float error)
    {
        return Vector3.SqrMagnitude(a - b) < error;
    }


    void MovePlat(float i)
    {
       // while (oTransform.position != m_spline.GetPoint(i))
        //{

            oTransform.position = Vector3.Lerp(oTransform.position, m_spline.GetPoint(i), Time.deltaTime);
        //}
    }
    
}
