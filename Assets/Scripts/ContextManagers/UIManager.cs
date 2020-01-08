using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    [SerializeField] GameObject pausePanel, gameOverPanel;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        GameManager tmpGM = GameManager.Instance;
        if(tmpGM)
        {
            tmpGM.OnGamePaused += PauseGame;
            tmpGM.OnGameResumed += ResumeGame;
            tmpGM.OnGameOver += ShowGameOver;
        }
    }

    private void PauseGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    private void ResumeGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    private void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
}
