using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNumber; //The GameManager script sets this value.
    private Rigidbody rb;
    private float dirX;
    public float moveSpeed, jumpForce;
    public int health = 100;
    public bool haveControls; //Currently used for testing
    public bool dead;
    public LayerMask groundLayer; //needs to stay set to the ground layer.
    Vector3 respawnPoint;
    
    public enum PlayerState
    {
        DEFAULT,
        DEAD
    }

    public PlayerState state;
    
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

        if(health <= 0)
        {
            state = PlayerState.DEAD;
        }


        if (haveControls)
        {
            //controls for moving left and right
            dirX = Input.GetAxis("Horizontal") * moveSpeed;
            rb.velocity = new Vector3(dirX, rb.velocity.y, 0);

            //Jump controls
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            //CEASAR ADDED FOR TESTING OF UI
            if (Input.GetKeyDown(KeyCode.Space))
            {
                health -= 10;

            }

        }
        
        switch (state)
        {
            case PlayerState.DEFAULT:
                
            break;

            case PlayerState.DEAD:
                haveControls = false;
                break;
        }


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

   
    
    //tells if player is in the air or not. Used for preventing infinite jumps.
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
