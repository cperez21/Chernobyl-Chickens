using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    private float cooldown = 0;

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (cooldown >= 0.5f)
            {
                other.gameObject.GetComponent<PlayerController>().health -= 5;
                cooldown = 0f;
            }
            else
            {

            }
        }
    }
}
