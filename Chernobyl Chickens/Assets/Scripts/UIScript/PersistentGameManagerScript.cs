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


    public bool AllPlayersReady;
    public bool PlayersJoined;

    public bool AI1, AI2, AI3, AI4;
    public GameObject[] players;

    public GameObject TGroup;

    private void Awake()
    {
        LoadedScene = "MenuScene";
        instance = this;
        

        
        SceneManager.LoadSceneAsync((int)SceneIndexes.MenuScene, LoadSceneMode.Additive);

        //if (!scene.name.Contains("Test")) // I put this in so I can still use my testbed without complications. -Cullen 3/31
        //{
        //    SceneManager.LoadSceneAsync((int)SceneIndexes.MenuScene, LoadSceneMode.Additive);
        //}
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

        //if (LoadedScene == "MenuScene" || LoadedScene == "CharacterSelect")
        //{
            
        //}
        players = GameObject.FindGameObjectsWithTag("PlayerContainer");

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
        if (LoadedScene == "MenuScene" || LoadedScene == "CharacterSelect")
        {
            SceneManager.UnloadSceneAsync("MenuScene");
        }
        else
        {
            SceneManager.UnloadSceneAsync(LoadedScene);
        }
        
        //SceneManager.UnloadSceneAsync(LoadedScene);
        SceneManager.LoadSceneAsync((int)SceneIndexes.MapSelect, LoadSceneMode.Additive);
        LoadedScene = "MapSelect";
    }

    public void GoToMenuScene()
    {
        Debug.Log(LoadedScene);
        SceneManager.UnloadSceneAsync(LoadedScene);
        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        LoadedScene = "MenuScene";
    }
    
    //ONLY REFERENCE WHEN GOING TO MAPS. will spawn characters
    public void ChangeScene(string sceneName)
    {
        //
        //Debug.Log(sceneName);
        SceneManager.UnloadSceneAsync(LoadedScene);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        LoadedScene = sceneName;
        Debug.Log("Changed = " + LoadedScene);


    }

    public void SwitchMM()
    {
        
        if(PlayersJoined == false)
        {
            MenuManager.SendMessage("SwitchToMainMenu");
            LoadedScene = "MenuScene";
            PlayersJoined = true;
        }
        
    }
  

    //USED FOR TESTINGONLY
    public void GoToChernobyl()
    {
        SceneManager.UnloadSceneAsync(LoadedScene);
        SceneManager.LoadSceneAsync((int)SceneIndexes.ChernobylWhiteBox, LoadSceneMode.Additive);
        LoadedScene = "ChernobylWhiteBox";
    }
    public void QuitGame()
    {
        Application.Quit();
    }


    //used to check whether or not players are ready
    public void ReadyUp()
    {
       AllPlayersReady = GameObject.Find("PlayerManager").GetComponent<PlayerManagerScript>().AllReadyUp;

        if (AllPlayersReady == true)
        {
            GoToMapSelect();
        }
    }

    public void toggleAI1()
    {
        AI1 = !AI1;

        //if (AI1 == false)
        //{ 

        //    AI1 = true;

        //}
        //else if (AI1 == true)
        //{

        //    AI1 = false;
        //}
    }
    public void toggleAI2()
    {
        AI2 = !AI2;
    }
    public void toggleAI3()
    {
        AI3 = !AI3;
    }
    public void toggleAI4()
    {
        AI4 = !AI4;
    }

    public void ResetPlayers()
    {
        TGroup = GameObject.Find("TargetGroup1");
        Destroy(TGroup);
        foreach (GameObject player in players)
        {
            
            
            if (player.name == "AIPlayer")
            {
                Destroy(player);
            }
            else
            {
                player.SendMessage("ResetChar");
            }
            
        }


        GameObject[] Feathers;
        Feathers = GameObject.FindGameObjectsWithTag("feathers");
        foreach (GameObject feather in Feathers)
        {
            Destroy(feather);
        }
        if (AI1 == true)
        {
            AI1 = false;
        }
        if (AI2 == true)
        {
            AI2 = false;
        }
        if (AI3 == true)
        {
            AI3 = false;
        }
        if (AI4 == true)
        {
            AI4 = false;
        }

    }
}

