using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleUIPlayerScript : MonoBehaviour
{

    //player, the character, the portrait, and the object itself.
    public int Playernum;
    public Character PlayerCharacterSerializable;
    public GameObject Portrait;
    public GameObject BattleUIPlayerCell;
    //public GameObject pauseMenu;
    //public bool GamePaused;
    public Image RadBar;
    public Image HpBar;
    public GameObject PlayerText;

    //healthbar and radbar values, and death
    public int maxHealth;
    public float percentageHealth;
    public int currentHealth;
    public float maxRad;
    public float percentageRad;
    public float currentRad;
    public bool death;
    
    //finds the controller and battleUIScript

    public GameObject PlayerObject;
    public PlayerController ControllerScript;
    public BattleUI BUIScript;

    //Calls PlayerInput Object, which exists in PersistentScene
    public GameObject Player;


    //USED FOR AI
    public GameObject spawnPoint;
    public GameObject CPUCharacter;
    public bool allowAI;

    // Start is called before the first frame update
    void Start()
    {

        BUIScript = GameObject.Find("BattleUICanvas").GetComponent<BattleUI>();
        BattleUIPlayerCell.name = "PlayerCell" + Playernum;
        //set this to player max health in Player Controller
        maxHealth = 100;
        maxRad = 1.0f;
        spawnPoint = GameObject.Find("SpawnPoint" + Playernum);
        death = false;

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
            else
            {
                death = true;
                BUIScript.SendMessage("CharDeath" + Playernum);
            }
        }


        //once player is found, set active. sets the portrait to true
        if (Player != null && BattleUIPlayerCell.activeSelf == false)
        {
            BattleUIPlayerCell.SetActive(true);
            
            Image icon = Portrait.transform.GetComponent<Image>();
            icon.sprite = PlayerCharacterSerializable.characterSprite;
            death = false;

            if(allowAI == false)
            {
                PlayerText.SetActive(true);
            }

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

            CheckForDeath();
            CheckForBoss();
        }

    }

    void SetCharacterUI(Character character)
    {
        PlayerCharacterSerializable = character;

    }

    public void SetAIMode(bool AIbool)
    {
        allowAI = AIbool;
    }

    void SpawnAI()
    {
        //Debug.Log("AIspawned");
        //GameObject plyr = Instantiate(CPUCharacter, spawnPoint.transform.position, Quaternion.identity, ThisObject.transform);
        GameObject plyr = Instantiate(CPUCharacter, spawnPoint.transform.position, Quaternion.identity);
        //plyr.name = ("Player" + Playernum);

        plyr.name = ("AIPlayer");
        Player = plyr;

        PlayerObject = plyr.transform.Find("Model").gameObject;
        PlayerObject.name = ("PlayerObject" + Playernum);
        //ControllerScript = PlayerObject.transform.Find("Model").gameObject.GetComponent<PlayerController>();
        ControllerScript = PlayerObject.GetComponent<PlayerController>();



        //PlayerObject = Player.transform.find("PlayerObject").gameObject;
        //Player = plyr;
        //ControllerScript = PlayerObject.transform.Find("Model").gameObject.GetComponent<PlayerController>();

        //Player.name = ("Player" + Playernum);
        //PlayerObject.name = ("PlayerObject" + Playernum);


    }

    public void CheckForDeath()
    {
        //if (PlayerObject != null && currentHealth <= 0 && death == false)
        if (currentHealth <= 0 && death == false)
        {
            death = true;
            BUIScript.SendMessage("CharDeath" + Playernum);
        }
    }

    public void CheckForBoss()
    {
        //Debug.Log(ControllerScript.radiationCount);
        if (ControllerScript.radiationCount >= maxRad && BUIScript.BossMode == false)
        {
            BUIScript.SendMessage("BossModeActivate");
            //Debug.Log("BOSSMOOOOOODDEEE");
        }
    }

}
