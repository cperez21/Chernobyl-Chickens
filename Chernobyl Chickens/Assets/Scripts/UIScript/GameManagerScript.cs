//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using UnityEngine.InputSystem;

//public class PersistentGameManagerScript : MonoBehaviour
//{
//    public static PersistentGameManagerScript instance;
//    public string LoadedScene;
//    private void Awake()
//    {
//        instance = this;
//        SceneManager.LoadSceneAsync((int)SceneIndexes.MenuScene, LoadSceneMode.Additive);
//    }

//    public void GoToCharSelect()
//    {
//        Debug.Log("Gone");
//        SceneManager.UnloadSceneAsync((int)SceneIndexes.MenuScene);
//        SceneManager.LoadSceneAsync((int)SceneIndexes.CharSelect, LoadSceneMode.Additive);
//        LoadedScene = "CharacterSelect";
//    }

//    public void ChangeScene(string sceneName, string currentScene)
//    {

//        SceneManager.UnloadSceneAsync(currentScene);
//        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
//        LoadedScene = sceneName;
        
//    }

//    public void QuitGame()
//    {
//        Application.Quit();
//    }

//}


