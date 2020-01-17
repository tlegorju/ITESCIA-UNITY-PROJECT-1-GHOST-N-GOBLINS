using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestScoreRegisterer : MonoBehaviour
{
    private static BestScoreRegisterer instance;
    public static BestScoreRegisterer Instance { get { return instance; } }

    private int[] bestScoresArray=null;

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    public int[] GetBestScores()
    {
        if (bestScoresArray == null)
            LoadBestScores();

        return bestScoresArray;
    }

    public void UpdateBestScore(int newScore)
    {
        if(bestScoresArray==null)
            LoadBestScores();

        for (int i=0;i< bestScoresArray.Length; i++)
        {
            if(newScore> bestScoresArray[i])
            {
                int tmp = bestScoresArray[i];
                bestScoresArray[i] = newScore;
                newScore = tmp;
            }
            PlayerPrefs.SetInt("BEST_SCORE"+(i+1), bestScoresArray[i]);
        }
    }

    public bool CheckBestScoreBeaten(int newScore)
    {
        if (bestScoresArray == null)
            LoadBestScores();

        for (int i= bestScoresArray.Length-1; i>=0; i--)
        {
            if (bestScoresArray[i] < newScore)
                return true;
        }
        return false;
    }

    private void LoadBestScores()
    {
        if (bestScoresArray == null)
            bestScoresArray = new int[]{    PlayerPrefs.GetInt("BEST_SCORE1", 0),
                                            PlayerPrefs.GetInt("BEST_SCORE2", 0),
                                            PlayerPrefs.GetInt("BEST_SCORE3", 0)};
    }
}
