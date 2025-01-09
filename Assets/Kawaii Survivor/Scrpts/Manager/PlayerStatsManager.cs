using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{

    private Dictionary<Stat,float> addends = new Dictionary<Stat,float>();

    // Start is called before the first frame update
    void Start()
    {
        
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

    private void UpdatePlayerStats()
    {

        IEnumerable<IPlayerStatsDependency> playerStatsDependencies =
           FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
           .OfType<IPlayerStatsDependency>();

        foreach (IPlayerStatsDependency dependency in playerStatsDependencies)
            dependency.UpdateStats(this);
    }
}
