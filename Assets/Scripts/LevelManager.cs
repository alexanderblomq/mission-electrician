using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public GameObject wholeScene;
    private System.Collections.Generic.List<Checkpoint> checkpoints = new System.Collections.Generic.List<Checkpoint>();

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

    public void LevelLost()
    {
        Debug.Log("This level has been lost");
        StartCoroutine((WipeToNextScene.WipeToScene(SceneManager.GetActiveScene().buildIndex, 1, Camera.main.GetComponentInChildren<CircleController>())));
    }

    public void LevelWon()
    {
        Debug.Log("The level has been won!");
        Camera.main.GetComponent<CinemachineCamera>().Follow = wholeScene.transform;
        StartCoroutine((WipeToNextScene.WipeToScene(SceneManager.GetActiveScene().buildIndex + 1, 
            3f, Camera.main.GetComponentInChildren<CircleController>())));
    }
}
