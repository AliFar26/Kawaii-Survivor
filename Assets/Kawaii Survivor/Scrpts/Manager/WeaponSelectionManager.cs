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
    [SerializeField] private PlayerWeapon playerWeapons;


    [Header("Data")]
    [SerializeField] private WeaponDataSO[] starterWeapon;
    private WeaponDataSO selectedWeapon;
    private int initialWeaponLevel;




    public void OnGameStateChangedCallback(GameState gameState)
    {

        switch (gameState)
        {
            //case GameState.MENU:
            //    break;


            case GameState.WEAPONSELECTION:
                configure();
                break;


            case GameState.GAME:
                if (selectedWeapon == null) 
                    return;
                playerWeapons.AddWeapon(selectedWeapon,initialWeaponLevel);
                selectedWeapon = null;
                initialWeaponLevel = 0;

                break;
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

    [NaughtyAttributes.Button]
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

        int level = UnityEngine.Random.Range(0, 4);

        //Debug.Log("InitialWeaponLevel ==> " + initialWeaponLevel);
        //Debug.Log("LEVEL ==> " + level);


        containerInstance.Configure(weaponData,level);

        containerInstance.Button.onClick.RemoveAllListeners();
        containerInstance.Button.onClick.AddListener(() => WeaponSelectedCallback(containerInstance, weaponData,level));

    }

    private void WeaponSelectedCallback(WeaponSelectionContainer containerInstance, WeaponDataSO weaponData,int level)
    {
        Debug.Log("Weapon name :" +  weaponData.Name);

        selectedWeapon = weaponData;
        initialWeaponLevel = level;


        foreach ( WeaponSelectionContainer container in containersParent.GetComponentsInChildren<WeaponSelectionContainer>())
        {
            if (container == containerInstance)
                container.Select();
            else
                container.Deselect();
        }
    }
}
