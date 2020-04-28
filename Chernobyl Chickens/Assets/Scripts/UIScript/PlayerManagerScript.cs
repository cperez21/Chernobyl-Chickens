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

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        GameManagerScript = GameManager.GetComponent<PersistentGameManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (players.Length < 4)
        {
            SearchForPlayers();
        }
        
            
    }

    void SearchForPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("PlayerContainer");
    }
}
