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

    public void AddWeapon(WeaponDataSO selectedWeapon,int weaponLevel)
    {
        //Debug.Log("We've selected " + selectedWeapon.Name + " With lvl " + weaponLevel);

        weaponPositions[Random.Range(0, weaponPositions.Length)].AssignWeapon(selectedWeapon.Prefab);

    }


}
