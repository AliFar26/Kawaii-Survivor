using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("Element")]
    private Player player;

    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned ;

    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadius;

    [Header("Debug")]
    [SerializeField] private bool gizmos;

    [Header("Debug")]
    [SerializeField] private ParticleSystem passAwayParticles;



    void Start()
    {
        player = FindFirstObjectByType<Player>(); ;
        if (player == null)
        {
            Destroy(gameObject);
        }

        //Hide the renderer
        renderer.enabled = false;
        
        //Show the spawn indicator
        spawnIndicator.enabled = true;


        //Scale up & down the spawn indicator
        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);



        //Show the enemy after 3 seconds
        //Hide the indicator

        //Prevent Followint & Attacking functionn during the spawn sequence


    }

    private void SpawnSequenceCompleted()
    {
        //Show the enemy after 3 seconds
        //Hide the indicator

        //Hide the renderer
        renderer.enabled = true;

        //Show the spawn indicator
        spawnIndicator.enabled = false;

        hasSpawned = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned)
            return;

        FollowPlayer();
        TryAttack();
    }


    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= playerDetectionRadius)
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
