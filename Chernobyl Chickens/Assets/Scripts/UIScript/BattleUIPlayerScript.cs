using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIPlayerScript : MonoBehaviour
{

    //player, the character, the portrait, and the object itself.
    public int Playernum;
    public Character PlayerCharacterSerializable;
    public GameObject Portrait;
    public GameObject BattleUIPlayerCell;
    public GameObject pauseMenu;
    public bool GamePaused;

    //healthbar and radbar values
    public Image RadBar;
    public Image HpBar;
    public int maxHealth;
    public float percentageHealth;
    public int currentHealth;
    public float maxRad;
    public float percentageRad;
    public float currentRad;
    
    //finds the controller
    public GameObject PlayerObject;
    public PlayerController ControllerScript; 

    //Calls PlayerInput Object, which exists in PersistentScene
    public GameObject Player;

    //USED FOR AI
    public GameObject spawnPoint;
    public GameObject CPUCharacter;
    public bool allowAI;

    // Start is called before the first frame update
    void Start()
    {

        BattleUIPlayerCell.name = "PlayerCell" + Playernum;
        //set this to player max health in Player Controller
        maxHealth = 100;
        maxRad = 1.0f;
        spawnPoint = GameObject.Find("SpawnPoint" + Playernum);

    }

    // Update is called once per frame
    void Update()
    {
        //if Player isn't filled, find a Player based on PlayerNum, and request the character for information
        if (Player == null)
        {
            Player = GameObject.Find("Player" + Playernum);
            if (Player != null)
            {
                Player.SendMessage("SendCharacter");
            }
            else if (Player == null && allowAI == true)
            {
                SpawnAI();
            }
        }


        //once player is found, set active. sets the portrait to true
        if (Player != null)
        {
            BattleUIPlayerCell.SetActive(true);
            Image icon = Portrait.transform.GetComponent<Image>();
            icon.sprite = PlayerCharacterSerializable.characterSprite;
        }
        

        //Find the Players Character in the Scene, and get the controller.
        if (PlayerObject == null && Player != null)
        {
            PlayerObject = GameObject.Find("PlayerObject" + Playernum);

            ControllerScript = PlayerObject.transform.Find("Model").gameObject.GetComponent<PlayerController>();
        }
        //if player exists, these values show their health and rad
        else if (PlayerObject != null)
        {
            currentHealth = ControllerScript.health;
            percentageHealth = (float)currentHealth / (float)maxHealth;
            HpBar.fillAmount = percentageHealth;

            currentRad = ControllerScript.radiationCount;
            percentageRad = (float)currentRad / (float)maxRad;
            RadBar.fillAmount = percentageRad;

        }
       
        

    }

    void SetCharacterUI(Character character)
    {
        PlayerCharacterSerializable = character;

    }

    void SpawnAI()
    {
        Debug.Log("AIspawned");
        //GameObject plyr = Instantiate(CPUCharacter, spawnPoint.transform.position, Quaternion.identity, ThisObject.transform);
        GameObject plyr = Instantiate(CPUCharacter, spawnPoint.transform.position, Quaternion.identity);
        plyr.name = ("Player" + Playernum);
        PlayerObject = plyr.transform.Find("PlayerObject").gameObject;
        PlayerObject.name = ("PlayerObject" + Playernum);
        ControllerScript = PlayerObject.transform.Find("Model").gameObject.GetComponent<PlayerController>();




        //PlayerObject = Player.transform.find("PlayerObject").gameObject;
        //Player = plyr;
        //ControllerScript = PlayerObject.transform.Find("Model").gameObject.GetComponent<PlayerController>();

        //Player.name = ("Player" + Playernum);
        //PlayerObject.name = ("PlayerObject" + Playernum);


    }

    public void PauseToggle()
    {
        if (GamePaused == false)
        {
            
            GamePaused = true;
            //Pause();
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

        }
        else if (GamePaused == true)
        {
            Debug.Log("resumed");
            GamePaused = false;
            // Resume();
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;

        }
    }

    //public void Resume()
    //{
        
    //}

    //public void Pause()
    //{
        
    //}
}
