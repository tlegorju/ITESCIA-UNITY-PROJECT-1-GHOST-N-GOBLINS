﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState { GameIdle, GamePlaying, GamePaused, GameStopped};

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public event Action OnGameStarted = delegate { },
                        OnGamePaused = delegate { },
                        OnGameResumed = delegate { },
                        OnLevelWon = delegate { },
                        OnGameOver = delegate { },
                        OnGameQuitted = delegate { };
           

    private GameState gameState = GameState.GameIdle;

    private GameObject playerRef;
    public GameObject PlayerRef { get { return playerRef; } set { playerRef = value; } }

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

    // Start is called before the first frame update
    void Start()
    {
        OnGameStarted();
        gameState = GameState.GamePlaying;

        TimerManager tmpTimeManager = TimerManager.Instance;
        if(tmpTimeManager)
            tmpTimeManager.OnTimerReachedZero += GameOver;

        playerRef = GameObject.FindGameObjectWithTag("Player");
        if (!playerRef)
        {
            Debug.Log("Error GameManager: No Player found");
            return;
        }
        SimplePlayerController tmpPlayerController = playerRef.GetComponent<SimplePlayerController>();
        if (tmpPlayerController)
            tmpPlayerController.OnDies += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        ///Input management might have to be moved in a script of its own
        if(Input.GetButtonDown("Cancel"))
        {
            if (gameState == GameState.GamePaused)
                ResumeGame();
            else if (gameState == GameState.GamePlaying)
                PauseGame();
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            LevelWon();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        OnGameResumed();
        gameState = GameState.GamePlaying;
    }

    public void PauseGame()
    {
        Time.timeScale = 0.01f;
        OnGamePaused();
        gameState = GameState.GamePaused;
    }

    public void LevelWon()
    {
        OnLevelWon();

        if (SceneChangingManager.Instance)
            SceneChangingManager.Instance.GoToNextLevel();
    }

    public void GameOver()
    {
        //Time.timeScale = 0.01f;
        OnGameOver();
        gameState = GameState.GameStopped;

    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        OnGameQuitted();
        gameState = GameState.GameStopped;

        Destroy(playerRef);
        if (SceneChangingManager.Instance)
            SceneChangingManager.Instance.GoToMainMenu();

        Destroy(gameObject);
        //Temporary
        //Application.Quit();
    }
}
