using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{

    private static TimerManager instance;
    public static TimerManager Instance { get { return instance; } }


    [SerializeField] float timerStartValueInSeconds = 500;
    private float timerValue;
    public float TimerValue { get { return timerValue; } }

    public event Action OnTimerReachedZero = delegate { };

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timerValue = timerStartValueInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        timerValue -= Time.deltaTime;
        if (timerValue <= 0)
            OnTimerReachedZero();
    }
}
