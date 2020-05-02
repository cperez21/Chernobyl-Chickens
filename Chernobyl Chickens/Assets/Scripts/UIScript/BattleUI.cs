using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleUI : MonoBehaviour
{
    public GameObject pauseContinue, winButton;

    public GameObject UI1, UI2, UI3, UI4;
    //public bool AI1, AI2, AI3, AI4;

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

        UI1.SendMessage("SetAIMode", GameManagerScript.AI1);
        UI2.SendMessage("SetAIMode", GameManagerScript.AI2);
        UI3.SendMessage("SetAIMode", GameManagerScript.AI3);
        UI4.SendMessage("SetAIMode", GameManagerScript.AI4);


    }

   

    // Update is called once per frame
    void Update()
    {


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

}
