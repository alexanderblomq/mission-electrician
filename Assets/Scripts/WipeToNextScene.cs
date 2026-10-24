using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WipeToNextScene : MonoBehaviour
{
    [SerializeField] CircleController circleController;
    [SerializeField] float delay = 0f;

    private bool _isRunning = false;

    private Coroutine curr;
    public void Wipe()
    {
        if(!_isRunning && SceneManager.sceneCount <= 1)
        {
            _isRunning = true;          

            StartCoroutine(WipeToScene(SceneManager.GetActiveScene().buildIndex + 1, delay, circleController));

            _isRunning = false;
        }
        
    }
    public static IEnumerator WipeToScene(int nextSceneIndex, float delay, CircleController circle)
    {
        // preloads next scene in the background
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(nextSceneIndex);
        loadScene.allowSceneActivation = false;
        yield return new WaitForSeconds(delay);
        circle.Close();
        yield return new WaitForSeconds(circle.closeTime * 2);
        loadScene.allowSceneActivation = true;
        yield return new WaitUntil(() => loadScene.isDone);
    }
}
