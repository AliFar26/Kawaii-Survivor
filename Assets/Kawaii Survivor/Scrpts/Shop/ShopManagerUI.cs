using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerUI : MonoBehaviour
{

    [Header("Player Stat Element")]
    [SerializeField] private RectTransform playerStatsPanel;
    [SerializeField] private RectTransform playerStatsClosePanel;
    private Vector2 playrStatsOpenedPos;
    private Vector2 playrStatsClosedPos;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;

        ConfigurePlayerStatsPanel();
    }

    private void ConfigurePlayerStatsPanel()
    {
        float width = Screen.width / (4 * playerStatsPanel.lossyScale.x);
        playerStatsPanel.offsetMax = playerStatsPanel.offsetMax.With(x: width);

        playrStatsOpenedPos = playerStatsPanel.anchoredPosition;
        playrStatsClosedPos = playrStatsOpenedPos - Vector2.right * width;

        playerStatsPanel.anchoredPosition = playrStatsClosedPos;

        HidePlayerStats();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [NaughtyAttributes.Button]
    public void ShowPlayerStats()
    {
        playerStatsPanel.gameObject.SetActive(true);
        playerStatsClosePanel.gameObject.SetActive(true);
        playerStatsClosePanel.GetComponent<Image>().raycastTarget = true;

        LeanTween.cancel(playerStatsPanel);
        LeanTween.move(playerStatsPanel, playrStatsOpenedPos , .5f).setEase(LeanTweenType.easeInCubic);

        LeanTween.cancel(playerStatsClosePanel);
        LeanTween.alpha(playerStatsClosePanel, 0.8f , 0.5f).setRecursive(false);
    }

    [NaughtyAttributes.Button]
    public void HidePlayerStats()
    {
        //playerStatsPanel.gameObject.SetActive(false);
        //playerStatsClosePanel.gameObject.SetActive(false);

        playerStatsClosePanel.GetComponent<Image>().raycastTarget = false;

        LeanTween.cancel(playerStatsPanel);
        LeanTween.move(playerStatsPanel, playrStatsClosedPos, .5f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(() => playerStatsPanel.gameObject.SetActive(false));

        LeanTween.cancel(playerStatsClosePanel);
        LeanTween.alpha(playerStatsClosePanel, 0, 0.5f).setRecursive(false)
            .setOnComplete(() => playerStatsClosePanel.gameObject.SetActive(false));

    }
}
