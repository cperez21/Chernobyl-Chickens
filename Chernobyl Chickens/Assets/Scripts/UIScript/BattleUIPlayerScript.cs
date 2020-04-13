using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIPlayerScript : MonoBehaviour
{
    public int Playernum;
    public Character PlayerCharacterSerializable;
    public GameObject Portrait;
    public GameObject BattleUIPlayerCell;

    //healtbarstuff
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

    // Start is called before the first frame update
    void Start()
    {
        BattleUIPlayerCell.name = "PlayerCell" + Playernum;
        //set this to player max health in Player Controller
        maxHealth = 100;
        maxRad = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player" + Playernum);
            if (Player != null)
            {
                Player.SendMessage("SendCharacter");
            }
        }

        if (Player != null)
        {
            BattleUIPlayerCell.SetActive(true);
        }

        Image icon = Portrait.transform.GetComponent<Image>();
        icon.sprite = PlayerCharacterSerializable.characterSprite;

        if (PlayerObject == null)
        {
            PlayerObject = GameObject.Find("PlayerObject" + Playernum);

            ControllerScript = PlayerObject.transform.Find("Model").gameObject.GetComponent<PlayerController>();
        }
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


}
