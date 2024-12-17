using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("Element")]
    private Player player;

    [Header("Setting")]
    [SerializeField] private float moveSpeed;

    void Start()
    {
        player = FindFirstObjectByType<Player>(); ;
        if (player == null)
        {
            Destroy(gameObject);


        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }
}
