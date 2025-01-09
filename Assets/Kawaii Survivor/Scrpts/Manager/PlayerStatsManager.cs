using System.Collections;
using System.Collections.Generic;
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
    }
}
