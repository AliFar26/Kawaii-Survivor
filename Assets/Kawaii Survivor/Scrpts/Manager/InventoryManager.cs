using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour,IGameStateListener
{
    [Header("Player Components")]
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private PlayerObjects PlayerObjects;

    [Header("Element")]
    [SerializeField] private Transform inventoryItemParent;
    [SerializeField] private InventoryItemContainer inventoryItemContainer;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.SHOP)
            Configure();
    }


    private void Configure()
    {
        inventoryItemParent.Clear();

        Weapon[] weapons = playerWeapon.GetWeapons();

        for (int i = 0; i < weapons.Length; i++)
        {
            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemParent);

            Color containerColor = ColorHolder.GetColor(weapons[i].Level);
            Sprite icon = weapons[i].WeaponData.Icon;

            container.Configure(containerColor, icon);

        }


        ObjectDataSO[] objectDatas = PlayerObjects.Objects.ToArray();

        for (int i = 0; i <objectDatas.Length; i++)
        {
            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemParent);
             
            Color containerColor = ColorHolder.GetColor(objectDatas[i].Rarity);
            Sprite icon = objectDatas[i].Icon;

            container.Configure(containerColor, icon);


        }
    }

   
}
