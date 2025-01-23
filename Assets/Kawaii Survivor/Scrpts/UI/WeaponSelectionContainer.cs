using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.VisionOS;
using System;

public class WeaponSelectionContainer : MonoBehaviour
{
    [Header(" Elemtns ")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Stats")]
    [SerializeField] private Transform statContainerParent;
    // DELETED //[SerializeField] private StatContainer statContainerPrefab;
    // DELETED //[SerializeField] private Sprite statIcon;

    private WeaponDataSO weaponData;

    [field: SerializeField] public Button Button { get; private set; }

    [Header(" Color ")]
    [SerializeField] private Image[] levelDependentImage;
    [SerializeField] private Image outline;
    public void Configure(WeaponDataSO weaponData, int level)
    {
        icon.sprite = weaponData.Icon;
        nameText.text = weaponData.Name + "(lvl " + (level + 1) + ")";

        Color imageColor = ColorHolder.GetColor(level);
        nameText.color = imageColor;

        outline.color = ColorHolder.GetOutlineColor(level); ;

        foreach (Image image in levelDependentImage)
            image.color = imageColor;

    Dictionary<Stat,float> calculatedStats = WeaponStatsCalculator.GetStats(weaponData,level);
        ConfigureStatContainers(calculatedStats);
    }



    private void ConfigureStatContainers(Dictionary<Stat, float> calculatedStats)
    {
        StatContainerManager.GenerateStatContainers(calculatedStats, statContainerParent);
    }

    public void Select()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.075f , .3f).setEase(LeanTweenType.easeInOutSine);
    }


    public void Deselect()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, .3f);
    }
}
