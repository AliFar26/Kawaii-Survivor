using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour,IGameStateListener
{

    [Header("Element")]
    [SerializeField] private Transform containerParent;
    [SerializeField] private GameObject shopItemContainerPrefab;

    
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

        for (int i = 0; i < containersToAdd; i++)
        {
            Instantiate(shopItemContainerPrefab, containerParent);
        }
    }

}
