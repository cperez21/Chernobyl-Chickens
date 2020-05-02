using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharacterSelectorScript : MonoBehaviour
{
    public List<Character> characters = new List<Character>();
    //public GameObject charCellPrefab;
    public Character CurrentCharacter;
    public int CharacterSelectNum;
    //public bool Active;
    public GameObject CharacterCell;
    public GameObject Player;
    public GameObject Portrait;
    public Text PlayerName;

    public Text ReadyText;
    public GameObject ReadyBox;

    public int CharacterCount;
    public int CharacterVal;

    public bool AIMode;

    //sets gamemanager and script
    public GameObject GameManager;
    public string AInum;
    PersistentGameManagerScript GameManagerScript;


    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        GameManagerScript = GameManager.GetComponent<PersistentGameManagerScript>();


        foreach (Character character in characters)
        {
            CharacterCount += 1;
            //CharacterToggle(character);
            //Debug.Log(character);
           // Active = false;
        }
        CurrentCharacter = characters[0];
        CharacterVal = 0;
        ReadyText.text = "JOIN/Set AI";
        PlayerName.text = "Player " + CharacterSelectNum;
        AIMode = false;
    }

    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player" + CharacterSelectNum);
        }
        
        if (Player !=null && ReadyText.text != "READY!")
        {
            CharacterCell.SetActive(true);
            ReadyText.text = "SELECT";
        }

        Image icon = Portrait.transform.GetComponent<Image>();
        icon.sprite = CurrentCharacter.characterSprite;
    }


    public void CharacterToggleLeft()
    {
        Debug.Log("Left");
        if (CharacterVal == 0)
        {
            CharacterVal = 4;
            CurrentCharacter = characters[CharacterVal];
        }
        else
        {
            CharacterVal -= 1;
            CurrentCharacter = characters[CharacterVal];
        }

    }
    public void CharacterToggleRight()
    {
        Debug.Log("Right");
        if (CharacterVal == 4)
        {
            CharacterVal = 0;
            CurrentCharacter = characters[CharacterVal];
        }
        else
        {
            CharacterVal += 1;
            CurrentCharacter = characters[CharacterVal];
        }
    }

    void SelectChar()
    {
        Player.SendMessage("SetCharacter", CurrentCharacter);
        Debug.Log(CurrentCharacter);
        ReadyText.text = "READY!";
        ReadyBox.GetComponent<Image>().color = new Color32(255, 134, 20, 219);
    }
    void UnSelectChar()
    {
        ReadyText.text = "SELECT";
        ReadyBox.GetComponent<Image>().color = new Color32(60, 60, 60, 219);
    }

    //used by ready button
    public void SelectToggle()
    {
        if(ReadyText.text == "SELECT")
        {
            SelectChar();
        }
        else if (ReadyText.text == "READY!")
        {
            UnSelectChar();
            Player.SendMessage("ReadyFalse");
        }
    }

    //sets AI
    public void SelectAI()
    {

        if (Player == null && AIMode == false)
        {
            


            ReadyText.text = "AI MODE";
            ReadyBox.GetComponent<Image>().color = new Color32(60, 215, 60, 219);
            AIMode = true;
            GameManagerScript.SendMessage("toggle" + AInum);
        }
        else if (Player == null && AIMode == true)
        {
            ReadyText.text = "JOIN/Set AI";
            ReadyBox.GetComponent<Image>().color = new Color32(60, 60, 60, 219);
            GameManagerScript.SendMessage("toggle" + AInum);
        }

    }

}
