using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScoresUIUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] bestScoresLabel;

    // Start is called before the first frame update
    void Start()
    {
        if(BestScoreRegisterer.Instance==null)
        {
            Debug.Log("Bestscores : missing best scores manager");
            return;
        }
        UpdateBestScoresLabels(BestScoreRegisterer.Instance.GetBestScores());
    }

    private void UpdateBestScoresLabels(int[] scores)
    {
        for(int i=0; i<bestScoresLabel.Length && i<scores.Length; i++)
        {
            if (bestScoresLabel[i] == null)
                continue;

            bestScoresLabel[i].text = "" + scores[i];
        }
    }

}
