using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerScript : MonoBehaviour
{
    //sets gamemanager and script
    public GameObject GameManager;
    PersistentGameManagerScript GameManagerScript;

    public GameObject UIBeforePlayersContainer;
    public GameObject UIMainMenuContainer;
    public GameObject UICharacterSelect;
    public GameObject UIMapSelect;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        GameManagerScript = GameManager.GetComponent<PersistentGameManagerScript>();

        UIBeforePlayersContainer = GameObject.Find("BeforePlayers");
        UIMainMenuContainer = GameObject.Find("MainMenuContainer");
        UICharacterSelect = GameObject.Find("CharacterSelectCanvas");
        UIMapSelect = GameObject.Find("MapSelectCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToMainMenu()
    {

    }
}
