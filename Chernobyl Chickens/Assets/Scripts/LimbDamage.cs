using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbDamage : MonoBehaviour
{
    public string bodyPart;
    [Header("Do not adjust these settings. For Debug only.")]
    public bool gotHit = false;
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
            magnitudeThreshold = 8.0f;
        }
       else if(bodyPart.Contains("Thigh") || bodyPart.Contains("Calf"))
        {
            baseDamage = 1f;
        }
        else if(bodyPart.Contains("Pelvis") || bodyPart.Contains("Spine"))
        {
            baseDamage = 2.5f;
            magnitudeThreshold = 5.0f;
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

        if (magnitude <= magnitudeThreshold)
        {
            magnitude = 0.0f;
        }
            totalDamage = (baseDamage * magnitude) *2;
        totalNormalizedDamage = (totalDamage / 20);
        
        if (totalNormalizedDamage >= 1f)
        {
            gotHit = true;
           
            
        }
        
    }
     void OnCollisionExit(Collision collision)
    {
        gotHit = false;    
    }



}
