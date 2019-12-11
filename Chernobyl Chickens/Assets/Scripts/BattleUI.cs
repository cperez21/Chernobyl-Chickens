using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    //NOTE - make sure event manager has input for horizontal and vertical crap present


    //creates variables for Player1
    public int currentHealth1;
    public GameObject player1;
    public Text healthText1;

    //creates variables for Player2
    public int currentHealth2;
    public GameObject player2;
    public Text healthText2;

    //WIN/LOSEdialogue
    //public GameObject dialoguepanel;


    //pausemenu variables
    public static bool GamePaused = false;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
        //for player1
        player1 = GameObject.Find("Clunk");
        //print(player1);

        //for player2
        player2 = GameObject.Find("Legolas");
        //print(player2);
        
    
        
    }

   

    // Update is called once per frame
    void Update()
    {
        //for player1 *********************************************************UNCOMMMENT LATER
        //currentHealth1 = player1.GetComponent<PlayerController>().health;
        //healthText1.text = "Player 1 :" + currentHealth1.ToString();

        //for player2 *********************************************************UNCOMMMENT LATER
        //currentHealth2 = player2.GetComponent<PlayerController>().health;
        //healthText2.text = "Player 2 :" + currentHealth2.ToString();


        //if(currentHealth1 <= 0) {
        //    dialoguepanel.SetActive(true);
        //}
        //else if(currentHealth2 <=0)
        //{
        //    dialoguepanel.SetActive(true);
        //}

        //pause menu logic
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("read");
            if (GamePaused)
            {
                //Debug.Log("resume");
                Resume();
            }
            else
            {
                //Debug.Log("pause");
                Pause();
            }
        }


    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

}
