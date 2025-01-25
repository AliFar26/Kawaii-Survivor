using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour,IPlayerStatsDependency
{


    [Header("Element")]
    [SerializeField] private Transform playerStatContainerParent;

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        int index = 0;

        foreach (Stat stat in Enum.GetValues(typeof(Stat)))
        {
            StatContainer statContianer = playerStatContainerParent.GetChild(index).GetComponent<StatContainer>();
            statContianer.gameObject.SetActive(true);

            Sprite statIcon = ResourcesManager.GetStatIcon(stat);

            float statValue = playerStatsManager.GetStatValue(stat);
            statContianer.Configure(statIcon,Enums.FormatStatName(stat), statValue,true);


            index++;
        }


        
        for (int i = index; i < playerStatContainerParent.childCount; i++)
        {
            playerStatContainerParent.GetChild(i).gameObject.SetActive(false);
            
        }

    }

}
