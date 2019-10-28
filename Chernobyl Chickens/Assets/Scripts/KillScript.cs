using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{

    GameObject[] players;
    Vector3[] respawnPoint;
    public bool respawnOnly;
    int x = 0;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        //Grabs initial position of each player to use as a 'spawn' point.
        for (x = 0;x < players.Length; x++)
        {
            respawnPoint[x] = players[x].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* private void OnCollisionEnter(Collider coll)
    {
        if(coll == players[x] && respawnOnly)
        {
            players[x].transform.position = respawnPoint[x];
        }
    
    
    
    } 

*/

}
