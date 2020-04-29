using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManagerScript : MonoBehaviour
{
    //sets gamemanager and script
    public GameObject GameManager;
    PersistentGameManagerScript GameManagerScript;
    //sets playermanager script
    PlayerManagerScript PManagerScript;

    public GameObject UIBeforePlayersContainer;
    public GameObject UIMainMenuContainer;
    public GameObject UICharacterSelect;
    public GameObject ReadyUpBar;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        GameManagerScript = GameManager.GetComponent<PersistentGameManagerScript>();

        PManagerScript = GameObject.Find("PlayerManager").GetComponent<PlayerManagerScript>();

        if (GameManagerScript.PlayersJoined == true)
        {
            SwitchToMainMenu();
        }
        //UIBeforePlayersContainer = GameObject.Find("BeforePlayers");
        //UIMainMenuContainer = GameObject.Find("MainMenuContainer");
        //UICharacterSelect = GameObject.Find("CharacterSelectCanvas");
        //UIMapSelect = GameObject.Find("MapSelectCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForReadyUp();
    }

    public void SwitchToMainMenu()
    {
        //Should only occur once when game starts
        if (UIBeforePlayersContainer.activeSelf == true)
        {
            UIBeforePlayersContainer.SetActive(false);
        }
        //For Character Select
        if (UICharacterSelect.activeSelf == true)
        {
            UICharacterSelect.SetActive(false);
        }
        //For Main Menu - ACTIVATED
        if (UIMainMenuContainer.activeSelf == false)
        {
            UIMainMenuContainer.SetActive(true);
        }



        

    }

    public void SwitchToCharSel()
    {
        //Should only occur once when game starts
        if (UIBeforePlayersContainer.activeSelf == true)
        {
            UIBeforePlayersContainer.SetActive(false);
        }
        //For Character Select - ACTIVATED
        if (UICharacterSelect.activeSelf == false)
        {
            UICharacterSelect.SetActive(true);
        }
        //For Main Menus
        if (UIMainMenuContainer.activeSelf == true)
        {
            UIMainMenuContainer.SetActive(false);
        }
        GameManagerScript.LoadedScene = "CharacterSelect";
    }

    void CheckForReadyUp()
    {
        if (PManagerScript.AllReadyUp == true)
        {
            ReadyUpBar.SetActive(true);
        }
        else
        {
            ReadyUpBar.SetActive(false);
        }
    }
    //may be redundant. currently being handled in playerinputsript under playerselect
    //public void SwitchToMapSel()
    //{
    //    GameManagerScript.SendMessage("GoToMapSelect");
    //    ////Should only occur once when game starts
    //    //if (UIBeforePlayersContainer.activeSelf == true)
    //    //{
    //    //    UIBeforePlayersContainer.SetActive(false);
    //    //}
    //    ////For Character Select 
    //    //if (UICharacterSelect.activeSelf == true)
    //    //{
    //    //    UICharacterSelect.SetActive(false);
    //    //}
    //    ////For Map Select - ACTIVATED
    //    //if (UIMapSelect.activeSelf == false)
    //    //{
    //    //    UIMapSelect.SetActive(true);
    //    //}
    //    ////For Main Menus
    //    //if (UIMainMenuContainer.activeSelf == true)
    //    //{
    //    //    UIMainMenuContainer.SetActive(false);
    //    //}
    //    //GameManagerScript.LoadedScene = "MapSelect";
    //}

    
}
