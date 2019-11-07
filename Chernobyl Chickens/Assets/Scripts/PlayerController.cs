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
    public bool willHurt;
    public bool haveControls; //Currently used for testing
    public LayerMask groundLayer; //needs to stay set to the ground layer.
    Vector3 respawnPoint, moveVelocity;
    Animator anim;
    private float animTimer = 0.0f;

    //ceasar added for combat cooldown
    private float cooldown = 0;
    private const float interval = 0.5f;
    //used to knock back opponenet
    public float knockBackForce;

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
        knockBackForce = 1;

    }

    // Update is called once per frame
    void Update()
    {
        //combat cooldown. Referenced in OntriggerStay
        cooldown -= Time.deltaTime;

        
        //when health is 0, set playerstate to DEAD
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
            
            //sets to walk animation when moving
            if(moveInput != Vector3.zero)
            {
                rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
                rb.rotation = Quaternion.LookRotation(moveInput);

                anim.SetTrigger("Walk");
                anim.ResetTrigger("Idle");
            }
            //else sets to idle
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
            //if (Input.GetKeyDown(KeyCode.X))
            {
                anim.SetTrigger("Strike");
                state = PlayerState.STRIKE;
            }
        }
    
        //CEASAR MOVED THIS FROM UNDER PlayerState.Strike, it was causing willHurt to not switch repeatedly.
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
        {
            willHurt = true;
        }
        else
        {
            state = PlayerState.DEFAULT;
            willHurt = false;
        }

        //state switch system
        switch (state)
        {
            case PlayerState.DEFAULT:
                
            break;

            case PlayerState.STRIKE:
                
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

    void OnTriggerEnter(Collider other)
    {

        //Moves player back to position they started at if they hit a killbox.
        if (other.gameObject.tag == "KillBox")
        {
            transform.position = respawnPoint;

        }

    }

    //used currently for damaging enemy
    void OnTriggerStay(Collider other)
    {
        //checks if player is punching(willhurt - true), checks if cooldown is in effect, if not, deals damage and resets cooldown
        if (other.gameObject.tag == "Player" && willHurt == true)
        {
            if (cooldown > 0)
                return;
            other.gameObject.GetComponent<PlayerController>().TakeDamage();
            other.gameObject.GetComponent<PlayerController>().KnockBack();
            cooldown = interval;
        }
    }

    //damage function - edit later
    void TakeDamage()
    {
        print("damagesent");
        health -= 10;

    }

    //WIP - will knock back 
    public void KnockBack()
    {
        

    }

}
