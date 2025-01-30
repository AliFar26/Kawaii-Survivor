//using System;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour,IGameStateListener
{
    [Header("Player Components")]
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private PlayerObjects PlayerObjects;

    [Header("Element")]
    [SerializeField] private Transform inventoryItemParent;
    [SerializeField] private Transform PauseInventoryItemParent;
    [SerializeField] private InventoryItemContainer inventoryItemContainer;
    [SerializeField] private ShopManagerUI ShopManagerUI;
    [SerializeField] private InventoryItemInfo itemInfo;




    private void Awake()
    {
        ShopManager.onItemPurchased += ItemPurchasedCallback;
        WeaponMerger.onMerge += WeaponMergedCallback;

        GameManager.onGamePaused += Configure;
    }

    private void OnDestroy()
    {
        ShopManager.onItemPurchased -= ItemPurchasedCallback;
        WeaponMerger.onMerge -= WeaponMergedCallback;

        GameManager.onGamePaused -= Configure;
    }

   

    public void OnGameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.SHOP)
            Configure();
    }


    private void Configure()
    {
        inventoryItemParent.Clear();
        PauseInventoryItemParent.Clear();

        Weapon[] weapons = playerWeapon.GetWeapons();

        for (int i = 0; i < weapons.Length; i++)
        {

            if (weapons[i] == null)
                continue;



            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemParent);
            container.Configure(weapons[i],i,() => ShowItemInfo(container));
            
            InventoryItemContainer pauseContainer = Instantiate(inventoryItemContainer, PauseInventoryItemParent);
            pauseContainer.Configure(weapons[i],i,null);

        }


        ObjectDataSO[] objectDatas = PlayerObjects.Objects.ToArray();

        for (int i = 0; i <objectDatas.Length; i++)
        {
            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemParent);
            container.Configure(objectDatas[i], () => ShowItemInfo(container));

            InventoryItemContainer pauseContainer = Instantiate(inventoryItemContainer, PauseInventoryItemParent);
            pauseContainer.Configure(objectDatas[i], null);

            ObjectDataSO objectData = objectDatas[i];


        }
    }

    private void ShowItemInfo(InventoryItemContainer container)
    {
        if(container.Weapon != null)
            ShowWeaponInfo(container.Weapon,container.Index);
        else
            ShowObjectInfo(container.ObjectData);
    }

    private void ShowWeaponInfo(Weapon weapon,int index)
    {
        itemInfo.Configure(weapon);
        ShopManagerUI.ShowItemInfo();

        itemInfo.RecycleButton.onClick.RemoveAllListeners();
        itemInfo.RecycleButton.onClick.AddListener(() => RecycleWeapon(index));

        ShopManagerUI.ShowItemInfo();
    }

    private void RecycleWeapon(int index)
    {
        playerWeapon.RecycleWeapon(index);

        Configure();

        ShopManagerUI.HideItemInfo();

        Debug.Log("Recycling weapin at index " + index);
    }
    private void ShowObjectInfo(ObjectDataSO objectData)
    {
        itemInfo.Configure(objectData);

        itemInfo.RecycleButton.onClick.RemoveAllListeners();
        itemInfo.RecycleButton.onClick.AddListener(() => RecycleObject(objectData));

        ShopManagerUI.ShowItemInfo();

    }

    private void RecycleObject(ObjectDataSO objectData)
    {
        // Destryo the inventory item container
        PlayerObjects.RecycleObject(objectData);
        //Close the item info
        Configure();
        //Remove the objectform playerObjects
        ShopManagerUI.HideItemInfo();
    }

    private void ItemPurchasedCallback() => Configure();


    private void WeaponMergedCallback(Weapon mergedWeapon)
    {
        Configure();
        itemInfo.Configure(mergedWeapon);

    }


}
