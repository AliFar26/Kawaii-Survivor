using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header("Component")]
    private EnemyMovement movement;

    [Header("Element")]
    private Player player;

    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned;

    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float playerDetectionRadius;
    private float attackDelay;
    private float attackTimer;

    [Header("Effect")]
    [SerializeField] private ParticleSystem passAwayParticles;

    [Header("Debug")]
    [SerializeField] private bool gizmos;


    void Start()
    {
        movement=GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>(); ;


        if (player == null)
        {
            Destroy(gameObject);
        }

        SetRenderersVisibility(false);
        StartSpawnSequence();
        attackDelay = 1f / attackFrequency;
        
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

        movement.StorePlayer(player);

    }

    private void SetRenderersVisibility(bool visible)
    {
  
        enemyRenderer.enabled = visible;
        spawnIndicator.enabled = !visible;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= attackDelay)
            TryAttack();
        else
            Wait();
    }

    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= playerDetectionRadius)
        {
            Attack();

            //PassAway();
        }

    }
    private void Attack()
    {
        
        attackTimer = 0f;
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
