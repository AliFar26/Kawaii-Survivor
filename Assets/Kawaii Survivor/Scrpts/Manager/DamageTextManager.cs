using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private DamageText damageTextPrefab;

    [Header("Pooling")]
    private ObjectPool<DamageText> damageTextPool;

    private void Awake()
    {
        Enemy.onDamageTaken += EnemyHitCallback;
    }

    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyHitCallback;
    }

    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(CreateFunction, ActionOnGet,ActionOnRelease,actionOnDestroy);
    }

    private DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab,transform);
    }
    private void ActionOnGet(DamageText damagetext)
    {
        damagetext.gameObject.SetActive(true);
    }
    private void ActionOnRelease(DamageText damagetext)
    {

        damagetext.gameObject.SetActive(false);
    }
    private void actionOnDestroy(DamageText damagetext)
    {
        Destroy(damagetext.gameObject);
    }




    // Update is called once per frame
    void Update()
    {
       
    }
    [NaughtyAttributes.Button]
    private void EnemyHitCallback( int damage , Vector2 enemyPos)
    {
        DamageText damageTextInstance = damageTextPool.Get();

        Vector3 spawnPosition = enemyPos + Vector2.up * 1.5f;
        damageTextInstance.transform.position = spawnPosition;

        damageTextInstance.Animate(damage);

        LeanTween.delayedCall(1,() => damageTextPool.Release(damageTextInstance));
    }
}
