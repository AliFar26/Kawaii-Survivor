using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Stats", menuName = "Scriptable Objects/Weapon Stats", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    //[field: SerializeField] public int RecyclePrice { get; private set; }
    [field: SerializeField] public Weapon Prefab { get; private set; }

    //[field: SerializeField] public Bullet bulletPrefab { get; private set; }

    //[field: SerializeField] public AnimatorOverrideController AnimatorOverride { get; private set; }
    //[field: SerializeField] public AudioClip AttackSound { get; private set; }


    [NaughtyAttributes.HorizontalLine]
    [SerializeField][Tooltip("Percent")] private float attack;
    [SerializeField][Tooltip("Percent")] private float attackSpeed;
    [SerializeField][Tooltip("Percent")] private float criticalChance;
    [SerializeField][Tooltip("Addend")] private float criticalPercent;
    [SerializeField][Tooltip("Addend")] private float range;

    public Dictionary<Stat, float> BaseStats
    {
        get
        {
            return new Dictionary<Stat, float>
            {
                { Stat.Attack, attack},
                { Stat.AttackSpeed, attackSpeed},
                { Stat.CriticalChance, criticalChance},
                { Stat.CriticalPercent, criticalPercent},
                { Stat.Range, range},
           
            };

        }

        private set { }
    }
}