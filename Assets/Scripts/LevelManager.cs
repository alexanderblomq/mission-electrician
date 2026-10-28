using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public GameObject wholeScene;
    private List<Checkpoint> checkpoints = new List<Checkpoint>();

    private bool[] levelUnlockedStatus = new bool[7];

    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;

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

    public void LevelWasStarted()
    {
        levelUnlockedStatus[getLevelID()] = true;
    }

    public void RegisterCheckpoint(Checkpoint checkpoint)
    {
        checkpoints.Add(checkpoint);
    }

    public bool isLevelWon()
    {
        foreach(Checkpoint checkpoint in checkpoints)
        {
            if(checkpoint.RequiredForCompletion && !checkpoint.CheckPowered())
            {
                return false;
            }
        }
        return true;
    }

    public void LevelLost()
    {
        GetComponent<AudioSource>().clip = loseSound;
        GetComponent<AudioSource>().volume = 0.5f;
        GetComponent<AudioSource>().Play();
        checkpoints = new List<Checkpoint>();
        StartCoroutine((WipeToNextScene.WipeToScene(SceneManager.GetActiveScene().buildIndex, 1, Camera.main.GetComponentInChildren<CircleController>())));
    }

    public bool CheckLevelUnlocked(int levelID)
    {
        if (levelUnlockedStatus[levelID - 1]) return true;
        return false;
    }

    private int getLevelID() { return SceneManager.GetActiveScene().buildIndex - 2; }

    public void LevelWon()
    {
        if(getLevelID() >= 0)
        {
            levelUnlockedStatus[getLevelID()] = true;
        }
        GetComponent<AudioSource>().clip = winSound;
        GetComponent<AudioSource>().Play();
        checkpoints = new List<Checkpoint>();
        CinemachineCamera cam = Camera.main.GetComponent<CinemachineCamera>();
        CinemachineThirdPersonFollow follow = cam.GetComponent<CinemachineThirdPersonFollow>();
        cam.Follow = wholeScene.transform;
        DOTween.To(() => follow.ShoulderOffset.y, x => follow.ShoulderOffset.y = x, follow.ShoulderOffset.y+10, 5f);


        StartCoroutine((WipeToNextScene.WipeToScene(SceneManager.GetActiveScene().buildIndex + 1, 
            3f, Camera.main.GetComponentInChildren<CircleController>())));
    }

    public void GameWon()
    {
        if (getLevelID() >= 0)
        {
            levelUnlockedStatus[getLevelID()] = true;
        }

        CinemachineCamera cam = Camera.main.GetComponent<CinemachineCamera>();
        CinemachineThirdPersonFollow follow = cam.GetComponent<CinemachineThirdPersonFollow>();
        cam.Follow = wholeScene.transform;

        StartCoroutine((WipeToNextScene.WipeToScene(0,
            3f, Camera.main.GetComponentInChildren<CircleController>())));
    }
}
