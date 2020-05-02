using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleUI : MonoBehaviour
{
    public GameObject pauseContinue, winButton, winMenu;
    public GameObject UI1, UI2, UI3, UI4;
    public bool Alive1, Alive2, Alive3, Alive4;
    public bool GameOver;


    //winner data
    public GameObject Winner;
    public BattleUIPlayerScript WinnerScript;

    public Character winChar;
    public GameObject WinnerPortrait;
    public Text WinnerTag;

    //sets gamemanager and script
    public GameObject GameManager;
    PersistentGameManagerScript GameManagerScript;

    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        GameManagerScript = GameManager.GetComponent<PersistentGameManagerScript>();

        //AI1 = GameManagerScript.AI1;
        //AI2 = GameManagerScript.AI2;
        //AI3 = GameManagerScript.AI3;
        //AI4 = GameManagerScript.AI4;

        Alive1 = true;
        Alive2 = true;
        Alive3 = true;
        Alive4 = true;

        GameOver = false;

        UI1.SendMessage("SetAIMode", GameManagerScript.AI1);
        UI2.SendMessage("SetAIMode", GameManagerScript.AI2);
        UI3.SendMessage("SetAIMode", GameManagerScript.AI3);
        UI4.SendMessage("SetAIMode", GameManagerScript.AI4);


    }



    // Update is called once per frame
    void Update()
    {
        if (GameOver == false)
        {
            CheckForWin();
        }

        //CheckForNullUI();
        

    }


    public void TargetPauseMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(pauseContinue);
    }

    public void TargetWinMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(winButton);
    }

    public void CharDeath1()
    {
        Alive1 = false;
    }
    public void CharDeath2()
    {
        Alive2 = false;
    }
    public void CharDeath3()
    {
        Alive3 = false;
    }
    public void CharDeath4()
    {
        Alive4 = false;
    }

    void CheckForWin() 
    {
        Debug.Log("Win" + ((Alive1 ? 1 : 0) + (Alive2 ? 1 : 0) + (Alive3 ? 1 : 0) + (Alive4 ? 1 : 0)));
        if (((Alive1 ? 1 : 0) + (Alive2 ? 1 : 0) + (Alive3 ? 1 : 0) + (Alive4 ? 1 : 0)) == 1)
        {
            Debug.Log("Winning");
            if (Alive1 == true)
            {
                Debug.Log("Player1 Won!");
                GameOver = true;
                Winner = UI1;
                WinActions();
            }
            else if (Alive2 == true)
            {
                Debug.Log("Player2 Won!");
                GameOver = true;
                Winner = UI2;
                WinActions();
            }
            else if (Alive3 == true)
            {
                Debug.Log("Player3 Won!");
                GameOver = true;
                Winner = UI3;
                WinActions();
            }
            else if (Alive4 == true)
            {
                Debug.Log("Player4 Won!");
                GameOver = true;
                Winner = UI4;
                WinActions();
            }

        }

    }

    void WinActions()
    {
        TargetWinMenu();
        WinnerScript = Winner.GetComponent<BattleUIPlayerScript>();
        winChar = WinnerScript.PlayerCharacterSerializable;

        Image icon = WinnerPortrait.transform.GetComponent<Image>();
        icon.sprite = winChar.characterSprite;

        WinnerTag.text = winChar.vicText;

        winMenu.SetActive(true);

    }

    //void CheckForNull()
    //{

    //}
    


}
