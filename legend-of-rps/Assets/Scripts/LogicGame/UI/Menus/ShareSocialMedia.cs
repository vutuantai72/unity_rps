using System.Collections;
using UnityEngine;
#if UNITY_ANDROID || UNITY_IPHONE
using NativeShareNamespace;
#endif

public class ShareSocialMedia : MonoBehaviour
{
#if UNITY_ANDROID || UNITY_IPHONE
    public void ShareSocial()
    {
		StartCoroutine(ShareLinkGame());
	}
	IEnumerator ShareLinkGame()
	{
		yield return new WaitForEndOfFrame();

		Texture2D tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		tx.Apply();

		Destroy(tx); //to avoid memory leaks

		new NativeShare()
			.SetUrl("https://rpsgame.world/")
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
	}
#endif
}
