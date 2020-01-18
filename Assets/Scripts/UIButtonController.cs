using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonController : MonoBehaviour
{
    public void GoToNextLevel()
    {
        if (SceneChangingManager.Instance)
            SceneChangingManager.Instance.GoToNextLevel();
    }

    public void ResumeGame()
    {
        Debug.Log("OUI");
        if (GameManager.Instance)
            GameManager.Instance.ResumeGame();
    }

    public void QuitLevel()
    {
        Debug.Log("NON");
        if (GameManager.Instance)
            GameManager.Instance.QuitGame();
    }

    public void QuitApplication()
    {
        Debug.Log("POUET");
        Application.Quit();
    }
}
