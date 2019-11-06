using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float playerNumber;
    private Rigidbody rb;
    private float dirX;
    public float moveSpeed, jumpForce;
    public LayerMask groundLayer;
    Vector3 respawnPoint;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 5f;
        respawnPoint = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal") * moveSpeed;
        //dirY = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(dirX, rb.velocity.y, 0);

        if(Input.GetButton("Jump"))
        {
            Jump();
        }
        
        
        //Debug.DrawRay(transform.position, Vector3.down, Color.blue, );

    }
    private void FixedUpdate()
    {
        
    }

    void Jump()
    {

        if (isGrounded())
            rb.AddForce(Vector3.up * jumpForce);
        else
            return;
    }

    bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f,groundLayer))
        {
            
            return true;
        }
        else
            return false;


        
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
