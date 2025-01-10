using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangeWeapon : Weapon
{
    [Header("Elements")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform shootingPoint;

    [Header("Pooling")]
    private ObjectPool<Bullet> bulletPool;


    void Start()
    {
        bulletPool = new ObjectPool<Bullet> (CreateFunction, ActionOnGet, ActionOnRelease, actionOnDestroy);

    }

    void Update()
    {
        AutoAim();
    }


    private Bullet CreateFunction()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.Configure(this);
        return bulletInstance;
        //return Instantiate(bulletPrefab, transform);
    }
    private void ActionOnGet(Bullet bullet)
    {
        bullet.Reload();
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
    }
    private void ActionOnRelease(Bullet bullet)
    {

        bullet.gameObject.SetActive(false);
    }
    private void actionOnDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
    public void ReleaseBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageShooting();
            return;
        }
            transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLurp);
        


    }

    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackDelay)
        {
            attackTimer = 0f;
            Shoot();
        }
    }

    private void Shoot()
    {
        int damage = GetDamage(out bool isCriticalHit);

        Bullet bulletInstance = bulletPool.Get();
        bulletInstance.Shoot(damage, transform.up, isCriticalHit);

    }

    public override void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        Debug.Log("Previous Damgage :" + damage);

        ConfigureStats();

        damage = Mathf.RoundToInt(damage * (1 + playerStatsManager.GetStatValue(Stat.Attack) / 100));

        Debug.Log("New Damage : " + damage);

    }
}
