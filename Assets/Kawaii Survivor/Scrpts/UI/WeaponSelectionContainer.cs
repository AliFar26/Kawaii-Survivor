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
    [SerializeField] private StatContainer statContainerPrefab;
    [SerializeField] private Sprite statIcon;

    private WeaponDataSO weaponData;

    [field: SerializeField] public Button Button { get; private set; }

    [Header(" Color ")]
    [SerializeField] private Image[] levelDependentImage;
    public void Configure(Sprite sprite, string name,int level,WeaponDataSO weaponData)
    {
        icon.sprite = sprite;
        nameText.text = name;

        Color imageColor = ColorHolder.GetColor(level);


        foreach(Image image in levelDependentImage)
            image.color = imageColor;

        ConfigureStatContainers(weaponData);
    }

    private void ConfigureStatContainers(WeaponDataSO weaponData)
    {
        foreach (KeyValuePair<Stat, float> kvp in weaponData.BaseStats)
        {
            StatContainer containerInstance = Instantiate(statContainerPrefab, statContainerParent);
            containerInstance.Configure(statIcon,Enums.FormatStatName(kvp.Key),kvp.Value.ToString());
        }
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
