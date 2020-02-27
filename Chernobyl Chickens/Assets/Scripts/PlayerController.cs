using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNumber; //The GameManager script sets this value.
    RootMotion.Dynamics.PuppetMaster puppet;
    LimbDamage[] limbs;
    public ParticleSystem feathers;
    private Rigidbody[] rbPuppet;
    private Rigidbody rb;
    private float timer;
    private float dirX;
    private float dirZ;
    public float stunAmount;

    // [Header("Audio Section")]

    public AudioClip slap1, slap2, slap3, slap4, slap5;
    private AudioSource audioS;
    [Tooltip("this should be bip01 Pelvis under the Non-Puppet master")]
    public GameObject getUpPosition;
    public bool canJump = false;
    Vector3 moveInput;
    Vector3 startOrientation;
    public float moveSpeed, jumpForce;
    public int health = 300; //Health set to be between 0.0 and 1.0 because of puppet master settings. -cullen
    private float healthF = 1.0f;

    private bool recoverEnabled; //called every frame to slowly recover the floppiness and anim/movespeed slow down from being stunned.
    public bool canHurt;
    public bool isPlayer2; //currently used for 2 player only prototype
    public bool haveControls; //Currently used for testing
    public LayerMask groundLayer; //needs to stay set to the ground layer.
    Vector3 respawnPoint, moveVelocity;
    Animator anim;
    private float animTimer = 0.0f;
    public float defaultAnimSpeed, defaultMoveSpeed;
   
    RaycastHit hit; //Used to check for jumps





    //ceasar added for combat cooldown
    private float Attackcooldown = 0; //attack frequency cooldown
    private float gotHitcooldown = 0; //used to prevent too many registered hits per attack
    private const float interval = 0.5f;
    //used to knock back opponenet
    public float strikeForce;
    public int strikeDamage;
    private float attackBonus = 0f;
    //Ceasar added - for radiation
    public float radiationCount = 0;
    public bool supersized;

    //used to declare controls
    private string HorizontalControl;
    private string VerticalControl;
    private string JumpControl;
    private string StrikeControl;

    

    IEnumerator Stunned()
    {
        puppet.state = RootMotion.Dynamics.PuppetMaster.State.Dead;
        

        Debug.Log("Stunned");
        haveControls = false;
        //recoverEnabled = true;
        state = PlayerState.STUNNED;

        yield return new WaitForSeconds(2.5f);
       
        
        // rb.velocity = Vector3.zero;

        
        //moveSpeed *= stunAmount;    temp disabled by cullen
         //anim.speed *= stunAmount;

        //Vector3 y = Vector3.Project(puppet.targetRoot.position - rb.position, puppet.targetRoot.up);
        //puppet.targetRoot.position += y;

        



       
    }

    public enum PlayerState
    {
        DEFAULT,
        ATTACKING,
        HURT,
        STUNNED,
        DEAD
    }
    public enum PlayerCharacter
    {
        CLUNK,
        LEGOLAS,
        COSPLAY,
        TSO
    }


    public PlayerCharacter character;
    public PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        startOrientation = transform.rotation.eulerAngles;
        audioS = GetComponent<AudioSource>();
        
        state = PlayerState.DEFAULT;
        rb = gameObject.GetComponent<Rigidbody>();
        puppet = transform.parent.GetChild(1).GetComponent<RootMotion.Dynamics.PuppetMaster>(); //Good god
        limbs = transform.parent.GetChild(1).GetComponentsInChildren<LimbDamage>();
        //rbPuppet = transform.parent.GetChild(1).GetComponentsInChildren<Rigidbody>(); //rbPuppet[7] is the head.
        anim = GetComponent<Animator>();
        defaultAnimSpeed = anim.speed;
        defaultMoveSpeed = moveSpeed;

        respawnPoint = transform.position;
        strikeForce = 1;


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

        //CEASAR ZONE
        supersized = false;

    }

    // Update is called once per frame
    void Update()
    {
        //combat Attackcooldown. Referenced in OntriggerStay
        Attackcooldown += Time.deltaTime;
        gotHitcooldown += Time.deltaTime;
        timer += Time.deltaTime;

        if(gotHitcooldown > 1f)
        DamageCheck(enabled);
       

        if(Input.GetKeyDown(KeyCode.T))
        {
            puppet.targetRoot.position = getUpPosition.transform.position;
        }

        //when health is 0, set playerstate to DEAD
        if (health <= 0)
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
                transform.rotation = Quaternion.LookRotation(moveInput);

                anim.SetTrigger("Walk");
                anim.ResetTrigger("Idle");
            }
            //else sets to idle
            else
            {

                anim.SetTrigger("Idle");
                anim.ResetTrigger("Walk");
            }


            //Jump controls
            if (Input.GetButtonDown(JumpControl))
            {

                Debug.DrawRay(transform.position, Vector3.down, Color.blue, Mathf.Infinity);
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.down, out hit, 2.5f, groundLayer))
                {
                    Debug.Log("raycast hit " + hit.collider.name);

                    Jump();


                }
                else
                {
                    return;
                }



            }


            //CEASAR ADDED FOR TESTING OF UI
            if (Input.GetKeyDown(KeyCode.H))
            {
                health -= 10;

            }
            if (Input.GetButtonDown(StrikeControl))
            {

                if (character == PlayerCharacter.LEGOLAS)
                {
                    //Used for Legolas's jump kick. Adds force upwards because he kept going b o n k
                    if (Attackcooldown >= 1.0f)
                    {
                        rb.AddForce(Vector3.up * 300); //the perfect amount of force on the first try fuck yes -cullen 8:09am
                        anim.SetTrigger("Strike");
                        Attackcooldown = 0f;
                    }
                    else
                    {
                        return;
                    }
                }
                //Regular strike for other characters
                else
                {
                    anim.SetTrigger("Strike");
                }





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
                Recover(0.05f); //default recovery amount, method only does something if pinfalloff distance is 5>. MUST be a postive number entered here.
                if (puppet.pinDistanceFalloff > 35f)
                {
                    StartCoroutine(Stunned());
                    
                }


                break;

            case PlayerState.STUNNED:
                Recover(0.2f); //currently only affecting ragdoll mechanics
                
                
                if (puppet.pinDistanceFalloff <= 5f)
                {
                    puppet.targetRoot.position = new Vector3(getUpPosition.transform.position.x, puppet.targetRoot.position.y, getUpPosition.transform.position.z);
                    puppet.state = RootMotion.Dynamics.PuppetMaster.State.Alive;
                    haveControls = true;
                    state = PlayerState.DEFAULT;
                    
                }

                break;

            case PlayerState.ATTACKING:





                break;
            case PlayerState.HURT: //This is probably not needed

                Debug.Log("I hit a hurtbox ouch");
                state = PlayerState.DEFAULT;
                break;
            case PlayerState.DEAD:
                haveControls = false;
                puppet.state = RootMotion.Dynamics.PuppetMaster.State.Dead;
                break;
        }

        //Ceasar added - scaling for radiation
        if (radiationCount >= 100 && supersized == false)
        {
            this.transform.localScale += new Vector3(2, 2, 2);
            supersized = true;
        }

    }


    void Jump()
    {
        anim.SetTrigger("Jump");
        rb.AddForce(Vector3.up * jumpForce);
    }

    //tells if player is in the air or not. Used for preventing infinite jumps.
    bool isGrounded()
    {

        Debug.DrawRay(transform.position, Vector3.down, Color.blue, Mathf.Infinity);
        return true;

    }
    bool IsAttacking(int x) //anim event passes 1 when attack starts, passes 0 to end it
    {


        if (x == 1) //beginning of attack animation
        {

            state = PlayerState.ATTACKING;
            canHurt = true;
            //returns true when called so damage can be given. returns false at end of attacking playerstate
            return true;
        }
        else //x == 0, end of attack animation
        {
            state = PlayerState.DEFAULT;
            canHurt = false;
            return false;
        }




    }
    void OnCollisionStay(Collision other)
    {

        if (other.relativeVelocity.magnitude > 0.1f)
        {
            //Debug.Log("I got Hit");
            //feathers.transform.position = other.GetContact(0).point;
            //feathers.Play();

        }
        if (other.gameObject.tag == "Environment") //needs to be removed, Environment physics should not hurt the players?
        {
            canJump = true;
        }
        else
            canJump = false;
       

    }

    void Recover(float x)
    {
        
        //Stun recovery. Snaps the character back into shape over time.
        if (puppet.pinDistanceFalloff >= 5f)
        {
            puppet.pinDistanceFalloff -= x;
        }
        else
        {
            puppet.pinDistanceFalloff = 5f;
           // recoverEnabled = false;
        }

        /*
                if(moveSpeed < defaultMoveSpeed) //movement speed recovery
                {
                    moveSpeed = 1f;
                    // moveSpeed = (moveSpeed * stunAmount) + moveSpeed;
                }

                if(anim.speed < defaultAnimSpeed) //movement speed recovery
                {
                    moveSpeed = 1f;
                    //anim.speed = (anim.speed * stunAmount) + anim.speed;
                }
        */


    }

    //Checks all limbs for any damage
    void DamageCheck(bool enabled)
    {
        if (enabled == true)
        {

            //checks all bip01 body parts for the limbdamage script
            for (int x = 0; x < limbs.Length; x++)
            {
                if (limbs[x].gotHit)
                {
                    AudioClip[] slapArray = { slap1, slap2, slap3, slap4, slap5 };
                    audioS.clip = slapArray[Random.Range(0, 4)];
                    audioS.Play();
                    puppet.pinDistanceFalloff += 1f;
                    Debug.Log(limbs[x].name + " Damage Check found a hit");

                    limbs[x].magnitude += attackBonus;

                    health -= (int)limbs[x].totalNormalizedDamage;
                    if (limbs[x].magnitude > 8.0f)
                    {
                        puppet.pinDistanceFalloff += 3f;
                        feathers.transform.position = limbs[x].transform.position;
                        feathers.Play();
                        //StartCoroutine(Stunned());
                    }
                    limbs[x].gotHit = false;
                    enabled = false;
                }

            }


        }

       

    }

    
   
    

}
