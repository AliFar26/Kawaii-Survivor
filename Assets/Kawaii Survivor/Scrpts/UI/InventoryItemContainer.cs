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

    public int Index { get; private set; }
    public Weapon Weapon { get; private set; }
    public ObjectDataSO ObjectData { get; private set; }
    public void Configure(Color containerColor, Sprite itemIcon)
    {
        container.color = containerColor;
        //icon.sprite = itemIcon.GetComponent<Image>().sprite;
        icon.sprite = itemIcon;
    }


    public void Configure(Weapon weapon,int index, Action clickedCallback)
    {
       
        Weapon = weapon;
        Index = index;

        Color color = ColorHolder.GetColor(weapon.Level);
        Sprite icon = weapon.WeaponData.Icon;

        Configure(color, icon);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => clickedCallback?.Invoke());


    }



    public void Configure(ObjectDataSO objectDataSO, Action clickedCallback)
    {
        ObjectData = objectDataSO;

        Color color = ColorHolder.GetColor(objectDataSO.Rarity);
        Sprite icon = objectDataSO.Icon;

        Configure(color, icon);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => clickedCallback?.Invoke());
    }


}
