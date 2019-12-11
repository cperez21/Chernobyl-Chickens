using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbDamage : MonoBehaviour
{
    public string bodyPart;
    [Header("Do not adjust these settings. For Debug only.")]
    public bool gotHit = false;
    public float baseDamage = 50f;
   // public Vector3 impulseVector;
   // public Vector3 impulseVectorNormalized;
   // public float totalImpulseAverage;
    public float magnitude = 0f;
    public float totalDamage = 0f;
    public float totalNormalizedDamage = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        bodyPart = gameObject.name;

        if (bodyPart.Contains("Head"))
        {
            baseDamage = 50f;
        }
        else if(bodyPart.Contains("arm"))
        {
            baseDamage = 10f;
        }
       else if(bodyPart.Contains("Thigh") || bodyPart.Contains("Calf"))
        {
            baseDamage = 10f;
        }
        else if(bodyPart.Contains("Pelvis"))
        {
            baseDamage = 25;
        }
       else
        {
            Debug.LogError("LimbDamage is not finding the proper body part string for " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        //impulseVector = collision.impulse;
        //totalImpulseAverage = (impulseVector.x + impulseVector.y + impulseVector.z) / 3f; //+ impulseVector.y + impulseVector.z;
        magnitude = collision.relativeVelocity.magnitude;
        if(magnitude < 4.0)
        {
            magnitude = 0.0f;
        }
        totalDamage = baseDamage * magnitude;
        totalNormalizedDamage = (totalDamage / 2000);

        if (totalNormalizedDamage >= 0.01)
        {
            gotHit = true;
           
            
        }
        
    }
     void OnCollisionExit(Collision collision)
    {
        gotHit = false;    
    }



}
