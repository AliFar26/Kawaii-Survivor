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


    [Header("Inventory Element")]
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private RectTransform inventoryClosePanel;
    private Vector2 inventoryOpenedPos;
    private Vector2 inventoryClosedPos;

    [Header("Item Info Slide Panel")]
    [SerializeField] private RectTransform itemInfoSlidePanel;
    private Vector2 itemInfoOpenedPos;
    private Vector2 itemInfoClosedPos;





    IEnumerator Start()
    {
        yield return null;

        ConfigurePlayerStatsPanel();
        ConfigureInventoryPanel();
        ConfigureItemInfoPanel();
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

        playerStatsClosePanel.GetComponent<Image>().raycastTarget = false;

        LeanTween.cancel(playerStatsPanel);
        LeanTween.move(playerStatsPanel, playrStatsClosedPos, .5f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(() => playerStatsPanel.gameObject.SetActive(false));

        LeanTween.cancel(playerStatsClosePanel);
        LeanTween.alpha(playerStatsClosePanel, 0, 0.5f).setRecursive(false)
            .setOnComplete(() => playerStatsClosePanel.gameObject.SetActive(false));

    }

    private void ConfigureInventoryPanel()
    {
        float width = Screen.width / (4 * inventoryPanel.lossyScale.x);
        inventoryPanel.offsetMin = inventoryPanel.offsetMin.With(x: -width);

        inventoryOpenedPos = inventoryPanel.anchoredPosition;
        inventoryClosedPos = inventoryOpenedPos + Vector2.right * width;

        inventoryPanel.anchoredPosition = inventoryClosedPos;

        HideInventory(false);
    }


    [NaughtyAttributes.Button]
    public void ShowInventory()
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryClosePanel.gameObject.SetActive(true);
        inventoryClosePanel.GetComponent<Image>().raycastTarget = true;

        LeanTween.cancel(inventoryPanel);
        LeanTween.move(inventoryPanel, inventoryOpenedPos, .5f).setEase(LeanTweenType.easeInCubic);

        LeanTween.cancel(inventoryClosePanel);
        LeanTween.alpha(inventoryClosePanel, 0.8f, 0.5f).setRecursive(false);
    }


    [NaughtyAttributes.Button]
    public void HideInventory( bool hideItemInfo = true)
    {

        inventoryClosePanel.GetComponent<Image>().raycastTarget = false;

        LeanTween.cancel(inventoryPanel);
        LeanTween.move(inventoryPanel, inventoryClosedPos, .5f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(() => inventoryPanel.gameObject.SetActive(false));

        LeanTween.cancel(inventoryClosePanel);
        LeanTween.alpha(inventoryClosePanel, 0, 0.5f).setRecursive(false)
            .setOnComplete(() => inventoryClosePanel.gameObject.SetActive(false));

        if (hideItemInfo ) 
            HideItemInfo();
    }

    private void ConfigureItemInfoPanel()
    {
        float height = Screen.height / (2 * itemInfoSlidePanel.lossyScale.x);
        Debug.Log(Screen.height);
        Debug.Log(height);
        itemInfoSlidePanel.offsetMax = itemInfoSlidePanel.offsetMax.With(y:height);

        itemInfoOpenedPos = itemInfoSlidePanel.anchoredPosition;
        itemInfoClosedPos = itemInfoOpenedPos + Vector2.down * height;

        itemInfoSlidePanel.anchoredPosition = itemInfoClosedPos;

        itemInfoSlidePanel.gameObject.SetActive(false);


    }

    [NaughtyAttributes.Button]
    public void ShowItemInfo()
    {
        itemInfoSlidePanel.gameObject.SetActive(true);
        itemInfoSlidePanel.LeanCancel();
        itemInfoSlidePanel.LeanMove((Vector3)itemInfoOpenedPos, .3f)
            .setEase(LeanTweenType.easeOutCubic);
    }


    [NaughtyAttributes.Button]
    public void HideItemInfo()

    {
        itemInfoSlidePanel.LeanCancel();
        itemInfoSlidePanel.LeanMove((Vector3)itemInfoClosedPos, .3f)
            .setEase(LeanTweenType.easeInCubic)
            .setOnComplete(() => itemInfoSlidePanel.gameObject.SetActive(false));
    }


}
