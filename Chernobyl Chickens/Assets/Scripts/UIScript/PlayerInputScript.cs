using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;


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

    //public bool p_joined;


    //for character selectorscreen
    public bool ready;
    public GameObject CharacterSelector;
    public Character SelectedCharacter;
    private bool hasKeyboard, hasMouse;
    private PlayerInput pInput;


    //for gameplay
    public bool spawned;
    Vector2 movement;
    public GameObject ThisObject;
    public GameObject PlayerCharacter;
    public GameObject Player;
    PlayerController PlayerScript;
    public GameObject spawnPoint;

    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        GameManagerScript = GameManager.GetComponent<PersistentGameManagerScript>();

        pInput = GetComponent<PlayerInput>();

        players = GameObject.FindGameObjectsWithTag("PlayerContainer");
        foreach (GameObject PlayerContainer in players)
        {
            PlayerCount += 1;
        }
        this.name = "Player" + PlayerCount;
        ThisObject = GameObject.Find("Player" + PlayerCount);
        GameManagerScript.SendMessage("PlayerJoin", ThisObject);
        //p_joined = false;
        ready = false;

        //Checks if controller has mouse or keyboard
        if(pInput.devices[0].name.Contains("Keyboard"))
        {
            hasKeyboard = true;
        }
        if(pInput.devices[0].name.Contains("Mouse"))
        {
            hasMouse = true;
        }


        //adds the mouse or keyboard to the existing keyboard/mouse player.
        if(hasKeyboard && !hasMouse)
        {
            InputDevice mouse = InputSystem.GetDevice<Mouse>();

            InputUser.PerformPairingWithDevice(mouse, pInput.user);
        }
        if(hasMouse && !hasKeyboard)
        {
            InputDevice keys = InputSystem.GetDevice<Keyboard>();

            InputUser.PerformPairingWithDevice(keys, pInput.user);
        }


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
        //    spawnPoint = GameObject.Find("SpawnPoint" + PlayerCount);
        //}

        spawnPoint = GameObject.Find("SpawnPoint" + PlayerCount);
        if(spawnPoint != null && spawned == false)
        {
            spawned = true;
            GameObject plyr = Instantiate(PlayerCharacter, spawnPoint.transform.position, Quaternion.identity, ThisObject.transform);
            plyr.name = ("PlayerObject" + PlayerCount);
            Player = plyr.transform.Find("Model").gameObject;
            PlayerScript = Player.GetComponent<PlayerController>();
            //NewPlayer.transform.parent = ThisObject.transform;
            spawned = true;
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
            //GameManagerScript.SendMessage("CharSelReadyUp");
            //GameManagerScript.SendMessage("GoToChernobyl");
            GameManagerScript.SendMessage("GoToMapSelect");
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

    void OnSprint()
    {
        Player.SendMessage("Sprint");
    }

    void OnPush()
    {
        Player.SendMessage("Push");
    }

    void OnAttack()
    {
        Player.SendMessage("Attack");
    }
    void OnJump()
    {
        if (CurrentScene == "CharacterSelect" )
        {

        }
        else if (CurrentScene == "MenuScene" )
        {

        }
        else
        {
            Player.SendMessage("JumpPrep");
        }

        
    }

    void OnPause()
    {
        Debug.Log("buttonpressed");
        if (CurrentScene == "CharacterSelect" || CurrentScene == "MenuScene")
        {
            //Debug.Log("badscene");
        }
        else
        {
            //Debug.Log("sending");
            GameObject.Find("PlayerCell" + PlayerCount).SendMessage("PauseToggle");
            //Debug.Log("sent");
        }

    }

    //MISC FUNCTIONS -----------------------------------------------------------------------------------------------------------------------
    void SetCharacter(Character character)
    {
        Debug.Log(character);
        SelectedCharacter = character;
        PlayerCharacter = character.characterModel;
        ready = true;
    }

    void SendCharacter()
    {
        GameObject.Find("PlayerCell" + PlayerCount).SendMessage("SetCharacterUI", SelectedCharacter);
    }

    void SpawnCharacter()
    {

    }
}
