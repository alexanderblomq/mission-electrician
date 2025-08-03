using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<GameObject> carrotSegments = new List<GameObject>();
    private void Start()
    {
        LevelManager.Instance.LevelWasStarted();
        for (int i = 0; i < carrotSegments.Count; i++)
        {
            if(!LevelManager.Instance.CheckLevelUnlocked(i+1))
            {
                carrotSegments[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    public void LoadLevel(int level)
    {
        if (LevelManager.Instance.CheckLevelUnlocked(level))
        {
            WipeToNextScene.WipeToScene(level, 3, Camera.main.GetComponentInChildren<CircleController>());
        }
    }
}
