using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameStateListener
{
    [Header(" Panels ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject weaponSelectionPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject stageCompletePanel;
    [SerializeField] private GameObject waveTransitionPanel;
    [SerializeField] private GameObject shopPanel;
    //[SerializeField] private GameObject settingsPanel;
    //[SerializeField] private GameObject characterSelectionPanel;
    //[SerializeField] private GameObject pausePanel;

    [Header(" Pause Stuff ")]
    //[SerializeField] private GameObject restartConfirmationPanel;

    private List<GameObject> panels = new List<GameObject>();


    [Header(" Actions ")]
    public static Action onCharacterSelectionOpened;

    private void Awake()
    {
        panels.AddRange(new GameObject[]
        {
            menuPanel,
            weaponSelectionPanel,
            //settingsPanel,
            //characterSelectionPanel,
            gamePanel,
            //pausePanel,
            waveTransitionPanel,
            stageCompletePanel,
            gameoverPanel,
            shopPanel
        });

        //GameManager.onGamePaused += GamePausedCallback;
        //GameManager.onGameResumed += GameResumedCallback;
    }

    //private void OnDestroy()
    //{
    //    GameManager.onGamePaused -= GamePausedCallback;
    //    GameManager.onGameResumed -= GameResumedCallback;
    //}

    //private void GamePausedCallback()
    //{
    //    pausePanel.SetActive(true);
    //}

    //private void GameResumedCallback()
    //{
    //    pausePanel.SetActive(false);
    //}

    public void OnGameStateChangedCallback(GameState gameState)
    {

        switch (gameState)
        {
            case GameState.MENU:
                ShowPanel(menuPanel);
                break;

            case GameState.WEAPONSELECTION:
                ShowPanel(weaponSelectionPanel);
                break;

            case GameState.GAME:
                ShowPanel(gamePanel);
                break;

            case GameState.WAVETRANSITION:
                ShowPanel(waveTransitionPanel);
                break;

            case GameState.SHOP:
                ShowPanel(shopPanel);
                break;

            case GameState.GAMEOVER:
                ShowPanel(gameoverPanel);
                break;
            case GameState.STAGECOMPLETE:
                ShowPanel(stageCompletePanel);
                break;
        }
    }

   

    private void ShowPanel(GameObject panel)
    {
        foreach (GameObject p in panels)
            p.SetActive(p == panel);
    }

    //public void RestartFromPauseButton()
    //{
    //    restartConfirmationPanel.SetActive(true);
    //}

    //public void CloseRestartConfirmationPanel()
    //{
    //    restartConfirmationPanel.SetActive(false);
    //}

    //public void ShowSettings()
    //{
    //    settingsPanel.gameObject.SetActive(true);
    //}

    //public void HideSettings()
    //{
    //    settingsPanel.gameObject.SetActive(false);
    //}

    //public void ShowCharacterSelection()
    //{
    //    onCharacterSelectionOpened?.Invoke();

    //    characterSelectionPanel.SetActive(true);
    //}

    //public void HideCharacterSelection()
    //{
    //    characterSelectionPanel.SetActive(false);
    //}

    
}
