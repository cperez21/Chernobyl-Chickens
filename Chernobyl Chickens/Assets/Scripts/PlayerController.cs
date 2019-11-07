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
    bool willHurt;
    public bool haveControls; //Currently used for testing
    public LayerMask groundLayer; //needs to stay set to the ground layer.
    Vector3 respawnPoint, moveVelocity;
    Animator anim;
    private float animTimer = 0.0f;
    
    public enum PlayerState
    {
        DEFAULT,
        STRIKE,
        DEAD
    }

    public PlayerState state;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        moveSpeed = 5f;
        jumpForce = 50f;
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
            Vector3 moveInput = new Vector3(dirX, 0, 0);
            moveVelocity = moveInput.normalized * moveSpeed;
            
            
            if(moveInput != Vector3.zero)
            {
                rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
                rb.rotation = Quaternion.LookRotation(moveInput);

                anim.SetTrigger("Walk");
                anim.ResetTrigger("Idle");
            }
            else
            {
                rb.velocity = Vector3.zero;
                anim.SetTrigger("Idle");
                anim.ResetTrigger("Walk");
            }
            
            
            //Jump controls
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            //CEASAR ADDED FOR TESTING OF UI
            if (Input.GetKeyDown(KeyCode.H)) //Changed to different key
            {
                health -= 10;

            }
            if(Input.GetButtonDown("Strike"))
            {
                anim.SetTrigger("Strike");
                state = PlayerState.STRIKE;
            }
        }
        
        switch (state)
        {
            case PlayerState.DEFAULT:
                
            break;

            case PlayerState.STRIKE:
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
                {
                    willHurt = true;
                    
                }
                else
                {
                    state = PlayerState.DEFAULT;
                    willHurt = false;
                }
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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10.0f,groundLayer))
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
        if (coll.gameObject.tag == "Player" && willHurt == true)
        {
            health -= 10;
        }

    }

}
