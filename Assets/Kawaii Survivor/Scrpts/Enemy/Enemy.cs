using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public  class Enemy : MonoBehaviour
{
    [Header("Component")]
    protected EnemyMovement movement;

    [Header("Health")]
    [SerializeField] protected int maxHealth;
    protected int health;

    [Header("Element")]
    protected Player player;

    [Header("Spawn Sequence Related")]
    [SerializeField] protected SpriteRenderer renderer;
    [SerializeField] protected SpriteRenderer spawnIndicator;
    [SerializeField] protected Collider2D collider;
    protected bool hasSpawned;

    [Header("Attack")]
    [SerializeField] protected float playerDetectionRadius;

    [Header("Action")]
    public static Action<int, Vector2,bool> onDamageTaken;
    public static Action< Vector2 > onPassedAway;

    [Header("Effect")]
    [SerializeField] protected ParticleSystem passAwayParticles;

    [Header("Debug")]
    [SerializeField] protected bool gizmos;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = maxHealth;
        movement = GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>();

        if (player == null)
        {
            Destroy(gameObject);
        }

        StartSpawnSequence();


    }

    // Update is called once per frame
     protected bool CanAttack()
    {
        return renderer.enabled;

    }



    private void StartSpawnSequence()
    {
        SetRenderersVisibility(false);
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

    public void TakeDamage(int damage, bool isCriticalHit)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        onDamageTaken?.Invoke(damage, transform.position, isCriticalHit);

        if (health <= 0)
        {
            PassAway();
        }
    }

    public void PassAway()
    {

        onPassedAway?.Invoke(transform.position);
        PassAwayAfterWave();
    }

    public void PassAwayAfterWave()
    {
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
