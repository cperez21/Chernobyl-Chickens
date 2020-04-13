using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationScript : MonoBehaviour
{
    [Header("All of these values are executed at a frame by frame rate")]
    
    public int addHealthAmount;
    public bool giveHealth;
    private bool addHealthEnabled = true;
    [SerializeField, Range(0.001f,0.005f)] public float addRadsAmount; //value has to be small because 1.0 is max

   IEnumerator AddHealth()
    {
        addHealthEnabled = false;
        yield return new WaitForSeconds(1f);
        giveHealth = true;
        addHealthEnabled = true;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other);
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.SendMessage("SetShaderColor", player.gameObject.GetComponent<PlayerShaderFunctions>().radiationColor);
            //Do not switch state if in cooldown
            if (player.radState != PlayerController.RadiationState.COOLDOWN)
            {
               
                player.radState = PlayerController.RadiationState.GAINING;
                player.radiationCount += addRadsAmount; //Changed to public variables for ez modifying

                //To prevent coroutine from being spammed every frame
                if (addHealthEnabled)
                {
                    StartCoroutine("AddHealth");

                }
                //because I cant reference the player
                if (giveHealth)
                {
                    player.health += addHealthAmount;
                    giveHealth = false;
                }
                
              
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            player.StartCoroutine("RadCooldown");
            player.radState = PlayerController.RadiationState.COOLDOWN;
            
        }
    }


    //public void radiationIncrease()
    //{
    //    if (cooldown >= 1.0f)
    //    {
    //        other.gameObject.GetComponent<PlayerController>().radiationCount += 30;
    //        cooldown = 0f;
    //    }
    //    else
    //    {

    //    }

    //}
}
