using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using System.Resources;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    public static WaveTransitionManager instance;

    [Header(" Player Related ")]
    //[SerializeField] private PlayerObjects playerObjects;

    [Header(" Elements ")]
    //[SerializeField] private PlayerStatsManager playerStatsManager;
    [SerializeField] private UpgradeContainer[] upgradeContainers;
    //[SerializeField] private Transform playerStatsContainersParent;
    //[SerializeField] private Transform upgradeContainersParent;

    //[Header(" Chest Management ")]
    //[SerializeField] private ChestObjectContainer chestObjectContainer;
    //[SerializeField] private Transform chestContainerParent;
    //private ChestObjectContainer currentChestContainer;
    //private int chestsCollected;

    //private void Awake()
    //{
    //    if (instance == null)
    //        instance = this;
    //    else
    //        Destroy(gameObject);

    //    Chest.onCollected += ChestCollectedCallback;
    //}

    //private void OnDestroy()
    //{
    //    Chest.onCollected -= ChestCollectedCallback;
    //}

    //public void GameStateChangedCallback(GameState gameState)
    //{
    //    //if (gameState == GameState.WAVETRANSITION)
    //    //    TryOpenChest();
    //}

    public void OnGameStateChangedCallback(GameState gameState)
    {

        switch (gameState)
        {
            case GameState.WAVETRANSITION:
                ConfigureBonuses();
                break;
        }
    }

    //private void ChestCollectedCallback()
    //{
    //    chestsCollected++;
    //}

    //private void TryOpenChest()
    //{
    //    chestContainerParent.Clear();

    //    if (chestsCollected <= 0)
    //        ConfigureBonuses();
    //    else
    //    {
    //        upgradeContainersParent.gameObject.SetActive(false);
    //        ShowObject();
    //    }
    //}

    //private void ShowObject()
    //{
    //    chestsCollected--;

    //    ObjectDataSO[] objectDatas = ResourceManager.Objects;
    //    ObjectDataSO objectData = objectDatas[Random.Range(0, objectDatas.Length)];

    //    currentChestContainer = Instantiate(chestObjectContainer, chestContainerParent);
    //    currentChestContainer.Configure(objectData, this);
    //}

    //public void TakeObject()
    //{
    //    playerObjects.AddObject(currentChestContainer.ObjectData);
    //    TryOpenChest();
    //}

    //public void RecycleObject()
    //{
    //    CurrencyManager.instance.AddCurrency(currentChestContainer.ObjectData.RecyclePrice);
    //    TryOpenChest();
    //}


    [NaughtyAttributes.Button]
    private void ConfigureBonuses()
    {
        //upgradeContainersParent.gameObject.SetActive(true);

        for (int i = 0; i < upgradeContainers.Length; i++)
        {
            upgradeContainers[i].Button.onClick.RemoveAllListeners();

            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stat)).Length);
            Stat stat = (Stat)(Enum.GetValues(typeof(Stat)).GetValue(randomIndex));

            string buttonString;
            Action action = GetActionToPerform(stat, out buttonString);

            //upgradeContainers[i].Configure(ResourceManager.GetStatIcon(stat), Enums.FormatStatName(stat), buttonString);
            upgradeContainers[i].Configure(null, Enums.FormatStatName(stat), buttonString);

            upgradeContainers[i].Button.onClick.AddListener(() => action?.Invoke());
            upgradeContainers[i].Button.onClick.AddListener(() => ConfigureBonuses());
            upgradeContainers[i].Button.onClick.AddListener(() => BonusSelectedCallback());
        }
    }

    private void BonusSelectedCallback()
    {
        GameManager.instance.WaveCompletedCallback();
    }

    private Action GetActionToPerform(Stat stat, out string buttonText)
    {
        buttonText = "";
        float value;

        switch (stat)
        {
            case Stat.Attack:

                value = Random.Range(1, 10);
                buttonText = "+" + value.ToString() + "%";
                break;

            case Stat.AttackSpeed:

                value = Random.Range(1, 10);
                buttonText = "+" + value.ToString() + "%";
                break;

            case Stat.Range:

                value = Random.Range(1f, 5f);
                buttonText = "+" + value.ToString("F2");
                break;

            case Stat.CriticalChance:

                value = Random.Range(1, 10);
                buttonText = "+" + value.ToString() + "%";
                break;

            case Stat.CriticalPercent:

                value = Random.Range(1f, 2f);
                buttonText = "+" + value.ToString("F2") + "x";
                break;

            case Stat.MaxHealth:

                value = Random.Range(1, 5);
                buttonText = "+" + value;
                break;

            case Stat.MoveSpeed:

                value = Random.Range(1, 10);
                buttonText = "+" + value.ToString() + "%";
                break;

            case Stat.Armor:
                value = Random.Range(1, 10);
                buttonText = "+" + value.ToString() + "%";
                break;

            case Stat.Dodge:
                value = Random.Range(1, 10);
                buttonText = "+" + value.ToString() + "%";
                break;

            case Stat.LifeSteal:
                value = Random.Range(1, 10);
                buttonText = "+" + value.ToString() + "%";
                break;

            case Stat.Luck:
                value = Random.Range(1, 10);
                buttonText = "+" + value.ToString() + "%";
                break;

            case Stat.HealthRecoverySpeed:
                value = Random.Range(1, 10);
                buttonText = "+" + value.ToString() + "%";
                break;

            default:
                value = 0;
                break;
        }

        //    return () => playerStatsManager.AddPlayerStat(stat, value);
        return () => Debug.Log(("IT WORKED"));
    }

    //public static bool HasCollectedChest()
    //{
    //    return instance.chestsCollected > 0;
    //}
}