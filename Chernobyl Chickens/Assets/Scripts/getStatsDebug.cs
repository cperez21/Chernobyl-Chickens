using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getStatsDebug : MonoBehaviour
{
    //This is just used to display puppetmaster stats / health for debug purposes - cullen
    public PlayerController clunk, legolas;
    Text clunkHealthText, LegolasHealthText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //LegolasHealthText = GameObject.Find("Legolas Health").GetComponent<Text>();
        clunkHealthText = GameObject.Find("Clunk Health").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        clunkHealthText.text = "Clunk Health: " + clunk.health.ToString();
    }
}
