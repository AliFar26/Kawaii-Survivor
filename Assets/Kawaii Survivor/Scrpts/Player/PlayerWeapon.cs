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

   
    public bool TryAddWeapon(WeaponDataSO Weapon, int level)
    {
        for (int i = 0; i < weaponPositions.Length; i++)
        {
            Debug.Log($"Checking position {i}: {(weaponPositions[i].Weapon == null ? "Empty" : "Full")}");

            if (weaponPositions[i].Weapon == null)
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


    public void RecycleWeapon(int weaponIndex)
    {
        for (int i = 0; i < weaponPositions.Length; i++)
        {

            if (i != weaponIndex)
                continue;


            int recyclePrice = weaponPositions[i].Weapon.GetRecyclePrice();
            CurrencyManager.instance.AddCurrency( recyclePrice );

            weaponPositions[i].RemoveWeapon();

            return;
        }
    }

    public Weapon[] GetWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();

        foreach (WeaponPosition weaponPosition in weaponPositions)
        {
            if (weaponPosition.Weapon == null)
                weapons.Add(null);
            else
                weapons.Add(weaponPosition.Weapon);
            

        }

        return weapons.ToArray();
    }

   

}
