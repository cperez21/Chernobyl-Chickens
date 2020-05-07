using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticleEffectSpawn : MonoBehaviour
{
    public ParticleSystem sploosh;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ParticleSystem waterSploosh = Instantiate(sploosh, other.transform.position,Quaternion.identity);
        }
    }


}