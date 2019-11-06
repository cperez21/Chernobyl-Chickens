using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    GameObject[] players;
    PlayerController[] playerControllers;
    int playersLeft; //players that aren't dead
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Finds all players in scene.
        players = GameObject.FindGameObjectsWithTag("Player");
        playersLeft = players.Length;
        for(int x = 0; x < players.Length; x++)
        {
            //Assigns player number to each player.
            playerControllers[x] = players[x].GetComponent<PlayerController>();
            playerControllers[x].playerNumber = x + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int x = 0; x < players.Length; x++)
        {
           if(playerControllers[x].state == PlayerController.PlayerState.DEAD)
            {
                //playersLeft--;
            }
        }

       
        
    }
}
