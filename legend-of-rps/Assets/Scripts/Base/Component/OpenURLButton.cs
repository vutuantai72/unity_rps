using UnityEngine;
public class OpenURLButton : RxButton
{
    public string url;
    public override void OnClickAsync()
    {
        Application.OpenURL(url);
    }
}
