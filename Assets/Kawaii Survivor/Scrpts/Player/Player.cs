using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth),typeof(PlayerLevel))]
public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("comppnent")]
    [SerializeField] private CircleCollider2D collider;
    private PlayerHealth playerHealth;
    private PlayerLevel playerLevel;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        playerHealth = GetComponent<PlayerHealth>();
        playerLevel = GetComponent<PlayerLevel>();

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }
    public Vector2 GetCenter()
    {
        return (Vector2)transform.position + collider.offset ;
    }

    public bool HasLevelUp()
    {
        return playerLevel.HasLevelUp();
    }
}
