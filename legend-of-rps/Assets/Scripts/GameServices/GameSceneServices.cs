
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Script.Utils;
public class GameSceneServices : UnityActiveSingleton<GameSceneServices>
{
    private string _cachePreviousSceneName = "";

    public void Start()
    {
        this.gameObject.AddComponent<GlobalData>();
    }

    /// <summary>
    /// GetPreviousSceneName Get cache previous scene name
    /// </summary>
    /// <returns></returns>
    public string GetPreviousSceneName()
    {
        return _cachePreviousSceneName;
    }

    /// <summary>
    /// CacheSceneName : set cache previous scene name before load new scene
    /// </summary>
    private void CacheSceneName()
    {
        _cachePreviousSceneName = SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// LoadScene: Load to new scene
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        CacheSceneName();
        SceneManager.LoadSceneAsync(sceneName);
    }

    /// <summary>
    /// LoadSceneWithDelay : load sync with delay for show some loading effect
    /// </summary>
    /// <param name="sceneName"> the target scene name</param>
    /// <param name="timeDelay"> the time to delay for effect</param>
    public void LoadSceneWithDelay(string sceneName, float timeDelay = 1)
    {
        CacheSceneName();
        StartCoroutine(OnLoadAsyncSceneRoutine(sceneName, timeDelay));
    }

    /// <summary>
    /// OnLoadAsyncSceneRoutine: routine to load new scene after the time delay
    /// </summary>
    /// <param name="screenName">the target scene to load</param>
    /// <param name="timeDelay">the time to yield for delay</param>
    private IEnumerator OnLoadAsyncSceneRoutine(string screenName, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(screenName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}