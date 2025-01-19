using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(WaveManagerUI))]
public class WaveManager : MonoBehaviour , IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Player player;
    private WaveManagerUI ui;
    

    [Header("Setting")]
    [SerializeField] private float waveDuration;
     private float timer ;
    private bool isTimerOn;
    private int currentWaveIndex;



    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    private List<float> localCounters = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        ui = GetComponent<WaveManagerUI>();
        //StartWave(currentWaveIndex);
    }

   

    // Update is called once per frame
    void Update()
    {
        if (!isTimerOn)
            return;


        if (timer < waveDuration)
        {

            ManageCurrentWave();
            string timerString = ((int)(waveDuration - timer)).ToString();
            ui.UpdateTimerText(timerString);
        }
        else
            StartWaveTransition();
    }

    private void StartWaveTransition()
    {
        isTimerOn = false;

        DefeatAllEnemies();

        currentWaveIndex++;

        if (currentWaveIndex >= waves.Length)
        {
            //Debug.Log("WAVE COMPLETED");
            ui.UpdateTimerText(" ");

            ui.UpdateWaveText("Stage Completed");

            GameManager.instance.SetGameState(GameState.STAGECOMPLETE);

        }
        else
            GameManager.instance.WaveCompletedCallback();
    }

    private void StartWave(int waveIndex)
    {
        ui.UpdateWaveText("Wave " + (waveIndex + 1) + "/" + waves.Length);
        localCounters.Clear();
        foreach (WaveSegment segment in waves[waveIndex].segments)
            localCounters.Add(1);

        timer = 0;
        isTimerOn = true;
    }

    private void DefeatAllEnemies()
    {
        transform.Clear();
    }







    private void ManageCurrentWave()
    {
        Wave currentWave = waves[currentWaveIndex];

        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment segment = currentWave.segments[i];

            float tStart = segment.tStartEnd.x / 100 * waveDuration;
            float tEnd = segment.tStartEnd.y / 100 * waveDuration;

            if (timer < tStart || timer > tEnd)
                continue;

            float timeSinceSegmentStart = timer - tStart;

            float spawnDelay = 1f / segment.spawnFrequency;

            if (timeSinceSegmentStart / spawnDelay > localCounters[i])
            {
                Instantiate(segment.prefab, GetSpawnPosition(), Quaternion.identity, transform);
                localCounters[i]++;
            }

        }

        timer += Time.deltaTime;
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 direction = Random.onUnitSphere;
        Vector2 offset = direction.normalized * Random.Range(6,10);
        Vector2 targetPosition = (Vector2)player.transform.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -18 ,18);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -8 ,8);


        return targetPosition;

    }

    

    public void OnGameStateChangedCallback(GameState gameState)
    {
        //Debug.Log($"Wave Manager Knows that the new gameState is {gameState}");

        switch (gameState)
        {
            //case GameState.MENU:
            //    break;
            case GameState.GAME:
                StartNextWave();
                break;
            //case GameState.WAVETRANSITION:
            //    break;
            //case GameState.SHOP:
            //    break;
            //default:
            //    break;

            case GameState.GAMEOVER:
                isTimerOn = false;
                DefeatAllEnemies();
                break;
        }
    }

    private void StartNextWave()
    {
        StartWave(currentWaveIndex);
    }
}

[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;
}


[System.Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0, 100)] public Vector2 tStartEnd;
    public float spawnFrequency;
    public GameObject prefab;
}