﻿using System.Collections;
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

    //winmenu
    public GameObject winMenu;
    public GameObject victory1;
    public GameObject victory2;

    //pausemenu variables
    public static bool GamePaused = false;
    public GameObject pauseMenu;
    public GameObject uiBoxes;

    //STYLIZATION
    //var speed = 1.0f; //how fast it shakes
    //var amount = 1.0f; //how much it shakes

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
        //for player1 
        currentHealth1 = player1.GetComponent<PlayerController>().health;
        healthText1.text = "Clunk :" + currentHealth1.ToString();

        //for player2 
        currentHealth2 = player2.GetComponent<PlayerController>().health;
        healthText2.text = "Legolas:" + currentHealth2.ToString();

        //winMenu logic - when someone dies it shows - COMPLETE
        if (currentHealth1 <= 0)
        {
            winMenu.SetActive(true);
            victory2.SetActive(true);
            uiBoxes.SetActive(false);
        }
        else if (currentHealth2 <= 0)
        {
            winMenu.SetActive(true);
            victory1.SetActive(true);
            uiBoxes.SetActive(false);
        }

        //pause menu logic - COMPLETE
        if (Input.GetKeyDown(KeyCode.P))
        {

            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        //STYLIZATION - WIP
        //transform.position.x = Mathf.Sin(Time.time * speed) * amount;

        //TESTING ZONE BELOW
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("pow");
            pow();
        }

    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        //uiBoxes.SetActive(true);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        //uiBoxes.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    //for testing
    public void pow()
    {
        player2.GetComponent<PlayerController>().health -= 10;
        Debug.Log(currentHealth2);
    }

}