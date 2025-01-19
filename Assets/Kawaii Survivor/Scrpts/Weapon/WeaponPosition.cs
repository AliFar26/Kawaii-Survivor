using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{

    //[Header("Elements")]
    public Weapon weapon { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AssignWeapon(Weapon weapon)
    {
       weapon =  Instantiate(weapon, transform);

        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation= Quaternion.identity;


    }
}
