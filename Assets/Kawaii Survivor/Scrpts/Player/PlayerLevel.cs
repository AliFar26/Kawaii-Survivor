using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [Header(" Settings ")]
    private int requiredXp;
    private int currentXp;
    private int level;
    private int levelsEarnedThisWave;

    [Header(" Visuals ")]
    [SerializeField] private Slider xpBar;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("DEBUG")]
    [SerializeField] private bool Debug;

    private void Awake()
    {
        // Right now I'm using candy, but I will need to use a base class, maybe abstract for all collectable if I have multiple variants
        Candy.onCollected += CandyCollectedCallback;
    }

    private void OnDestroy()
    {
        Candy.onCollected -= CandyCollectedCallback;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateRequiredXp();
        UpdateVisuals();
    }

    private void CandyCollectedCallback(Candy candy)
    {
        currentXp++;

        if (currentXp >= requiredXp)
            LevelUp();

        UpdateVisuals();
    }

    private void LevelUp()
    {
        level++;
        levelsEarnedThisWave++;
        currentXp = 0;
        UpdateRequiredXp();

        UpdateVisuals();

    }

    private void UpdateVisuals()
    {
        xpBar.value = (float)currentXp / requiredXp;
        levelText.text = "lvl " + (level + 1);
    }

    private void UpdateRequiredXp()
    {
        requiredXp = (level + 1) * 5;
    }

    public bool HasLevelUp()
    {
        if(Debug)
            return true;

        if (levelsEarnedThisWave > 0)
        {
            levelsEarnedThisWave--;
            return true;
        }
        return false;
    }

}
