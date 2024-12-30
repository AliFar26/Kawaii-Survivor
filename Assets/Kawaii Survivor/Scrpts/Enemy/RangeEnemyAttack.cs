using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private EnemyBullet bulletPrefab;
    private Player player;

    [Header("Setting")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;

    [Header("Bullet Pooling")]
    private ObjectPool<EnemyBullet> bulletPool;
    // Start is called before the first frame update
    void Start()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = attackDelay;
        bulletPool = new ObjectPool<EnemyBullet> (CreateFunction, ActionOnGet, ActionOnRelease, actionOnDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private EnemyBullet CreateFunction()
    {
        EnemyBullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.Configure(this);
        return bulletInstance;
        //return Instantiate(bulletPrefab, transform);
    }
    private void ActionOnGet(EnemyBullet bullet)
    {
        bullet.Reload();
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
    }
    private void ActionOnRelease(EnemyBullet bullet)
    {

        bullet.gameObject.SetActive(false);
    }
    private void actionOnDestroy(EnemyBullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void ReleaseBullet(EnemyBullet bullet)
    {
        bulletPool.Release(bullet);
    }
    public void StorePlayer(Player player)
    {
        this.player = player;
    }
    public void AutoAim()
    {
       
            ManageShooting();
    }

    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackDelay) { 
        attackTimer = 0f;
            Shoot();
        }
    }

    
    private void Shoot()
    {
        Vector2 direction = (player.GetCenter() - (Vector2)shootingPoint.position).normalized;
        EnemyBullet bulletInstance = bulletPool.Get();
        bulletInstance.Shoot(damage, direction);
       


    }

    
}
