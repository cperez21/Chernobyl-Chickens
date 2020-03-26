using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //sets gamemanager and script
    public GameObject GameManager;
    PersistentGameManagerScript GameManagerScript;
    //Grabs current scene
    public string CurrentScene;
    //setsplayername
    public GameObject[] players;
    public int PlayerCount;
    public bool p_joined;


    //for character selectorscreen
    public bool ready;
    public GameObject CharacterSelector;
    public Character SelectedCharacter;


    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        GameManagerScript = GameManager.GetComponent<PersistentGameManagerScript>();

        players = GameObject.FindGameObjectsWithTag("PlayerContainer");
        foreach (GameObject PlayerContainer in players)
        {
            PlayerCount += 1;
        }
        this.name = "Player" + PlayerCount;
        p_joined = false;
        ready = false;


        //CSS = GameObject.Find("PlayerCharacterSelect" + PlayerCount)
    }

    // Update is called once per frame
    void Update()
    {
        //If Current Scene isn't actual scene, then sets current scene. 
        if (CurrentScene != GameManagerScript.LoadedScene)
        {
            CurrentScene = GameManagerScript.LoadedScene;
            //Debug.Log(CurrentScene);
        }

        //If in character select, sets character selector to correct panel. 
        if (CurrentScene == "CharacterSelect")
        {
            CharacterSelector = GameObject.Find("PlayerCharacterSelect" + PlayerCount);
            
        }

        //Sets p_Joined to TRUE
        if (p_joined == false && CurrentScene == "CharacterSelect")
        {
            //Debug.Log("WE IN FAM");
            p_joined = true;
        }
    }
    //CONTROLS----------------------------------------------------------------------------------------------------------------------------
    void OnLeft()
    {
        if (CurrentScene == "CharacterSelect")
        {
            //Debug.Log(CharacterSelector);
            CharacterSelector.SendMessage("CharacterToggleLeft");
        }
        
    }

    void OnRight()
    {
        if (CurrentScene == "CharacterSelect")
        {
            //Debug.Log(CharacterSelector);
            CharacterSelector.SendMessage("CharacterToggleRight");
        }
    }
    void OnPlayerSelect()
    {
        Debug.Log("ping");
        if (CurrentScene == "CharacterSelect" && ready == false)
        {
            //Debug.Log(CharacterSelector);
            CharacterSelector.SendMessage("SelectChar");
        }
        else if (CurrentScene == "CharacterSelect" && ready == true)
        {
            GameManagerScript.SendMessage("GoToMapSelect");
        }
    }

    void OnPlayerBack()
    {
        if (CurrentScene == "CharacterSelect" && ready == true)
        {
            ready = false;
        }
    }

    //MISC FUNCTIONS -----------------------------------------------------------------------------------------------------------------------
    void SetCharacter(Character character)
    {
        Debug.Log(character);
        SelectedCharacter = character;
        ready = true;
    }
}
