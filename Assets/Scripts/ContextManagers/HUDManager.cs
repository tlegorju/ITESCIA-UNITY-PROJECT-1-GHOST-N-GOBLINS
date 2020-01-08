using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private static HUDManager instance;
    public static HUDManager Instance { get { return instance; } }

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI scoreText;

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
        ScoreManager tmpScoreManager = ScoreManager.Instance;
        if(tmpScoreManager)
            tmpScoreManager.OnScoreChanged += UpdateScoreText;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if(TimerManager.Instance)
        timerText.text = ""+((int)(TimerManager.Instance.TimerValue+1));
    }

    private void UpdateScoreText()
    {
        scoreText.text = "" + ScoreManager.Instance.CurrentScore;
    }
}
