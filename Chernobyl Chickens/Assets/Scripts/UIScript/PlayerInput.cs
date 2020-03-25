using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public GameObject GameManager;
    public Character SelectedCharacter;
    public GameObject[] players;
    public int PlayerCount;
    public bool p_joined;
    public string CurrentScene;
    PersistentGameManagerScript GameManagerScript;

    public GameObject CharacterSelector;


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
        if (CurrentScene == "CharacterSelect")
        {
            //Debug.Log(CharacterSelector);
            CharacterSelector.SendMessage("SelectChar");
        }
    }

    //MISC FUNCTIONS -----------------------------------------------------------------------------------------------------------------------
    void SetCharacter(Character character)
    {
        Debug.Log(character);
        SelectedCharacter = character;
    }
}
