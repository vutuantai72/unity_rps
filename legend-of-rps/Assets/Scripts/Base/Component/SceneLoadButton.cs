using UnityEngine.SceneManagement;
public class SceneLoadButton : RxButton
{
    public string sceneName;
    public override void OnClickAsync()
    {
        SceneManager.LoadScene(sceneName);
    }
}
