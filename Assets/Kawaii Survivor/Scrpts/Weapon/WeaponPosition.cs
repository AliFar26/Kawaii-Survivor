using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{

    //[Header("Elements")]
    public Weapon Weapon { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public void AssignWeapon(Weapon weapon,int weaponLevel)
    //{
    //   weapon =  Instantiate(weapon, transform);

    //    weapon.transform.localPosition = Vector3.zero;
    //    weapon.transform.localRotation= Quaternion.identity;

    //    weapon.UpgradeTo(weaponLevel);
    //}


    public void AssignWeapon(Weapon weaponPrefab, int weaponLevel)
    {
        Weapon = Instantiate(weaponPrefab, transform); // Assign to the property

        Weapon.transform.localPosition = Vector3.zero;
        Weapon.transform.localRotation = Quaternion.identity;

        Weapon.UpgradeTo(weaponLevel);
    }

    public void RemoveWeapon()
    {
        Destroy(Weapon.gameObject);
        Weapon = null;
    }
}
