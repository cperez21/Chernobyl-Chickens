using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody rb;
    private float dirX;
    public float mSpeed, jumpForce;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mSpeed = 5f;
        //jumpForce = 20f;
        
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal") * mSpeed;
        //dirY = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(dirX, rb.velocity.y, 0);

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

        Debug.Log("velocity is " + rb.velocity);

    }
    private void FixedUpdate()
    {
        
    }


}
