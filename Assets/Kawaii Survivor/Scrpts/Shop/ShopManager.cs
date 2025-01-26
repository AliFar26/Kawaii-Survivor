using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ShopManager : MonoBehaviour, IGameStateListener
{

    [Header("Element")]
    [SerializeField] private Transform containerParent;
    [SerializeField] private ShopItemContainer shopItemContainerPrefab;

    [Header("Reroll")]
    [SerializeField] private Button rerollButton;
    [SerializeField] private int rerollPrice;
    [SerializeField] private TextMeshProUGUI rerollPriceText;


    private void Awake()
    {
        CurrencyManager.onUpdated += CurrencyUpdatedCallback;
    }

    private void  OnDestroy()
    {
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
        CurrencyManager.instance.UseCoin(rerollPrice);
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
}
