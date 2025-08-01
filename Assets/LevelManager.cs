using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private List<Checkpoint> checkpoints = new List<Checkpoint>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
    }

    public void RegisterCheckpoint(Checkpoint checkpoint)
    {
        checkpoints.Add(checkpoint);
    }

    public void CheckWin()
    {
        foreach(Checkpoint checkpoint in checkpoints)
        {
            if(!checkpoint.CheckPowered())
            {
                return;
            }
        }
        LevelWon();
    }

    private void LevelWon()
    {
        Debug.Log("The level has been won!");
    }
}
