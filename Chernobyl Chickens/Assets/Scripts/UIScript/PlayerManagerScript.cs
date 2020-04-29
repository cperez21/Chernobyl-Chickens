using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerManagerScript : MonoBehaviour
{
    //sets gamemanager and script
    public GameObject GameManager;
    PersistentGameManagerScript GameManagerScript;

    public GameObject[] players;
    public bool AllReadyUp;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        GameManagerScript = GameManager.GetComponent<PersistentGameManagerScript>();
        AllReadyUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Length < 4)
        {
            SearchForPlayers();
        }


        //if(players.Length > 0 && AllReadyUp == false)
        if (players.Length > 0)
        {
            CheckForReadyUp();
        }    
    }

    void SearchForPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("PlayerContainer");
    }

    void CheckForReadyUp()
    {
        AllReadyUp = true;
        for (int i = 0; i < players.Length; ++i)
        {
            if (players[i].GetComponent<PlayerInputScript>().ready == false)
            {

                AllReadyUp = false;
                break;
            }
            
        }
         

        //for (int i = 0; i < players.Length; i++)
        //    if (!players[i])
        //        break;

        //AllReadyUp = true;

    }



}
