using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour, IGameStateListener
{

    [Header("Element")]
    [SerializeField] private Transform containerParent;
    [SerializeField] private ShopItemContainer shopItemContainerPrefab;


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
        containerParent.Clear();

        int containersToAdd = 6;
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

}
