using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour ,IPlayerStatsDependency
{
    [field: SerializeField] public WeaponDataSO WeaponData {  get; private set; }

    [Header("Setting")]
    [SerializeField] private float range;
    [SerializeField] protected LayerMask enemyMask;
    
    [Header("Attack")]
    [SerializeField] protected int baseDamage;
    protected int damage;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected Animator animator;
    protected float attackTimer;


    [Header("Animation")]
    [SerializeField] protected float aimLurp;

    void Start()
    {
        damage = baseDamage;
    }

    void Update()
    {
        
    }
    protected Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        if (enemies.Length <= 0)
            return null;
        
        float minDistance = range;
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();

            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;
            }

        }

        return closestEnemy;
    }

    protected int GetDamage(out bool isCriticalHit)
    {
        isCriticalHit = false;

        if (Random.Range(0, 101) <= 50)
        {
            isCriticalHit = true;
            return damage * 2;
        }

        return damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public abstract void UpdateStats(PlayerStatsManager playerStatsManager);
}
