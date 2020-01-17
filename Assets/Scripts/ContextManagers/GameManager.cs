using System;
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
                        OnGameOver = delegate { },
                        OnGameQuitted = delegate { };

    private GameState gameState = GameState.GameIdle;

    private GameObject playerRef;
    public GameObject PlayerRef { get { return playerRef; } }

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

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
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        OnGameResumed();
        gameState = GameState.GamePlaying;
    }

    public void PauseGame()
    {
        Debug.Log("pause");
        Time.timeScale = 0.01f;
        OnGamePaused();
        gameState = GameState.GamePaused;
    }

    public void GameOver()
    {
        //Time.timeScale = 0.01f;
        OnGameOver();
        gameState = GameState.GameStopped;

        Invoke("TempLoadMainMenu", 1.0f);
    }

    public void QuitGame()
    {
        OnGameQuitted();
        gameState = GameState.GameStopped;

        //Temporary
        //Application.Quit();
    }

    private void TempLoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
