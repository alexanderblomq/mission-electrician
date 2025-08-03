using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<GameObject> carrotSegments = new List<GameObject>();
    [SerializeField] GameObject spriteTopOfMusic;
    private AudioSource mainMusic;

    private void Start()
    {
        mainMusic = GetComponent<AudioSource>(); 
        if(LevelManager.Instance != null)
        {
            LevelManager.Instance.LevelWasStarted();
            for (int i = 0; i < carrotSegments.Count; i++)
            {
                if (LevelManager.Instance.CheckLevelUnlocked(i + 1))
                {
                    carrotSegments[i].GetComponent<Button>().interactable = true;
                }
            }
        }
        
    }

    public void ToggleMusic()
    {
        spriteTopOfMusic.SetActive(!MusicManager.Instance.ToggleMusic());
    }
    

    public void LoadLevel(int level)
    {
        if (LevelManager.Instance.CheckLevelUnlocked(level))
        {
            StartCoroutine(WipeToNextScene.WipeToScene(level+1, 3, Camera.main.GetComponentInChildren<CircleController>()));
        }
    }
}
