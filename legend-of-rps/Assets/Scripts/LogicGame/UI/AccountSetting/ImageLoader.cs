using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

//For firebase storage
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class ImageLoader : MonoBehaviour
{
    [SerializeField] private Image avatarUser;
    public Image avatar;

    private GameService gameService = GameService.@object;
    public static ImageLoader Instance { get; private set; }

    [System.Obsolete]
    public IEnumerator LoadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url); //Get url when login

        // gameService.isLoading.Value = true;
        yield return request.SendWebRequest(); //wait for request to complete

        if (request.isNetworkError || request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // gameService.isLoading.Value = false;
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2), 720f);

        }
    }

    public void OnShowAvatar()
    {
//#if UNITY_EDITOR
//        string maleAvaPath = $"{Application.dataPath}/Resources/Avatar/Male";
//        string femaleAvaPath = $"{Application.dataPath}/Resources/Avatar/Female";
//#else
//        string maleAvaPath = $"{Application.persistentDataPath}/Resources/Avatar/Male";
//        string femaleAvaPath = $"{Application.persistentDataPath}/Resources/Avatar/Female";  
//#endif

        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            if (Resources.Load<Texture2D>($"Avatar/Male/{gameService.userAvatar.Value}") != null)
            {
                var texture = Resources.Load<Texture2D>($"Avatar/Male/{gameService.userAvatar.Value}");

                if (texture != null)
                {
                    avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                }
            }
            else
            {
                if (Resources.Load<Texture2D>($"Avatar/Female/{gameService.userAvatar.Value}") != null)
                {
                    var texture = Resources.Load<Texture2D>($"Avatar/Female/{gameService.userAvatar.Value}");

                    if (texture != null)
                    {
                        avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                    }
                }
                else
                {
                    if (Resources.Load<Texture2D>($"Avatar/Default") != null)
                    {
                        var texture = Resources.Load<Texture2D>($"Avatar/Default");
                        if (texture != null)
                        {
                            avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                        }
                    }
                }
            }            
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log($"Avatar User ===== {gameService.userAvatar.Value}");
            if (Resources.Load<Texture2D>($"Avatar/MaleMenu/{gameService.userAvatar.Value}") != null)
            {
                var texture = Resources.Load<Texture2D>($"Avatar/MaleMenu/{gameService.userAvatar.Value}");

                if (texture != null)
                {
                    avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                }
            }
            else
            {
                if (Resources.Load<Texture2D>($"Avatar/FemaleMenu/{gameService.userAvatar.Value}") != null)
                {
                    var texture = Resources.Load<Texture2D>($"Avatar/FemaleMenu/{gameService.userAvatar.Value}");

                    if (texture != null)
                    {
                        avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                    }
                }
                else
                {
                    if (Resources.Load<Texture2D>($"Avatar/Default") != null)
                    {
                        var texture = Resources.Load<Texture2D>($"Avatar/Default");
                        if (texture != null)
                        {
                            avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                        }
                    }
                }
            }
            avatarUser.sprite = avatar.sprite;
        }      
    }

    public void OnShowImage(string IDImage)
    {

        if (Resources.Load<Texture2D>($"Avatar/Male/{IDImage}") != null)
        {
            var texture = Resources.Load<Texture2D>($"Avatar/Male/{IDImage}");

            if (texture != null)
            {
                avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
            }
        }
        else
        {
            if (Resources.Load<Texture2D>($"Avatar/Female/{IDImage}") != null)
            {
                var texture = Resources.Load<Texture2D>($"Avatar/Female/{IDImage}");

                if (texture != null)
                {
                    avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                }
            }
            else
            {
                if(Resources.Load<Texture2D>($"Avatar/Default") != null)
                {
                    var texture = Resources.Load<Texture2D>($"Avatar/Default");
                    if (texture != null)
                    {
                        avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                    }
                }
            }
        }
    }
}
