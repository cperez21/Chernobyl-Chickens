﻿using System.Collections;
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
    public Renderer rend;
    public float radLossRate;
    public float radCooldownTime;
    private CPUScript cpu;
    private bool isHuman = false;
    private bool isStunned;
    

   


    // [Header("Audio Section")]

    public AudioClip slapSwing, slap1, slap2, slap3, death;
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
   public Vector3 movement;
    public LayerMask groundLayer; //needs to stay set to the ground layer.
    Vector3 respawnPoint, moveVelocity;
    Animator anim;
    private float animTimer = 0.0f;
    

    RaycastHit hit; //Used to check for jumps





    //ceasar added for combat cooldown
    private float Attackcooldown = 0; //attack frequency cooldown
    private float gotHitcooldown = 0; //used to prevent too many registered hits per attack
    private const float interval = 0.5f;
    //used to knock back opponenet
   
    private float attackBonus = 0f;
    //Ceasar added - for radiation
    public float radiationCount = 0f;
    public bool supersized;
    public float superSizeMultiplier = 0f;

    //used to declare controls
    private string HorizontalControl;
    private string VerticalControl;
    private string JumpControl;
    private string StrikeControl;
    //ceasar added for new controls
    public Vector3 i_movement;
    
    IEnumerator RadCooldown()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("rad cooldown coroutine ended");
        radState = RadiationState.LOSING;
    }

    IEnumerator Stunned()
    {

        haveControls = false;
        puppet.state = RootMotion.Dynamics.PuppetMaster.State.Dead;
        state = PlayerState.STUNNED;

        Debug.Log("Stunned");
       
        //recoverEnabled = true;
        

        yield return new WaitForSeconds(2.5f);
        isStunned = false;

    }
    IEnumerator AirStunned()
    {
        state = PlayerState.STUNNED;
        puppet.state = RootMotion.Dynamics.PuppetMaster.State.Dead;


        Debug.Log("AirStunned");
        haveControls = false;
        //recoverEnabled = true;
        

        yield return new WaitUntil(() => canJump == true);
        isStunned = false;
       
    }

    public enum RadiationState
    {
        DEFAULT,
        GAINING,
        LOSING,
        COOLDOWN
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
        JERRY,
        TSO
    }

    public RadiationState radState;
    public PlayerCharacter character;
    public PlayerState state;

    // Start is called before the first frame update
    void Awake()
    {
        haveControls = true;
        radCooldownTime = 1.0f;
        startOrientation = transform.rotation.eulerAngles;
        audioS = GetComponent<AudioSource>();
        state = PlayerState.DEFAULT;
        rb = gameObject.GetComponent<Rigidbody>();
        puppet = transform.parent.GetChild(1).GetComponent<RootMotion.Dynamics.PuppetMaster>(); //Good god
        limbs = transform.parent.GetChild(1).GetComponentsInChildren<LimbDamage>();
        //rbPuppet = transform.parent.GetChild(1).GetComponentsInChildren<Rigidbody>(); //rbPuppet[7] is the head.
        anim = GetComponent<Animator>();

        cpu = gameObject.GetComponent<CPUScript>();
        if(cpu == null)
        {
             isHuman = true;
        }



        respawnPoint = transform.position;
        

        
        //if (isPlayer2)
        //{
        //    Debug.Log("Player Controls are set to Player2");
        //    HorizontalControl = "Horizontal_P2";
        //    VerticalControl = "Vertical_P2";
        //    JumpControl = "Jump_P2";
        //    StrikeControl = "Strike_P2";
        //}
        //else
        //{
        //    Debug.Log("Player Controls are set to Player1");

        //    HorizontalControl = "Horizontal_P1";
        //    VerticalControl = "Vertical_P1";
        //    JumpControl = "Jump_P1";
        //    StrikeControl = "Strike_P1";

        //}

        //CEASAR ZONE
        supersized = false;

    }

    // Update is called once per frame
    void Update()
    {
        //This must be at start of update to prevent actions after death
        if (state == PlayerState.DEAD) //skips Update logic if player is dead. (prevents further damage from being taken and sounds)
        {
            return;
        }

        if(puppet.state == RootMotion.Dynamics.PuppetMaster.State.Dead)
        {
            state = PlayerState.STUNNED;
        }

        Recover(0.05f); //default recovery amount, method only does something if pinfalloff distance is 5>. MUST be a postive number entered here.
        if (puppet.pinDistanceFalloff > 7f)
        {
            StartCoroutine(Stunned());

        }



        Move();

        
       
        
        //combat Attackcooldown. Referenced in OntriggerStay
        Attackcooldown += Time.deltaTime;
        gotHitcooldown += Time.deltaTime;
        timer += Time.deltaTime;

        if (gotHitcooldown > 1f)
            DamageCheck(enabled);

        //checks for jumping ability
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.down, out hit, 1.50f, groundLayer))
        {
            canJump = true;
            
        }
        else
        {
            canJump = false;
        }

       

        //when health is 0, set playerstate to DEAD
        if (health <= 0)
        {
            state = PlayerState.DEAD;
            
        }



        //if (haveControls)
        //{
        //    //controls for moving left and right
        //    dirX = Input.GetAxisRaw(HorizontalControl) * moveSpeed;
        //    dirZ = Input.GetAxisRaw(VerticalControl) * moveSpeed;
        //    moveInput = new Vector3(dirX, 0, dirZ);
        //    moveVelocity = moveInput.normalized * moveSpeed;

        //    //sets to walk animation when moving
        //    if (moveInput != Vector3.zero)
        //    {

        //        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
        //        transform.rotation = Quaternion.LookRotation(moveInput);

        //        anim.SetTrigger("Walk");
        //        anim.ResetTrigger("Idle");
        //    }
        //    //else sets to idle
        //    else
        //    {

        //        anim.SetTrigger("Idle");
        //        anim.ResetTrigger("Walk");
        //    }


        //    //Jump controls
        //    if (Input.GetButtonDown(JumpControl))
        //    {

        //        Debug.DrawRay(transform.position, Vector3.down, Color.blue, Mathf.Infinity);
        //        if (canJump && moveInput == Vector3.zero)
        //        {
        //            Debug.Log("raycast hit " + hit.collider.name);

        //            Jump();


        //        }
        //        else if(canJump && moveInput != Vector3.zero)
        //        {
        //            rb.AddForce(moveInput * 5);
        //            Jump();
        //        }



        //    }
        //    else if(Input.GetButtonDown(JumpControl) && moveInput != Vector3.zero)
        //    {
        //        Jump();
        //    }
        //    //ATTACK
        //    if (Input.GetButtonDown(StrikeControl))
        //    {

        //        if (character == PlayerCharacter.LEGOLAS)
        //        {
        //            //Used for Legolas's jump kick. Adds force upwards because he kept going b o n k
        //            if (Attackcooldown >= 1.0f)
        //            {
        //                rb.AddForce(Vector3.up * 300); //the perfect amount of force on the first try fuck yes -cullen 8:09am
        //                anim.SetTrigger("Strike");
        //                Attackcooldown = 0f;
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }
        //        //Regular strike for other characters
        //        else
        //        {
        //            anim.SetTrigger("Strike");
        //        }





        //    }
        //}

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
        
        switch (radState)
        {
            case RadiationState.DEFAULT:
                {
                    //Do nothing, does not have any radiation
                    break;
                }

            case RadiationState.GAINING:
                {
                    //gaining radiation from the RadiationScript on the effect plane
                    break;
                }

            case RadiationState.COOLDOWN:
                {
                    //cannot gain rads in this state. Once the cooldown coroutine finishes, state will be switched to losing

                    break;
                }

            case RadiationState.LOSING:
                {
                    //After cooldown, start to lose radiation
                    if(radiationCount > 0)
                    {
                        LoseRads();
                    }
                    else
                    {
                        radiationCount = 0f;
                        radState = RadiationState.DEFAULT;
                    }

                    break;
                }


        }
        
        
        
        
        
        
        //state switch system
        switch (state)
        {
            case PlayerState.DEFAULT:
               

                break;

            case PlayerState.STUNNED:
                Recover(0.2f); //currently only affecting ragdoll mechanics
                haveControls = false;
                puppet.targetRoot.position = new Vector3(getUpPosition.transform.position.x, getUpPosition.transform.position.y, getUpPosition.transform.position.z);

                if (!isStunned && canJump)
                {
                    puppet.state = RootMotion.Dynamics.PuppetMaster.State.Alive;
                    puppet.targetRoot.position = new Vector3(getUpPosition.transform.position.x, getUpPosition.transform.position.y, getUpPosition.transform.position.z);

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
                haveControls = false;

                DamageCheck(false);
                audioS.clip = death;
                audioS.Play();
                break;
        }

        //Ceasar added - scaling for radiation
        if (radiationCount >= 1f && supersized == false) //changed to 1. Makes it easier to work with shader 0.00 - 1.00 settings. -cullen
        {
            SuperSize();
        }

    }
    //Ceasar Commented out
    //void Jump()
    //{
    //    anim.SetTrigger("Jump");
    //    rb.AddForce(Vector3.up * jumpForce);
    //}

    /*NEW CONTROLS---------------------------------------------------------------------------------------------------------------------------------*/
    //private void OnMove(InputValue value)
    //{
    //    i_movement = value.Get<Vector2>();
    //    //Debug.Log("imove = " + i_movement);
    //}
    

    public void Move()
    {

        if (haveControls)
        {



            if (isHuman) //isHuman declared at Start if there is no CPU script found
                movement = new Vector3(i_movement.x, 0, i_movement.y) * moveSpeed * Time.deltaTime;
            else
                movement = cpu.movementCPU;


            Debug.Log("move = " + movement);
            //transform.Translate(movement);


            //controls for moving left and right
            //dirX = i_movement.x;
            //dirZ = i_movement.y;
            moveVelocity = movement.normalized * moveSpeed;
            //sets to walk animation when moving
            if (movement != Vector3.zero)
            {

                rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(movement);
                anim.SetTrigger("Walk");
                anim.ResetTrigger("Idle");
            }
            //else sets to idle
            else
            {
                anim.SetTrigger("Idle");
                anim.ResetTrigger("Walk");
                // anim.ResetTrigger("Jump");
            }

        }
        else
        {
            return;
        }


    }

    public void Attack()
    {

        if(haveControls)
        { 
       // if (state != PlayerState.ATTACKING)
        //{

            Debug.Log("attack ");
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
        else
        {
            return;
        }
    }
    private void JumpPrep()
    {
      
        if(haveControls)
        { 
        //Debug.DrawRay(transform.position, Vector3.down, Color.blue, Mathf.Infinity);
       // if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.down, out hit, 2.0f, groundLayer))
        //{
           // Debug.Log("raycast hit " + hit.collider.name);
        if (canJump)
            Jump();
        else
            return;


        //}
        //else
        //{
          //  return;
        }
        else
        {
            return;
        }

    }
    private void Jump()
    {
        if (haveControls)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce);
        }
        else
        {
            return;
        }
    }


    /*NEW CONTROLS---------------------------------------------------------------------------------------------------------------------------------*/



    //tells if player is in the air or not. Used for preventing infinite jumps.
    bool isGrounded()
    {

        Debug.DrawRay(transform.position, Vector3.down, Color.blue, Mathf.Infinity);
        return true;

    }
    void IsAttacking(int x) //anim event passes 1 when attack starts, passes 0 when it ends
    {


        if (x == 1) //beginning of attack animation
        {
            audioS.clip = slapSwing;
            audioS.Play();
            state = PlayerState.ATTACKING;
            canHurt = true;
            //returns true when called so damage can be given. returns false at end of attacking playerstate
            //return true;
        }
        else //x == 0, end of attack animation
        {
            Debug.Log("attack to default");
            state = PlayerState.DEFAULT;
            canHurt = false;
            //return false;
        }




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
            
        }
        
    }

    //Checks all limbs for any damage
    void DamageCheck(bool enabled)
    {
        if (enabled == true)
        {

            //checks all bip01 body parts for the limbdamage script
            for (int x = 0; x < limbs.Length; x++)
            {
                if (limbs[x].gotHit && state != PlayerState.STUNNED)
                {
                    


                    AudioClip[] slapArray = { slap1, slap2, slap3};
                    audioS.clip = slapArray[Random.Range(0, 2)];
                    audioS.Play();
                    puppet.pinDistanceFalloff += 0.40f;
                    Debug.Log(limbs[x].name + " Damage Check found a hit");
                    gameObject.SendMessage("DamageFlash");
                    

                    limbs[x].magnitude += attackBonus;

                    health -= (int)limbs[x].totalNormalizedDamage;




                    if (!canJump)
                    {
                        StartCoroutine(AirStunned());
                    }

                    if (limbs[x].magnitude > 8.0f)
                    {
                        puppet.pinDistanceFalloff += 1.0f;
                        Vector3 featherSpawn = limbs[x].transform.position;
                        featherSpawn.y = limbs[x].transform.position.y + 2f;
                        featherSpawn.z = limbs[x].transform.position.z + 2f;
                        
                        Instantiate(feathers, featherSpawn, Quaternion.LookRotation(Vector3.zero,Vector3.up));
                       // feathers.transform.position = limbs[x].transform.position;
                        //feathers.Play();
                        //StartCoroutine(Stunned());
                    }
                    limbs[x].gotHit = false;
                    enabled = false;
                }

            }
        }
        
    }

    void SuperSize()
    {
        this.transform.parent.position += Vector3.up * 10;
        this.transform.parent.localScale += new Vector3(1f, 1f, 1f);
        supersized = true;
    }
    
    void LoseRads()
    {
        radiationCount -= radLossRate;
    }
}
