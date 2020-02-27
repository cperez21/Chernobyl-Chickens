using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationScript : MonoBehaviour
{
    private float cooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.tag == "Player")
        {
            if (cooldown >= 1.0f)
            {
                other.gameObject.GetComponent<PlayerController>().radiationCount += 30;
                other.gameObject.GetComponent<PlayerController>().health += 10;
                cooldown = 0f;
            }
            else
            {

            }
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
