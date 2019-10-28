using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject[] players;
    Vector3 averageP, totalP;
    private float minX, minY, maxX, maxY;
    public float x, y, w, h;
    Camera cam;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //makes array of all Players in game scene
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(players);
        cam = GetComponent<Camera>();
        
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Resets Total x and y values from calculating average
        totalP = Vector3.zero;

        //Calculates the total position values for all players in scene.
        for (int x = 0; x < players.Length; x++)
        {
            
            if (x == 0)
            {
                minX = players[x].transform.position.x;
                maxX = players[x].transform.position.x;

                minY = players[x].transform.position.y;
                maxY = players[x].transform.position.y;
            }
            else
            {
                minX = Mathf.Min(minX, players[x].transform.position.x);
                maxX = Mathf.Max(maxX, players[x].transform.position.x);

                minY = Mathf.Min(minY, players[x].transform.position.y);
                maxY = Mathf.Max(maxY, players[x].transform.position.y);
            }


            totalP.x += players[x].transform.position.x;
            totalP.y += players[x].transform.position.y;

           

        }

       

        //Grabs average of total x and y coordinates of players and assigns camera to the average position
        averageP.x = totalP.x / players.Length;
        averageP.y = totalP.y / players.Length;
        averageP.z = transform.position.z;
        transform.position = averageP;


       //cam.rect = new Rect(0,0,0,0);
        
            
             


    }
}
