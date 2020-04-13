using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeathersScript : MonoBehaviour
{
    ParticleSystem feathers;
    ParticleCollisionEvent[] colEvents;
    ParticleSystem.VelocityOverLifetimeModule feathersVelocity;
    
    
    // Start is called before the first frame update
    void Start()
    {
        feathers = gameObject.GetComponent<ParticleSystem>();
        feathersVelocity = feathers.velocityOverLifetime;
        colEvents = new ParticleCollisionEvent[8]; //set to any size for default
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {

        
        




        /*
        
        int collcount = feathers.GetSafeCollisionEventSize(); //gets the amount of particle collisions on this frame

        if(collcount > colEvents.Length)
        {
            colEvents = new ParticleCollisionEvent[collcount]; //Updates array size to hold all collision events
        }

        int eventCount = feathers.GetCollisionEvents(other, colEvents);

        for(int x = 0; x < eventCount; x++)
        {
            colEvents[x]
        }
        */

        
    }



}
