using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerStatsManager))]
public class PlayerObjects : MonoBehaviour
{
    [field: SerializeField] public List<ObjectDataSO> Objects { get; private set; }
    private PlayerStatsManager playerStatsManager;

    private void Awake()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    private void Start()
    {
        foreach (ObjectDataSO objectData in Objects)
            playerStatsManager.AddObject(objectData.BaseStats);
    }















    //private void Start()
    //{
    //    foreach (ObjectDataSO obj in Objects)
    //        foreach (StatData psd in obj.Stats)
    //            GetComponent<PlayerStatsManager>().AddObjectStat(psd);
    //}

    //public void RecycleObject(ObjectDataSO objectData)
    //{
    //    foreach (StatData psd in objectData.Stats)
    //        GetComponent<PlayerStatsManager>().RemoveObjectStat(psd);

    //    CurrencyManager.instance.AddCurrency(objectData.RecyclePrice);
    //    Objects.Remove(objectData);
    //}

    public void AddObject(ObjectDataSO objectData)
    {
        Objects.Add(objectData);

            playerStatsManager.AddObject(objectData.BaseStats);
    }

}
