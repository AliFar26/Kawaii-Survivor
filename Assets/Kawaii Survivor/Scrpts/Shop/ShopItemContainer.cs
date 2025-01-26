using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemContainer : MonoBehaviour
{
    [Header(" Elemtns ")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;

    [Header("Stats")]
    [SerializeField] private Transform statContainerParent;
    [field: SerializeField] public Button PurchasebButton { get; private set; }

    [Header(" Color ")]
    [SerializeField] private Image[] levelDependentImage;
    [SerializeField] private Image outline;

    public void Configure(WeaponDataSO weaponData, int level)
    {
        icon.sprite = weaponData.Icon;
        nameText.text = weaponData.Name + "(lvl " + (level + 1) + ")";
        priceText.text = WeaponStatsCalculator.GetPurchasePrice(weaponData, level).ToString();

        Color imageColor = ColorHolder.GetColor(level);
        nameText.color = imageColor;

        outline.color = ColorHolder.GetOutlineColor(level); ;

        foreach (Image image in levelDependentImage)
            image.color = imageColor;

        Dictionary<Stat, float> calculatedStats = WeaponStatsCalculator.GetStats(weaponData, level);
        ConfigureStatContainers(calculatedStats);
    }

    public void Configure(ObjectDataSO objectData)
    {
        icon.sprite = objectData.Icon;
        nameText.text = objectData.Name ;
        priceText.text = objectData.Price.ToString();

        Color imageColor = ColorHolder.GetColor(objectData.Rarity);
        nameText.color = imageColor;

        outline.color = ColorHolder.GetOutlineColor(objectData.Rarity); ;

        foreach (Image image in levelDependentImage)
            image.color = imageColor;

        //Dictionary<Stat, float> calculatedStats = WeaponStatsCalculator.GetStats(objectData, level);
        ConfigureStatContainers(objectData.BaseStats);
    }

    private void ConfigureStatContainers(Dictionary<Stat, float> stats)
    {
        statContainerParent.Clear();

        StatContainerManager.GenerateStatContainers(stats, statContainerParent);
    }
}
