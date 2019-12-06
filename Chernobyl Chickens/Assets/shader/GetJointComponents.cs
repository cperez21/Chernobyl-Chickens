using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetJointComponents : MonoBehaviour
{
    public Collider col;
   public Rigidbody rBody;
    

    
    
    
    // Start is called before the first frame update
    void Start()
    {

        col = gameObject.GetComponent<BoxCollider>();
        rBody = gameObject.AddComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
