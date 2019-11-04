using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float playerNumber;
    private Rigidbody rb;
    private float dirX;
    public float mSpeed, jumpForce;
    Vector3 respawnPoint;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mSpeed = 5f;
        respawnPoint = transform.position;
        
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

        

    }
    private void FixedUpdate()
    {
        
    }
     void OnTriggerEnter(Collider coll)
    {


        //Moves player back to position they started at if they hit a killbox.
        if (coll.gameObject.tag == "KillBox")
        {
            transform.position = respawnPoint;
            
        }
    }

}
