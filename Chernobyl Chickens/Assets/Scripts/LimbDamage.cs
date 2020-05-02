﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbDamage : MonoBehaviour
{
    public string bodyPart;
    [Header("Do not adjust these settings. For Debug only.")]
    public bool hurtOnly = false; //Will prevent limb from taking damage and only giving damage. Ex: limbs you want to attack only with like fists, feet, etc.
    public bool gotHit = false;
    public bool gotPushed;
    public bool canPush = false; //canPush version for the limbs.
    public bool canHurt = false; //canHurt version for the limbs. Playercontroller object does not physically collide with the limbs.
    public PlayerController selfController;
    public float baseDamage = 0f;
   // public Vector3 impulseVector;
   // public Vector3 impulseVectorNormalized;
   // public float totalImpulseAverage;
    public float magnitude = 0f;
    public float magnitudeThreshold = 1f; //How much velocity the collision will have to actually take damage.
    public float totalDamage = 0f;
    public float totalNormalizedDamage = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        bodyPart = gameObject.name;

        if (bodyPart.Contains("Head"))
        {
            baseDamage = 5f;
            magnitudeThreshold = 2.0f;
        }
        else if(bodyPart.Contains("arm"))
        {
            baseDamage = 1f;
            magnitudeThreshold = 2.0f;
        }
       else if(bodyPart.Contains("Thigh") || bodyPart.Contains("Calf"))
        {
            baseDamage = 1f;
        }
        else if(bodyPart.Contains("Pelvis") || bodyPart.Contains("Spine"))
        {
            baseDamage = 2.5f;
            magnitudeThreshold = 2.0f;
        }
       else
        {
            Debug.LogError("LimbDamage is not finding the proper body part string for " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(selfController.canPush)
        {
            canPush = true;
        }
        else
        {
            canPush = false;
        }
        if (selfController.canHurt) //if player is in attacking state
            canHurt = true;
        else
            canHurt = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (hurtOnly == true)
        {
            return;
        }
        else
        {
            //hit by push attack
            if (collision.gameObject.GetComponent<LimbDamage>().canPush)
            {
                gotPushed = true;
                //Vector3 dir = collision.GetContact(0).point - transform.position;
                Vector3 dir = collision.transform.position - selfController.transform.position;
                dir.Set(dir.x, 0f, dir.z);
                dir = -dir.normalized;
                selfController.gameObject.GetComponent<Rigidbody>().AddForce(dir * 600f);
            }


            if (collision.gameObject.GetComponent<LimbDamage>().canHurt) //checks if the opponenet is actually attacking and limbs take damage
            {


                magnitude = collision.relativeVelocity.magnitude;

                if (magnitude <= magnitudeThreshold)
                {
                    magnitude = 0.0f;
                }
                totalDamage = (baseDamage * magnitude) * 2;
                totalNormalizedDamage = (totalDamage / 20);

                if (totalNormalizedDamage >= 1f)
                {
                    gotHit = true;
                    //small knockback
                    //Vector3 dir = collision.GetContact(0).point - transform.position;
                    Vector3 dir = collision.transform.position - selfController.transform.position;
                    dir.Set(dir.x, 0f, dir.z);
                    dir = -dir.normalized;
                    selfController.gameObject.GetComponent<Rigidbody>().AddForce(dir * 200f);

                }
            }

        }

        
    }
     void OnCollisionExit(Collision collision)
    {
        gotHit = false;
        gotPushed = false;
    }



}
