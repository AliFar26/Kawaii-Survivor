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
    [SerializeField] private ShopManagerUI ShopManagerUI;
    [SerializeField] private InventoryItemInfo itemInfo;




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

            container.Configure(weapons[i],() => ShowItemInfo(container));

        }


        ObjectDataSO[] objectDatas = PlayerObjects.Objects.ToArray();

        for (int i = 0; i <objectDatas.Length; i++)
        {
            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemParent);
             ObjectDataSO objectData = objectDatas[i];
            container.Configure(objectDatas[i], () => ShowItemInfo(container));


        }
    }

    private void ShowItemInfo(InventoryItemContainer container)
    {
        if(container.Weapon != null)
            ShowWeaponInfo(container.Weapon);
        else
            ShowObjectInfo(container.ObjectData);
    }

    private void ShowWeaponInfo(Weapon weapon)
    {
        itemInfo.Configure(weapon);
        ShopManagerUI.ShowItemInfo();

    }
    private void ShowObjectInfo(ObjectDataSO objectData)
    {
        itemInfo.Configure(objectData);
        ShopManagerUI.ShowItemInfo();

    }



}
