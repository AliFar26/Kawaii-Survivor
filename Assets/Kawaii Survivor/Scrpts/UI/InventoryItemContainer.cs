using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemContainer : MonoBehaviour
{

    [Header("Element")]
    [SerializeField] private Image container;
    [SerializeField] private Image icon;

    public void Configure(Color containerColor, Sprite itemIcon)
    {
        container.color = containerColor;
        //icon.sprite = itemIcon.GetComponent<Image>().sprite;
        icon.sprite = itemIcon;
    }



}
