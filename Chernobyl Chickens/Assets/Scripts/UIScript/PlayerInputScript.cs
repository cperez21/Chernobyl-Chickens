﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    //sets gamemanager and script
    public GameObject GameManager;
    PersistentGameManagerScript GameManagerScript;
    //Grabs current scene
    public string CurrentScene;
    //setsplayername
    public GameObject[] players;
    public int PlayerCount;
    
    // may not be needed
    //public bool p_joined;


    //for character selectorscreen
    public bool ready;
    public GameObject CharacterSelector;
    public Character SelectedCharacter;


    //for gameplay
    public bool spawned;
    Vector2 movement;
    public GameObject ThisObject;
    public GameObject PlayerCharacter;
    public GameObject Player;
    PlayerController PlayerScript;
    public GameObject spawnPoint;
    public GameObject BattleUIPlayerCell;

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
        ThisObject = GameObject.Find("Player" + PlayerCount);
        //p_joined = false;
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
        //if (p_joined == false && CurrentScene == "CharacterSelect")
        //{
        //    p_joined = true;
        //}

        //used to spawn player
        if (spawnPoint == null)
        {
            spawnPoint = GameObject.Find("SpawnPoint" + PlayerCount);
        }
        if (spawnPoint != null && spawned == false)
        {
            spawned = true;
            GameObject plyr = Instantiate(PlayerCharacter, spawnPoint.transform.position, Quaternion.identity, ThisObject.transform);
            plyr.name = ("PlayerObject" + PlayerCount);
            Player = plyr.transform.Find("Model").gameObject;
            PlayerScript = Player.GetComponent<PlayerController>();
            //NewPlayer.transform.parent = ThisObject.transform;
            
        }

        //finds playercell in BattleUI
        if (BattleUIPlayerCell == null)
        {
            //Debug.Log("procc");
            BattleUIPlayerCell = GameObject.Find("PlayerCell" + PlayerCount);
            if (BattleUIPlayerCell != null)
            {
                Debug.Log("procc2");
                BattleUIPlayerCell.SendMessage("SetCharacterUI", SelectedCharacter);
            }

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
        
        if (CurrentScene == "CharacterSelect" && ready == false)
        {
            //Debug.Log(CharacterSelector);
            CharacterSelector.SendMessage("SelectChar");
        }
        else if (CurrentScene == "CharacterSelect" && ready == true)
        {
            //GameManagerScript.SendMessage("GoToMapSelect");
            GameManagerScript.SendMessage("GoToChernobyl");
        }
    }

    void OnPlayerBack()
    {
        if (CurrentScene == "CharacterSelect" && ready == true)
        {
            ready = false;
            CharacterSelector.SendMessage("UnSelectChar");
        }
    }

    void OnMove(InputValue value)
    {
        if(Player != null)
        {
            movement = value.Get<Vector2>();
            PlayerScript.i_movement = movement;
            //Player.SendMessage("Move", i_movement);
            //Debug.Log("imove = " + movement);
        }
        
    }

    void OnAttack()
    {
        Player.SendMessage("Attack");
    }
    void OnJump()
    {
        Player.SendMessage("JumpPrep");
    }

    //MISC FUNCTIONS -----------------------------------------------------------------------------------------------------------------------
    void SetCharacter(Character character)
    {
        Debug.Log(character);
        SelectedCharacter = character;
        PlayerCharacter = character.characterModel;
        ready = true;
    }

    
}
