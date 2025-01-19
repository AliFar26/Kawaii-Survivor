using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelectionContainer : MonoBehaviour
{
    [Header(" Elemtns ")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;


    public void Configure(Sprite sprite, string name)
    {
        icon.sprite = sprite;
        nameText.text = name;
    }
}
