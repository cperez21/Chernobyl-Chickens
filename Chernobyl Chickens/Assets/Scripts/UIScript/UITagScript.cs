using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITagScript : MonoBehaviour
{
    public Text playerLabel;

    
    //public GameObject BUI;

    //public GameObject Player;
    public PlayerInputScript PIS;
    public int PlayerCount;

    // Update is called once per frame
    void Update()
    {
        if (playerLabel == null)
        {
            SetUpText();
        }

        Vector3 namePose = Camera.main.WorldToScreenPoint(this.transform.position);
        playerLabel.transform.position = namePose;
        //Debug.Log(transform.root);
    }

    public void SetUpText()
    {
        // = transform.root;
        if(PlayerCount == null || PlayerCount == 0)
        {
            PIS = transform.root.GetComponent<PlayerInputScript>();
            PlayerCount = PIS.PlayerCount;
        }
        

        //BUI = GameObject.Find("BattleUICanvas");

        playerLabel = GameObject.Find("PlayerTag" + PlayerCount).GetComponent<Text>();

        if (playerLabel.text  == "P1")
        {
            playerLabel.color = new Color32(255, 134, 20, 255);
        }
        else if (playerLabel.text == "P2")
        {
            playerLabel.color = new Color32(134, 255, 20, 255);
        }
        else if(playerLabel.text == "P3")
        {
            playerLabel.color = new Color32(20, 255, 134, 255);

        }
        else if(playerLabel.text == "P4")
        {
            playerLabel.color = new Color32(20, 134, 255, 255);
        }
    }
}
