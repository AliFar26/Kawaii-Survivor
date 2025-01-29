using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Diagnostics;
using System;

using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour, IGameStateListener
{

    [Header("Element")]
    [SerializeField] private Transform containerParent;
    [SerializeField] private ShopItemContainer shopItemContainerPrefab;

    [Header("Player Componant")]
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private PlayerObjects playerObjects;


    [Header("Reroll")]
    [SerializeField] private Button rerollButton;
    [SerializeField] private int rerollPrice;
    [SerializeField] private TextMeshProUGUI rerollPriceText;

    [Header("Action")]
    public static Action onItemPurchased;

    private void Awake()
    {
        ShopItemContainer.onPurchase += ItemPurchasedCallback;
        CurrencyManager.onUpdated += CurrencyUpdatedCallback;
    }

    private void  OnDestroy()
    {
        ShopItemContainer.onPurchase -= ItemPurchasedCallback;
        CurrencyManager.onUpdated -= CurrencyUpdatedCallback;
    }

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
        {
            Configure();
            UpdateRerollVisuals();
        }
    }

    private void Configure()
    {

        List<GameObject> toDestroy = new List<GameObject>();

        for (int i = 0; i < containerParent.childCount; i++)
        {
            ShopItemContainer container = containerParent.GetChild(i).GetComponent<ShopItemContainer>();

            if (!container.isLocked)
                toDestroy.Add(container.gameObject);
        }

        while (toDestroy.Count > 0)
        {
            Transform t = toDestroy[0].transform;
            t.SetParent(null);
            Destroy(t.gameObject);
            toDestroy.RemoveAt(0);
        }

        int containersToAdd = 6 - containerParent.childCount;
        int weaponContainerCount = Random.Range(Mathf.Min(2, containersToAdd), containersToAdd);
        int objectContainerCount = containersToAdd - weaponContainerCount;

        for (int i = 0; i < weaponContainerCount; i++)
        {
            ShopItemContainer weaponContainerInstance = Instantiate(shopItemContainerPrefab, containerParent);
            WeaponDataSO randomWeapon = ResourcesManager.GetRandomWeapon();
            weaponContainerInstance.Configure(randomWeapon, Random.Range(0, 2));

        }
        for (int i = 0; i < objectContainerCount; i++)
        {
            ShopItemContainer objectContainerInstance = Instantiate(shopItemContainerPrefab, containerParent);
            ObjectDataSO randomObject = ResourcesManager.GetRandomObject();

            objectContainerInstance.Configure(randomObject);
        }
    }

    public void Reroll()
    {
        Configure();
        CurrencyManager.instance.UseCurrency(rerollPrice);
    }

    private void UpdateRerollVisuals()
    {
        rerollPriceText.text = rerollPrice.ToString();
        rerollButton.interactable = CurrencyManager.instance.HasEnoughCurrency(rerollPrice);

    }


    private void CurrencyUpdatedCallback()
    {
        UpdateRerollVisuals();
    }

    private void ItemPurchasedCallback(ShopItemContainer container , int weponLevel)
    {
        if (container.WeaponData != null)
            TryPurchaseWeapon(container, weponLevel);
        else
            PurchaseObject(container);
       

    }

    private void TryPurchaseWeapon(ShopItemContainer container, int weponLevel)
    {
        if (playerWeapon.TryAddWeapon(container.WeaponData, weponLevel))
        {
            int price = WeaponStatsCalculator.GetPurchasePrice(container.WeaponData,weponLevel);
            CurrencyManager.instance.UseCurrency(price);

            Destroy(container.gameObject);
        }

        onItemPurchased?.Invoke();

    }

    private void PurchaseObject(ShopItemContainer container)
    {
        playerObjects.AddObject(container.ObjectData);
        CurrencyManager.instance.UseCurrency(container.ObjectData.Price);
        Destroy(container.gameObject);

        onItemPurchased?.Invoke();

    }
}
