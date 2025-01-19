using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;


public class WeaponSelectionManager : MonoBehaviour ,IGameStateListener
{

    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private WeaponSelectionContainer weaponContainerPrefab;


    [Header("Data")]
    [SerializeField] private WeaponDataSO[] starterWeapon;



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

        WeaponDataSO weaponData = starterWeapon[UnityEngine.Random.Range(0, starterWeapon.Length)];

        containerInstance.Configure(weaponData.Icon , weaponData.Name);

        containerInstance.Button.onClick.RemoveAllListeners();
        containerInstance.Button.onClick.AddListener(() => WeaponSelectedCallback(containerInstance, weaponData));

    }

    private void WeaponSelectedCallback(WeaponSelectionContainer containerInstance, WeaponDataSO weaponData)
    {
        Debug.Log("Weapon name :" +  weaponData.Name);

        foreach ( WeaponSelectionContainer container in containersParent.GetComponentsInChildren<WeaponSelectionContainer>())
        {
            if (container == containerInstance)
                container.Select();
            else
                container.Deselect();
        }
    }
}
