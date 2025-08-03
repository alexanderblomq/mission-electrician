using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WipeToNextScene : MonoBehaviour
{
    [SerializeField] CircleController circleController;
    [SerializeField] float delay = 0f;
    private Coroutine curr;
    public void Wipe()
    {
        if(SceneManager.loadedSceneCount <= 1)
        {
            StartCoroutine(WipeToScene(SceneManager.GetActiveScene().buildIndex + 1, delay, circleController));
        }
        
    }
    public static IEnumerator WipeToScene(int nextSceneIndex, float delay, CircleController circle)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(nextSceneIndex);
        loadScene.allowSceneActivation = false;
        yield return new WaitForSeconds(delay);
        circle.Close();
        yield return new WaitForSeconds(circle.closeTime * 2);
        loadScene.allowSceneActivation = true;
        yield return new WaitUntil(() => loadScene.isDone);
    }
}
