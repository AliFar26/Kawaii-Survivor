using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    enum State
    {
        Idle,
        Attack
    }

    private State state;

    [Header("Element")]
    [SerializeField] private Transform hitDetectionTransform;
    [SerializeField] private float hitDetectionRadius;

    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackDelay;
    [SerializeField] private Animator animator;
    private float attackTimer;
    private List<Enemy> damagedEnemies = new List<Enemy>();


    [Header("Setting")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;

    [Header("Animation")]
    [SerializeField] private float aimLurp;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;

                case State.Attack:
                Attacking();
                break;
        }

        Attack();
    }
    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            ManageAttack();
        }

        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLurp);

        IncreamentAttackTimer();
       
    }

    private void ManageAttack()
    {
        

        if (attackTimer >= attackDelay) {
            attackTimer = 0;
            StartAttack();
        
        }
    }

    private void IncreamentAttackTimer()
    {
        attackTimer = Time.deltaTime;
    }



    [NaughtyAttributes.Button]
    private void StartAttack()
    {
        animator.Play("Attack");
        state = State.Attack;

        damagedEnemies.Clear();
    }
    private void Attacking()
    {
        Attack();
    }

    private void StopAttack()
    {
        state = State.Idle;
        damagedEnemies.Clear();
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetectionTransform.position, hitDetectionRadius, enemyMask);

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();

            if (!damagedEnemies.Contains(enemy)) { 
                enemy.TakeDamage(damage);
                damagedEnemies.Add(enemy);
            }
        }
        
    }


    private Enemy GetClosestEnemy()
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



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitDetectionTransform.position, hitDetectionRadius);

      
    }
}
