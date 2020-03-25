using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TargetGroupExtraSettingsScript : MonoBehaviour //This is used to stop tracking characters when they are killed. -Cullen
{
   // [Header("NOT the PuppetMaster. ex: Legolas > Bip001")]
   // [Header("Cinemachine Targes are set to 'Bip001' from PlayerController")]

    
    private CinemachineTargetGroup tg;
    private List<PlayerController> pcs;
    

    public enum Setting
        {
        Auto,
        Manual
        }


    [Header("Leave on Auto unless you are playing with the camera")]
    [Tooltip("Auto will set the camera to follow all chara")]
    public Setting findPlayers;


    // Start is called before the first frame update
    void Start()
    {
        tg = gameObject.GetComponent<CinemachineTargetGroup>();


        switch (findPlayers) //if you would like the camera group targets to be set automatically by Player tag, or do it yourself. -Cullen
        {
            case Setting.Auto:

                int originalTargetsLength = tg.m_Targets.Length; //variable made so the loop doesn't hurt itself in confusion -Cullen

               for( int x = tg.m_Targets.Length -1; x >= 0 ; x--) //Clears out existing Target group
                {
                   
                    if(x == 0) //Final part
                    {
                        tg.m_Targets[x].target = null;
                    }
                    else
                    tg.RemoveMember(tg.m_Targets[x].target);

                    
                }

                var characters = GameObject.FindObjectsOfType<PlayerController>(); //Finds Player Controllers
                

                for(int x = 0; x < characters.Length; x++)
                {
                    if(x == 0)
                    {
                        tg.m_Targets[x].target = characters[x].gameObject.transform.GetChild(0); //sets the final one instead of adding one more
                    }
                    else
                    tg.AddMember(characters[x].gameObject.transform.GetChild(0),1,0); //Adds Players to target group.
                }

                
                


                break;

            case Setting.Manual:


                break;
        }


    }

    // Update is called once per frame
    void Update()
    {
       
        for(int x = 0; x < tg.m_Targets.Length; x++)
        {
            if(tg.m_Targets[x].target.GetComponentInParent<PlayerController>().state == PlayerController.PlayerState.DEAD)
            {
                tg.m_Targets[x].target = null;
            }
        }
       
    }

}
