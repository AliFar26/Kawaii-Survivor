using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponMerger : MonoBehaviour
{
    public static WeaponMerger instance;

    [Header("Elements")]
    [SerializeField] private PlayerWeapon PlayerWeapon;

    [Header("Setting")]
    private List<Weapon> weaponsToMerge = new List<Weapon>();


    [Header("Action")]
    public static Action <Weapon> onMerge;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool CanMerge(Weapon weapon)
    {
        if(weapon.Level >= 3)
            return false;


        weaponsToMerge.Clear();
        weaponsToMerge.Add(weapon);


        Weapon[] weapons = PlayerWeapon.GetWeapons();

        foreach (Weapon playerWeapon in weapons)
        {
            // We can't merge with a null weapon
            if(playerWeapon == null)
                continue;

            //we can't merge a weapon with itself
            if (playerWeapon == weapon)
                continue;

            //no the same weapon type
            if(playerWeapon.WeaponData.name != weapon.WeaponData.name)
                continue;

            //we can't merge sam weapon with different levels 
            if(playerWeapon.Level != weapon.Level)
                continue;

            weaponsToMerge.Add(playerWeapon);

            return true;
        }
        return false;
    }


    public void Merge()
    {
        if (weaponsToMerge.Count < 2 )
        {
            Debug.Log("Something went wrong here ...");
            return;
        }

        DestroyImmediate(weaponsToMerge[1].gameObject);

        weaponsToMerge[0].Upgrade();

        Weapon weapon = weaponsToMerge[0];
        weaponsToMerge.Clear();

        onMerge?.Invoke(weapon);
    }
}
