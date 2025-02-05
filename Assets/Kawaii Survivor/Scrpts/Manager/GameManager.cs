using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;



    [Header(" Actons ")]
    public static Action onGamePaused;
    public static Action onGameResumed;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        SetGameState(GameState.MENU);
    }

   public void StartGame() => SetGameState(GameState.GAME);
   public void StartWeaponSelection() => SetGameState(GameState.WEAPONSELECTION);
   public void StartShop() => SetGameState(GameState.SHOP);

    public void SetGameState(GameState gameState)
    {
        IEnumerable<IGameStateListener> gameStateListeners = 
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IGameStateListener>();

        foreach (IGameStateListener gameStateListener in gameStateListeners)
            gameStateListener.OnGameStateChangedCallback(gameState);

        //if (gameState == GameState.GAMEOVER)
        //    ManageGameOver();

    }

   

    public void WaveCompletedCallback()
    {
        if (Player.instance.HasLevelUp() || WaveTransitionManager.instance.HasCollectedChest())
        {
            SetGameState(GameState.WAVETRANSITION);
        }
        else
        {

            SetGameState(GameState.SHOP);

        }
    }

    public void ManageGameOver()
    {
        SceneManager.LoadScene(0);
    }

    public void pauseButtonCallback()
    {
        Time.timeScale = 0;
        onGamePaused?.Invoke();
    }

    public void resumeButtonCallback()
    {
        Time.timeScale = 1;
        onGameResumed?.Invoke();
    }

    public void RestartFromPause()
    {
        Time.timeScale = 1;
        ManageGameOver();
    }

}

public interface IGameStateListener
{
    void OnGameStateChangedCallback(GameState gameState);
}