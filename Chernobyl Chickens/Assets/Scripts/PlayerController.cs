using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNumber; //The GameManager script sets this value.
    private Rigidbody rb;
    public ParticleSystem particles;
    
    private float timer;
    private float dirX;
    private float dirZ;
    Vector3 moveInput;
    public float moveSpeed, jumpForce;
    public int health = 100;
    //public bool willHurt;
    public bool isPlayer2; //currently used for 2 player only prototype
    public bool haveControls; //Currently used for testing
    public LayerMask groundLayer; //needs to stay set to the ground layer.
    Vector3 respawnPoint, moveVelocity;
    Animator anim;
    private float animTimer = 0.0f;
    

    //ceasar added for combat cooldown
    private float cooldown = 0;
    private const float interval = 0.5f;
    //used to knock back opponenet
    public float strikeForce;
    public int strikeDamage;
    private BoxCollider attackBox;


    //used to declare controls
    private string HorizontalControl;
    private string VerticalControl;
    private string JumpControl;
    private string StrikeControl;
 

    public enum PlayerState
    {
        DEFAULT,
        STRIKE,
        HURT,
        DEAD
    }

     public PlayerState state;
    
    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.DEFAULT;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
       // particles = GetComponent<ParticleSystem>();
        moveSpeed = 5f;
        jumpForce = 0f;
        respawnPoint = transform.position;
        strikeForce = 1;
         //attackBox = transform.GetChild(2).GetComponent<BoxCollider>();

        if (isPlayer2)
        {
            Debug.Log("Player Controls are set to Player2");
            HorizontalControl = "Horizontal_P2";
            VerticalControl = "Vertical_P2";
            JumpControl = "Jump_P2";
            StrikeControl = "Strike_P2";
        }
        else
        {
            Debug.Log("Player Controls are set to Player1");

            HorizontalControl = "Horizontal_P1";
            VerticalControl = "Vertical_P1";
            JumpControl = "Jump_P1";
            StrikeControl = "Strike_P1";

        }

        

    }

    // Update is called once per frame
    void Update()
    {
        //combat cooldown. Referenced in OntriggerStay
        cooldown -= Time.deltaTime;

        timer += Time.deltaTime;

       
      
        
        
        
        //when health is 0, set playerstate to DEAD
        if(health <= 0)
        {
            state = PlayerState.DEAD;
        }


        if (haveControls)
        {
            //controls for moving left and right
            dirX = Input.GetAxisRaw(HorizontalControl) * moveSpeed;
            dirZ = Input.GetAxisRaw(VerticalControl) * moveSpeed;
             moveInput = new Vector3(dirX, 0, dirZ);
            moveVelocity = moveInput.normalized * moveSpeed;

            //sets to walk animation when moving
            if (moveInput != Vector3.zero)
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
            if (Input.GetButtonDown(JumpControl))
            {
                Jump();
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                ReOrient();
            }
            //CEASAR ADDED FOR TESTING OF UI
            if (Input.GetKeyDown(KeyCode.H)) 
            {
                health -= 10;

            }
            if(Input.GetButtonDown(StrikeControl)) 
            //if (Input.GetKeyDown(KeyCode.X))
            {


                anim.SetTrigger("Strike");

                Instantiate(particles);
                
                state = PlayerState.STRIKE;



            }
        }
    
       /* //CEASAR MOVED THIS FROM UNDER PlayerState.Strike, it was causing willHurt to not switch repeatedly.
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
        {
            willHurt = true;
        }
        else
        {
           // state = PlayerState.DEFAULT;
            willHurt = false;
        }
*/
        //state switch system
        switch (state)
        {
            case PlayerState.DEFAULT:

                Debug.Log("help im stuck in default");

                break;

            case PlayerState.STRIKE:
                
                if (attackBox.enabled == true)
                {
                    state = PlayerState.DEFAULT;
                    anim.ResetTrigger("Strike");
                    attackBox.enabled = false;
                   
                }
              
               
                break;
            case PlayerState.HURT:
                
                Debug.Log("I hit a hurtbox ouch");
                state = PlayerState.DEFAULT;
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
    void Strike() //This is an animation event from Clunk's 'Punch' animation.
    {
        attackBox.enabled = true; //This is the 'HurtTransforms' box collider.
        

    }
   
    
    //tells if player is in the air or not. Used for preventing infinite jumps.
    bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f,groundLayer))
        {
            
            return true;
        }
        else
            return false;


        
    }

    void OnCollisionStay(Collision other)
    {
       
        
        //Moves player back to position they started at if they hit a killbox.
        if (other.gameObject.tag == "Player")
        {
            

            

        }
        
    }

    //used currently for damaging enemy
    void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.tag == "HurtBox")
        {
             
            //particles.Play();
        }
        
        
        
        /*  PlayerController enemy = other.gameObject.GetComponent<PlayerController>();
        Collider enemyHurtBox = enemy.transform.GetChild(2).GetComponent<BoxCollider>();
        
        if (other.gameObject.tag == "HurtBox")
        {
            Debug.Log("hurtbox hit me");
            Vector3 dir = enemyHurtBox.transform.position - transform.position;
            dir = -dir.normalized;
            rb.AddForce(dir * enemy.strikeForce);


            TakeDamage(enemy.strikeDamage);
            state = PlayerState.HURT;
        
        }
        */




        /* //checks if player is punching(willhurt - true), checks if cooldown is in effect, if not, deals damage and resets cooldown
        if (other.gameObject.tag == "Player" && willHurt == true)
        {
            if (cooldown > 0)
                return;
            other.gameObject.GetComponent<PlayerController>().TakeDamage();
            other.gameObject.GetComponent<PlayerController>().KnockBack();
            cooldown = interval;
        }
    */
    }
    


    //damage function - edit later
    void TakeDamage(int damage)
    {
        print("damagesent");
        health -= damage;

    }

    void ReOrient()//Resets player rotation if capsized
    {
        //transform.rotation = Quaternion.LookRotation(Vector3.zero);
       
    }

    //WIP - will knock back 
    public void KnockBack()
    {
        

    }

}
