using UnityEngine;
using RageRunGames.EasyFlyingSystem;
using System.Collections.Generic;
using System;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    [SerializeField]
    public Dictionary<int,bool>unlockedlevels = new Dictionary<int,bool>();
    
    int i = 0;
    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    //Bu update sil
    private void Update()
    {
        //Debuging
        if (Input.GetKeyDown(KeyCode.H))
        {
            i++;
            AddStage(i, true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            i++;
            AddStage(i, false);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {

           CheckStage(1);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {

            StageUpdate(2, false);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {

            StageUpdate(2, true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            DiscardStage(3);
        }

    }
    public void AddStage(int stage,bool isLevelFinished)
    {
        try
        {
            unlockedlevels.Add(stage, isLevelFinished);
        }
        catch
        {
            unlockedlevels[stage]=isLevelFinished;
        }

        DebugUnlockedLevels();
    }
    public void DiscardStage(int stage)
    {
        if (unlockedlevels.ContainsKey(stage))
        {

            unlockedlevels.Remove(stage);
            
        }

        DebugUnlockedLevels();
    }

    void CheckStage(int stage)
    {
        if (unlockedlevels.ContainsKey(stage))
        {
            Debug.Log($"Bu bölum Key:{stage},value: {unlockedlevels[stage]}");

        }

    }

    void DebugUnlockedLevels()
    {
        foreach (var key in unlockedlevels.Keys)
        {
            Debug.Log($"Key: {key}, Value: {unlockedlevels[key]}");
        }
    }


    public void StageFortfeight()
    {
      
        

    }

    public void StageUpdate(int stage,bool isComplated)
    {    
        if (unlockedlevels.ContainsKey(stage))
        {

            unlockedlevels[stage] = isComplated;
            Debug.Log($"Bu bölum Key:{stage},value: {unlockedlevels[stage]}");
        }
        else
        {
            Debug.Log($"Stage {stage} bitmedi ({unlockedlevels[stage]})");
        }
    }

    
}
