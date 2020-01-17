using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonController : MonoBehaviour
{


    public void ResumeGame()
    {
        if (GameManager.Instance)
            GameManager.Instance.ResumeGame();
    }

    public void QuitLevel()
    {

        if (GameManager.Instance)
            GameManager.Instance.QuitGame();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
