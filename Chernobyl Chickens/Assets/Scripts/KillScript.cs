using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{

    //GameObject[] players;
    //Vector3[] respawnPoint;
    //public bool respawnOnly;
    //int x = 0;



    //// Start is called before the first frame update
    //void Start()
    //{
    //    players = GameObject.FindGameObjectsWithTag("Player");
    //    //Grabs initial position of each player to use as a 'spawn' point.
    //    for (x = 0;x < players.Length; x++)
    //    {
    //        respawnPoint[x] = players[x].transform.position;
    //    }
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //}
    //private void OnCollisionEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "player")
    //    {
    //        StartCoroutine
    //    }
    //} 

    private float cooldown = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().health = 0;
            
        }
    }

}
