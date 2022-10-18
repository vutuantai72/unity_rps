using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class JsonData
{
    public string path;
    public string imagePath;

    public OriginCoords originCoords;
    public float top;
    public float left;
    public float width;
    public float height;
}

[Serializable]
public class OriginCoords
{

    public float top;
    public float left;
    public float width;
    public float height;
}

[Serializable]
class PsdTree
{
    public string type;
    public bool visible;
    public float opacity;

    public string name;
    public int left;
    public int right;
    public int top;
    public int bottom;
    public int height;
    public int width;

    public List<PsdTree> children;
}


public class FileLoadManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject prefabForCreation;
    List<TextAsset> jsonFiles;
    List<JsonData> dataToBeInterpreted = new List<JsonData>();

    List<string> imagePathNames = new List<string>();

    string folderNameToCreate = "ingame_size_1238x2198";

    void Start()
    {

        jsonFiles = Resources.LoadAll<TextAsset>(folderNameToCreate).ToList();

        CreateGroup();
    }

    void CreateGroup()
    {
        TextAsset rescue = jsonFiles.Find(asset => asset.name == folderNameToCreate);

        PsdTree tree = JsonUtility.FromJson<PsdTree>(rescue.text);


        Work(tree, canvas.transform, folderNameToCreate);
    }

    void Work(PsdTree tree, Transform transform, string route)
    {

        if (tree.name != null && tree.name.Contains("backup"))
            return;

        GameObject createdObject = Instantiate(prefabForCreation, transform);
        createdObject.name = tree.name;
        string imagePath = route;
        if (createdObject.name != null && createdObject.name != "")
        {
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            transform.GetComponent<Image>().enabled = false;
            imagePath = route + "/" + createdObject.name;
        }
        else
        {
            createdObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            createdObject.GetComponent<Image>().enabled = false;
        }

        Sprite sprite = Resources.Load<Sprite>(imagePath);
        Image ImageOfCreatedObject = createdObject.GetComponent<Image>();
        ImageOfCreatedObject.sprite = sprite;


        ImageOfCreatedObject.rectTransform.anchoredPosition = new Vector2(tree.left * (1280 / (float)1920), -tree.top * (720 / (float)1080));
        ImageOfCreatedObject.rectTransform.sizeDelta = new Vector2(tree.width * (1280 / (float)1920), tree.height * (720 / (float)1080));


        for (int index = tree.children.Count - 1; index >= 0; index--)
        {
            Work(tree.children[index], createdObject.transform, imagePath);
        }
    }

}