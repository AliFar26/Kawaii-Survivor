using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/New Character", order = 0)]
public class CharacterDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField] public int PurchasePrice { get; private set; }

    [NaughtyAttributes.HorizontalLine]
    [SerializeField][Tooltip("Percent")] private float attack;
    [SerializeField][Tooltip("Percent")] private float attackSpeed;
    [SerializeField][Tooltip("Percent")] private float criticalChance;
    [SerializeField][Tooltip("Addend")] private float criticalPercent;
    [SerializeField][Tooltip("Percent")] private float moveSpeed;
    [SerializeField][Tooltip("Addend")] private float maxHealth;
    [SerializeField][Tooltip("Addend")] private float range;
    [SerializeField][Tooltip("Frequency")] private float healthRecoverySpeed;
    [SerializeField][Tooltip("Percent")] private float armor;
    [SerializeField][Tooltip("Percent")] private float luck;
    [SerializeField][Tooltip("Percent")] private float dodge;
    [SerializeField][Tooltip("Percent")] private float lifeSteal;
}
