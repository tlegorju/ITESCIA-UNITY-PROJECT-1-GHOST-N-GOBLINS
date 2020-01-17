using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance { get { return instance; } }

    //The score is always an integer number
    private int currentScore=0;
    public int CurrentScore { get { return currentScore; } }

    public event Action OnScoreChanged = delegate { };

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameOver += UpdateScoreOnGameOver;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        OnScoreChanged();
    }

    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChanged();
    }

    private void UpdateScoreOnGameOver()
    {
        if(BestScoreRegisterer.Instance!=null)
        {
            if(BestScoreRegisterer.Instance.CheckBestScoreBeaten(currentScore))
            {
                Debug.Log("Registered score " + currentScore);
                BestScoreRegisterer.Instance.UpdateBestScore(currentScore);
                currentScore = 0;
            }
        }
    }
}
