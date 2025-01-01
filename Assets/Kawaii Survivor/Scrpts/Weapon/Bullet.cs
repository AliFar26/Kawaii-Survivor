using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private Collider2D collider;
    private RangeWeapon rangeWeapon;

    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask enemyMask;
    private int damage;
    private bool isCriticalHit;
    private Enemy target;
    void Start()
    {
        //rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    public void Configure(RangeWeapon rangeWeapon)
    {
        this.rangeWeapon = rangeWeapon;
    }

    public void Reload()
    {
        target = null;

        rig.velocity = Vector2.zero;
        collider.enabled = true;
    }
    public void Shoot(int damage, Vector2 direction, bool isCriticalHit)
    {
        Invoke("Release", 1);

        this.damage = damage;
        this.isCriticalHit = isCriticalHit;

        transform.right = direction;
        rig.velocity = direction * moveSpeed;
    }

    private void Release()
    {
        if (!gameObject.activeSelf)
            return;
        rangeWeapon.ReleaseBullet(this);
    }
    private void OnTriggerEnter2D(Collider2D collider )
    {
        if (target != null)
            return;

        if (IsInLayerMask(collider.gameObject.layer , enemyMask))
        {
            target = collider.GetComponent<Enemy>();
            CancelInvoke();
            Attack(target);
            Release();
        }
    }

    

    private void Attack(Enemy enemy)
    {
        enemy.TakeDamage(damage,isCriticalHit);

    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1<< layer)) != 0;
    }
}
