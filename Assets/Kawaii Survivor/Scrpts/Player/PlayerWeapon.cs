using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
   

    [Header("Elements")]
    [SerializeField] private WeaponPosition[] weaponPositions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void AddWeapon(WeaponDataSO selectedWeapon,int weaponLevel)
    //{
    //    //Debug.Log("We've selected " + selectedWeapon.Name + " With lvl " + weaponLevel);

    //    weaponPositions[Random.Range(0, weaponPositions.Length)].AssignWeapon(selectedWeapon.Prefab,weaponLevel);

    //}

    //public bool TryAddWeapon(WeaponDataSO Weapon, int level)
    //{
    //    for (int i = 0; i < weaponPositions.Length; i++)
    //    {
    //        if (weaponPositions[i].weapon != null)
    //        {
    //            Debug.Log("Its full");
    //            continue;

    //        }


    //        weaponPositions[i].AssignWeapon(Weapon.Prefab, level);
    //        return true;
    //    }
    //    return false;
    //}


    //public bool TryAddWeapon(WeaponDataSO Weapon, int level)
    //{
    //    for (int i = 0; i < weaponPositions.Length; i++)
    //    {
    //        // Check if the current position is empty
    //        if (weaponPositions[i].weapon == null)
    //        {
    //            Debug.Log($"Position {i}: {(weaponPositions[i].weapon == null ? "Empty" : "Full")}");

    //            weaponPositions[i].AssignWeapon(Weapon.Prefab, level);
    //            return true; // Weapon added successfully
    //        }
    //        else
    //        {
    //            Debug.Log("Position is full");
    //        }
    //    }

    //    // All positions are full, return false
    //    return false;
    //}



    public bool TryAddWeapon(WeaponDataSO Weapon, int level)
    {
        for (int i = 0; i < weaponPositions.Length; i++)
        {
            Debug.Log($"Checking position {i}: {(weaponPositions[i].weapon == null ? "Empty" : "Full")}");

            if (weaponPositions[i].weapon == null)
            {
                Debug.Log($"Assigning weapon to position {i}");
                weaponPositions[i].AssignWeapon(Weapon.Prefab, level);
                return true;
            }
            else
            {
                Debug.Log($"Position {i} is full");
            }
        }

        Debug.Log("No empty positions available");
        return false;
    }

    public Weapon[] GetWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();

        foreach (WeaponPosition weaponPosition in weaponPositions)
        {
            if (weaponPosition.weapon == null)
                continue;

            weapons.Add(weaponPosition.weapon);
        }



        return weapons.ToArray();
    }



}
