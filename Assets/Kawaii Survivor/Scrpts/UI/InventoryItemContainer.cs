using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryItemContainer : MonoBehaviour
{

    [Header("Element")]
    [SerializeField] private Image container;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    public void Configure(Color containerColor, Sprite itemIcon)
    {
        container.color = containerColor;
        //icon.sprite = itemIcon.GetComponent<Image>().sprite;
        icon.sprite = itemIcon;
    }


    public void Configure(Weapon weapon, Action clickedCallback)
    {
        Color color = ColorHolder.GetColor(weapon.Level);
        Sprite icon = weapon.WeaponData.Icon;

        Configure(color, icon);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => clickedCallback?.Invoke());


    }



    public void Configure(ObjectDataSO objectDataSO)
    {
        Color color = ColorHolder.GetColor(objectDataSO.Rarity);
        Sprite icon = objectDataSO.Icon;

        Configure(color, icon);
    }


}
