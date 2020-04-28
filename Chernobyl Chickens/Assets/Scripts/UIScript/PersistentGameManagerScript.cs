using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PersistentGameManagerScript : MonoBehaviour
{
    public static PersistentGameManagerScript instance;
    PersistentGameManagerScript gameManager;

    public MenuManagerScript MenuManager;

    public string LoadedScene;
    public string SelectedMap;

    public bool PlayersJoined;

    private void Awake()
    {
        LoadedScene = "MenuScene";
        instance = this;
        

        Scene scene = SceneManager.GetActiveScene();
        if (!scene.name.Contains("Test")) // I put this in so I can still use my testbed without complications. -Cullen 3/31
        {
            SceneManager.LoadSceneAsync((int)SceneIndexes.MenuScene, LoadSceneMode.Additive);
        }
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<PersistentGameManagerScript>();
        //players = GameObject.FindObjectsOfType<PlayerController>();
        PlayersJoined = false;

    }

    private void Update()
    {
        if(LoadedScene == "MenuScene" && MenuManager == null)
        {
            MenuManager = GameObject.Find("MenuManager").GetComponent<MenuManagerScript>();
        }

        //if (players.Length < MaxPlayers)
        //{
        //    players = GameObject.FindObjectsOfType<PlayerController>();
        //}

        //for (int x = 0; x < players.Length; x++)
        //{
        //    if (players[x].state == PlayerController.PlayerState.DEAD)
        //    {
        //        gameManager.SendMessage("GoToMenuScene");
        //    }
        //}
    }

    //public void GoToCharSelect()
    //{

    //    SceneManager.UnloadSceneAsync((int)SceneIndexes.MenuScene);
    //    SceneManager.LoadSceneAsync((int)SceneIndexes.CharSelect, LoadSceneMode.Additive);
    //    LoadedScene = "CharacterSelect";
    //}

    public void GoToMapSelect()
    {
        MenuManager = null;
        SceneManager.UnloadSceneAsync("MenuScene");
        //SceneManager.UnloadSceneAsync(LoadedScene);
        SceneManager.LoadSceneAsync((int)SceneIndexes.MapSelect, LoadSceneMode.Additive);
        LoadedScene = "MapSelect";
    }

    public void GoToMenuScene()
    {
        SceneManager.UnloadSceneAsync(LoadedScene);
        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        LoadedScene = "MenuScene";
    }
    
    //ONLY REFERENCE WHEN GOING TO MAPS. will spawn characters
    public void ChangeScene(string sceneName)
    {

        SceneManager.UnloadSceneAsync(LoadedScene);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        LoadedScene = sceneName;


    }

    //Used by MenuManager MM switches to Main Menu, CS swithces to Character Select Canvas. Back to Menu Scene is used to return from map select
    public void SwitchMM()
    {
        PlayersJoined = true;
        MenuManager.SendMessage("SwitchToMainMenu");
        LoadedScene = "MenuScene";
    }
    //public void BackToMenuScene()
    //{
    //    SceneManager.UnloadSceneAsync(LoadedScene);
    //    SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
    //    LoadedScene = "MenuScene";
    //    if (LoadedScene == "MenuScene" && MenuManager == null)
    //    {
    //        Debug.Log("FindingMenuManager");
    //        MenuManager = GameObject.Find("MenuManager").GetComponent<MenuManagerScript>();
    //        Debug.Log("FoundMenuManager");
    //        SwitchCS();
    //    }
    //    else
    //    {
    //        SwitchCS();
    //    }
        
    //}
    //public void SwitchCS()
    //{
    //    MenuManager.SendMessage("SwitchToCharSel");
    //    LoadedScene = "CharacterSelect";
    //}







    //USED FOR TESTINGONLY
    public void GoToChernobyl()
    {
        SceneManager.UnloadSceneAsync("MenuScene");
        SceneManager.LoadSceneAsync((int)SceneIndexes.ChernobylWhiteBox, LoadSceneMode.Additive);
        LoadedScene = "ChernobylWhiteBox";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    

}

