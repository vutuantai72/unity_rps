using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class MetadreamMenu : MonoBehaviour
{
    [MenuItem("GameObject/1. Metadream/Add browser button", false)]
    static void BrowserButtonAdd(MenuCommand menuCommand)
    {

        GameObject gameObject = buttonAdd("NewOpenURLButton", menuCommand);
        gameObject.AddComponent<OpenURLButton>();
    }

    [MenuItem("GameObject/1. Metadream/Added scene move button", false)]
    static void SceneMoveButtonAdd(MenuCommand menuCommand)
    {

        GameObject gameObject = buttonAdd("NewLoadSceneButton", menuCommand);
        gameObject.AddComponent<SceneLoadButton>();
    }

    static GameObject buttonAdd(string name, MenuCommand menuCommand)
    {
        GameObject gameObject = gameBbjectAdd(name, menuCommand);
        gameObject.AddComponent<Image>();
        gameObject.AddComponent<Button>();
        return gameObject;
    }

    static GameObject gameBbjectAdd(string name, MenuCommand menuCommand)
    {
        GameObject gameObject = new GameObject(name);


        GameObjectUtility.SetParentAndAlign(gameObject, menuCommand.context as GameObject);

        Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);

        Selection.activeObject = gameObject;

        return gameObject;
    }
}