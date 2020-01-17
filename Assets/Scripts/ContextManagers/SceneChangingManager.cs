using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangingManager : MonoBehaviour
{
    private static SceneChangingManager instance;
    public static SceneChangingManager Instance { get { return instance; } }



    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
