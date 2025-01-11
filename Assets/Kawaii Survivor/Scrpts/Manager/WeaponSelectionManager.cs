using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour ,IGameStateListener
{

    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private WeaponSelectionContainer weaponContainerPrefab;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameStateChangedCallback(GameState gameState)
    {

        switch (gameState)
        {
            //case GameState.MENU:
            //    break;


            case GameState.WEAPONSELECTION:
                configure();
                break;


            //case GameState.GAME:
            //    break;
            //case GameState.GAMEOVER:
            //    break;
            //case GameState.STAGECOMPLETE:
            //    break;
            //case GameState.WAVETRANSITION:
            //    break;
            //case GameState.SHOP:
            //    break;
            //default:
            //    break;
        }
    }

    private void configure()
    {
        // Clean our parent , no children
        containersParent.Clear();

        //Generate weapon containers
        for (int i = 0; i < 3; i++)
            GenerateWeaponContainer();
    }

    private void GenerateWeaponContainer()
    {
        WeaponSelectionContainer containerInstance = Instantiate(weaponContainerPrefab,containersParent);
    }
}
