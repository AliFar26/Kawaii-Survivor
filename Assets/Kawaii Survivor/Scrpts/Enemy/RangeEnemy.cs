using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

[RequireComponent(typeof(EnemyMovement),typeof(RangeEnemyAttack))]
public class RangeEnemy : MonoBehaviour
{

    [Header("Component")]
    private EnemyMovement movement;
    private RangeEnemyAttack attack;


    [Header("Health")]
    [SerializeField] private int maxHealth;
    private int health;

    [Header("Element")]
    private Player player;

    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    [SerializeField] private Collider2D collider;
    private bool hasSpawned;

    [Header("Attack")]
    [SerializeField] private float playerDetectionRadius;

    [Header("Action")]
    public static Action<int, Vector2> onDamageTaken;

    [Header("Effect")]
    [SerializeField] private ParticleSystem passAwayParticles;

    [Header("Debug")]
    [SerializeField] private bool gizmos;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<RangeEnemyAttack>();
        player = FindFirstObjectByType<Player>(); ;

        attack.StorePlayer(player);


        if (player == null)
        {
            Destroy(gameObject);
        }

        SetRenderersVisibility(false);
        StartSpawnSequence();
    }

    private void StartSpawnSequence()
    {
        //Scale up & down the spawn indicator
        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);


    }

    private void SpawnSequenceCompleted()
    {
        //Show the enemy after 3 seconds
        //Hide the indicator

        SetRenderersVisibility(true);
        hasSpawned = true;

        collider.enabled = true;

        movement.StorePlayer(player);

    }

    private void SetRenderersVisibility(bool visible)
    {

        renderer.enabled = visible;
        spawnIndicator.enabled = !visible;
    }

    // Update is called once per frame
    void Update()
    {

        if (!renderer.enabled)
            return;

        ManageAttack();
      
    }

    private void ManageAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > playerDetectionRadius)
        {
            movement.FollowPlayer();
        }
        else
        {
            TryAttack();
        }
    }

  

    private void TryAttack()
    {
        attack.AutoAim();


    }
    

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        onDamageTaken?.Invoke(damage, transform.position);

        if (health <= 0)
        {
            PassAway();
        }
    }

    private void PassAway()
    {
        //Unparent the particle & play them
        passAwayParticles.transform.SetParent(null);
        passAwayParticles.Play();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);




    }
}
