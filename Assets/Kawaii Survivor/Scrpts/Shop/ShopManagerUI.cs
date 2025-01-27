using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManagerUI : MonoBehaviour
{

    [Header("Player Stat Element")]
    [SerializeField] private RectTransform playerStatsPanel;
    [SerializeField] private GameObject playerStatsClosePanel;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;

        ConfigurePlayerStatsPanel();
    }

    private void ConfigurePlayerStatsPanel()
    {
        float width = Screen.width / (4 * playerStatsPanel.lossyScale.x);
        playerStatsPanel.offsetMax = playerStatsPanel.offsetMax.With(x : width);

    }

    // Update is called once per frame
    void Update()
    {

    }

    [NaughtyAttributes.Button]
    public void ShowPlayerStats()
    {
        playerStatsPanel.gameObject.SetActive(true);
        playerStatsClosePanel.SetActive(true);
    }

    [NaughtyAttributes.Button]
    public void HidePlayerStats()
    {
        playerStatsPanel.gameObject.SetActive(false);
        playerStatsClosePanel.SetActive(false);

    }
}
