using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private CharacterDataSO playerData;

    private Dictionary<Stat,float> playerStat = new Dictionary<Stat,float>();
    private Dictionary<Stat,float> addends = new Dictionary<Stat,float>();


    private void Awake()
    {
        playerStat = playerData.BaseStats;

        foreach (KeyValuePair<Stat,float> kvp in playerStat)
        {
            addends.Add(kvp.Key, 0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
         

        UpdatePlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayerStat(Stat stat , float value)
    {
        if (addends.ContainsKey(stat))
            addends[stat] += value;
        else
            Debug.Log($"The key {stat} has not been found , this is not normal !!!! Review your code !!!!");

        UpdatePlayerStats();

    }

    public float GetStatValue(Stat stat)
    {
        float value = playerStat[stat] + addends[stat];
        return value;
    }

    private void UpdatePlayerStats()
    {

        IEnumerable<IPlayerStatsDependency> playerStatsDependencies =
           FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
           .OfType<IPlayerStatsDependency>();

        foreach (IPlayerStatsDependency dependency in playerStatsDependencies)
            dependency.UpdateStats(this);
    }
}
