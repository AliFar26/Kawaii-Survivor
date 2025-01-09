using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour,IPlayerStatsDependency
{
    [Header("Setting")]
    [SerializeField] private int baseMaxHealth;
     private int maxHealth;
    private int health;

    [Header("Element")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        UpdateUI();


        if (health <= 0)
            PassAway();
       
    }

    private void UpdateUI()
    {
        float healthBar = (float)health / maxHealth;
        healthSlider.value = healthBar;
        healthText.text = health + " / " + maxHealth;
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
    }
}
