using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintTransform : MonoBehaviour
{
    Transform myTransform;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name + "Transform position is " + transform.position);
        if(gameObject.GetComponent<SkinnedMeshRenderer>())
        {
           SkinnedMeshRenderer smr = gameObject.GetComponent<SkinnedMeshRenderer>();
            //Debug.Log(gameObject.name + "Transform position is " + smr.ce)
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
