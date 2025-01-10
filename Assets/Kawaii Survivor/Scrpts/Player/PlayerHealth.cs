using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;
public class PlayerHealth : MonoBehaviour,IPlayerStatsDependency
{
    [Header("Setting")]
    [SerializeField] private float baseMaxHealth;
     private float armor;
     private float maxHealth;
    private float health;
    private float dodge;
    private float healthRecoverySpeed;
    private float healthRecoveryTimer;
    private float healthRecoveryDuration;

    [Header("Element")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    private float lifeSteal;

    [Header("Action")]
    public static Action <Vector2> onAttackDodged;

    private void Awake()
    {
        Enemy.onDamageTaken += EnemyTookDamageCallback;
    }

    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyTookDamageCallback;
        
    }

    private void EnemyTookDamageCallback(int damage, Vector2 enemyPos, bool isCriticalHit)
    {
        if (health >= maxHealth)
            return;

        float lifeStealValue = damage * lifeSteal ;
        Debug.Log("lifeSteal value : " + lifeStealValue);

        float healthToAdd = Math.Min(lifeStealValue,maxHealth - health);
        Debug.Log("health To Add: " + healthToAdd);

        health += healthToAdd;
        UpdateUI();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health < maxHealth)
            RecoverHealth();
    }

    private void RecoverHealth()
    {
        healthRecoveryTimer += Time.deltaTime;

        if (healthRecoveryTimer >= healthRecoveryDuration)
        {
            healthRecoveryTimer = 0;
            float healthToAdd = MathF.Min(.1f, maxHealth - health);
            health += healthToAdd;
            Debug.Log("health :" + health);

            UpdateUI() ;   
        }
    }

    public void TakeDamage(int damage)
    {
        if (ShouldDodge())
        {
            onAttackDodged?.Invoke(transform.position);
            return;
        }

        float realDamage = damage * Mathf.Clamp(1 - (armor / 1000), 0, 10000);
        realDamage = Mathf.Min(realDamage, health);
        health -= realDamage;

        Debug.Log("Real Damage :" + realDamage);

        UpdateUI();


        if (health <= 0)
            PassAway();
       
    }

    private bool ShouldDodge()
    {
        return Random.Range(0f, 100f) < dodge;
    }

    private void UpdateUI()
    {
        float healthBar = (float)health / maxHealth;
        healthSlider.value = healthBar;
        healthText.text = (int)health + " / " + maxHealth;


    }
    private void PassAway()
    {
        GameManager.instance.SetGameState(GameState.GAMEOVER);
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {

        float addedHealth = playerStatsManager.GetStatValue(Stat.MaxHealth);
        maxHealth = baseMaxHealth + (int)addedHealth;
        maxHealth = Mathf.Max(maxHealth, 1);

        health = maxHealth;
        UpdateUI();

        armor = playerStatsManager.GetStatValue(Stat.Armor);
        lifeSteal = playerStatsManager.GetStatValue(Stat.LifeSteal)/100;
        dodge = playerStatsManager.GetStatValue(Stat.Dodge);


        healthRecoverySpeed = MathF.Max(.0001f, playerStatsManager.GetStatValue(Stat.HealthRecoverySpeed));
        healthRecoveryDuration = 1f / healthRecoverySpeed;
    }
}
