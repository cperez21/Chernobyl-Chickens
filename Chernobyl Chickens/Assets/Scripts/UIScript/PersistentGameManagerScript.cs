using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PersistentGameManagerScript : MonoBehaviour
{
    public static PersistentGameManagerScript instance;
    public string LoadedScene;
    public string SelectedMap;

    private void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync((int)SceneIndexes.MenuScene, LoadSceneMode.Additive);
    }

    public void GoToCharSelect()
    {
        Debug.Log("Gone");
        SceneManager.UnloadSceneAsync((int)SceneIndexes.MenuScene);
        SceneManager.LoadSceneAsync((int)SceneIndexes.CharSelect, LoadSceneMode.Additive);
        LoadedScene = "CharacterSelect";
    }

    public void GoToMapSelect()
    {
        SceneManager.UnloadSceneAsync(LoadedScene);
        SceneManager.LoadSceneAsync((int)SceneIndexes.MapSelect, LoadSceneMode.Additive);
        LoadedScene = "MapSelect";
    }
    //ONLY REFERENCE WHEN GOING TO MAPS. will spawn characters
    public void ChangeScene(string sceneName)
    {

        SceneManager.UnloadSceneAsync(LoadedScene);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        LoadedScene = sceneName;



    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

