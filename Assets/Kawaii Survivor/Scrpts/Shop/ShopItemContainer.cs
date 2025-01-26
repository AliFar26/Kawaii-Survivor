using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Runtime.CompilerServices;

public class ShopItemContainer : MonoBehaviour
{
    [Header(" Elemtns ")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;

    [Header("Stats")]
    [SerializeField] private Transform statContainerParent;
    [SerializeField] public Button purchasebButton;

    [Header(" Color ")]
    [SerializeField] private Image[] levelDependentImage;
    [SerializeField] private Image outline;

    [Header("Lock Elements")]
    [SerializeField] private Image lockImage;
    [SerializeField] private Sprite lockedSprite, unlockedSprite;
    public bool isLocked {  get; private set; }


    [Header("Action")]
    public static Action<ShopItemContainer, int> onPurchase;

    //[Header("Purchase")]
    public WeaponDataSO WeaponData {  get; private set; }
    public ObjectDataSO ObjectData {  get; private set; }

    private int weaponLevel;


    private void Awake()
    {
        CurrencyManager.onUpdated += CurrencyUpdatedCallback;
    }

    private void OnDestroy()
    {
        CurrencyManager.onUpdated -= CurrencyUpdatedCallback;
        
    }

    private void CurrencyUpdatedCallback()
    {
        int itemPrice;

        if (WeaponData != null)
            itemPrice = WeaponStatsCalculator.GetPurchasePrice(WeaponData, weaponLevel);
        else
            itemPrice = ObjectData.Price;

        purchasebButton.interactable = CurrencyManager.instance.HasEnoughCurrency(itemPrice);
    }

    public void Configure(WeaponDataSO weaponData, int level)
    {
        weaponLevel = level;
        WeaponData = weaponData;

        icon.sprite = weaponData.Icon;
        nameText.text = weaponData.Name + "(lvl " + (level + 1) + ")";

        int weaponPrice = WeaponStatsCalculator.GetPurchasePrice(weaponData, level);
        priceText.text = weaponPrice.ToString();

        Color imageColor = ColorHolder.GetColor(level);
        nameText.color = imageColor;

        outline.color = ColorHolder.GetOutlineColor(level); ;

        foreach (Image image in levelDependentImage)
            image.color = imageColor;

        Dictionary<Stat, float> calculatedStats = WeaponStatsCalculator.GetStats(weaponData, level);
        ConfigureStatContainers(calculatedStats);

        purchasebButton.onClick.AddListener(Purchase);

        purchasebButton.interactable = CurrencyManager.instance.HasEnoughCurrency(weaponPrice);

    }

    public void Configure(ObjectDataSO objectData)
    {

        ObjectData = objectData;
        icon.sprite = objectData.Icon;
        nameText.text = objectData.Name ;
        priceText.text = objectData.Price.ToString();

        Color imageColor = ColorHolder.GetColor(objectData.Rarity);
        nameText.color = imageColor;

        outline.color = ColorHolder.GetOutlineColor(objectData.Rarity); ;

        foreach (Image image in levelDependentImage)
            image.color = imageColor;


        ConfigureStatContainers(objectData.BaseStats);


        purchasebButton.onClick.AddListener(Purchase);
        purchasebButton.interactable = CurrencyManager.instance.HasEnoughCurrency(objectData.Price);

    }

    private void ConfigureStatContainers(Dictionary<Stat, float> stats)
    {
        statContainerParent.Clear();

        StatContainerManager.GenerateStatContainers(stats, statContainerParent);
    }

    private void Purchase()
    {
        onPurchase?.Invoke(this, weaponLevel);

    }

    public void LockButtonCallback()
    {
        isLocked = !isLocked;
        UpdateLockVisuals();
    }

    private void UpdateLockVisuals()
    {
        lockImage.sprite = isLocked ? lockedSprite : unlockedSprite;
    }
}
