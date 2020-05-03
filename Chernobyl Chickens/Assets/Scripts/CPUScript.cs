using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CPUScript : MonoBehaviour //This Script accessess the PlayerController script for all movement, attacks, etc
{
    PlayerController self;
    public Vector3 movementCPU; //this is what 'movement' paramater in the playercontroller function Move() will be set to.
    public GameObject target; //The object (player or radiation pool) the CPU will move at
    [Tooltip("Max number of times the CPU will make attempts to move to the radpool.")]
    public int maxRadAttempts = 1; //the amount of times the cpu will move to the rad pool.
    public int radAttempts;
    private Vector3 targetDirection;
    //public PlayerController[] players;
    private float proximity;
    public List<PlayerController> players;
    private float closestProximity =1000; //set stupid high because players keep targeting themselves.
    private int frameCount;
    private SphereCollider attackCollider;
    private float attackTimeOut;
    

    IEnumerator StandardAttack()
    {
        Debug.Log("attack coroutine started");
        Move(targetDirection * 0.25f);
        self.Attack();
        yield return new WaitForSeconds(0.1f);
        

        
        //if(self.state == PlayerController.PlayerState.DEFAULT) //meaning the attacking state has ended
        //{
            Move(Vector3.zero);
        //}

        yield return new WaitForSeconds(Random.Range(0.75f,1f)); //amount of time before going to default and executing next attack

        state = CPUState.Default;

    }

    //small delay between moving to target so they don't track you immediatly
    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(0.5f);
        state = CPUState.MoveToTarget;
    }

    public enum CPUState
    {
        Default,
        MoveToTarget,
        Attack,
        MoveToRads,
        Radiating,
        Nothing

    }

    public CPUState state;

    // Start is called before the first frame update
    void Awake()
    {
        self = gameObject.GetComponent<PlayerController>();
        
        attackCollider = gameObject.GetComponent<SphereCollider>();
        
        
        

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.name + " Distance to target is " + proximity);

        if (target != null)
        targetDirection = (target.transform.position - gameObject.transform.position) * 0.75f;

        switch (state)
        {
            case CPUState.Default:
                attackCollider.enabled = true;
               // Move(Vector3.zero);
                if (self.health < 50f && radAttempts < maxRadAttempts)
                {
                    radAttempts++;
                    var radPools = GameObject.FindGameObjectsWithTag("Radiation");
                    var radPoolz = radPools.ToList(); //lol
                    target = TargetClosestObject(radPoolz);
                    state = CPUState.MoveToRads;
                }

                TargetnewPlayer();
               
                StartCoroutine("MoveDelay"); //switches to moveToTarget state with minor delay.

                break;

            case CPUState.MoveToTarget:

                Move(targetDirection * 0.5f);

                break;

            case CPUState.Attack:
                attackTimeOut += Time.deltaTime;

                Move(Vector3.zero);
                if (attackCollider.enabled)
                {
                    
                    StartCoroutine("StandardAttack");
                    attackCollider.enabled = false;
                }
                if(proximity >2)
                {
                    state = CPUState.Default;

                }
                if(attackTimeOut > 2f)
                {
                    state = CPUState.Default;
                    attackTimeOut = 0f;
                }
                break;

            case CPUState.MoveToRads:

                Debug.Log("moving to rads");
                proximity = Vector3.Distance(target.transform.position, gameObject.transform.position);
                Move(targetDirection);
                if (proximity < 1f)
                {
                    state = CPUState.Radiating;
                }


                break;

            case CPUState.Radiating:

                Move(Vector3.zero);
                if (self.supersized == true)
                {
                    state = CPUState.Default;
                }

                break;

            case CPUState.Nothing:
                {
                    Move(Vector3.zero);

                    break;

                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("I collided with " + other.gameObject.name);
            state = CPUState.Attack;
            
        }
        


    }

    GameObject TargetClosestObject(List<GameObject> objects) //Finds the closest thing of that type
    {
        float prox = 1000;
        GameObject targ = null;

        for (int x = 0; x < objects.Count; x++)
        {
            prox = Vector3.Distance(objects[x].transform.position, gameObject.transform.position);
            
            

            if (x == 0)
            {
                closestProximity = prox;
                targ = objects[x].gameObject;
            }
            else if (proximity < closestProximity)
            {
                targ = players[x].gameObject;
                closestProximity = prox;
            }


        }

        return targ;

        
        
    }

    void Move(Vector3 movement) //sets playerController movement vector
    {
        movement.x = Mathf.Clamp(movement.x, -1f, 1f);
        movement.z = Mathf.Clamp(movement.z, -1f, 1f);
        movement.y = 0;
        movementCPU = movement;
    }


    void TargetnewPlayer() //Finds new player to attack based on who is closer.
    {

        var player = GameObject.FindObjectsOfType<PlayerController>();
        if (player == null)
        {
            state = CPUState.Nothing; //game end (all characters are def
        }
        else
        {
            players = player.ToList();
        }
        
        for(int x = 0; x < players.Count; x++)
        {
            proximity = Vector3.Distance(players[x].transform.position, gameObject.transform.position);

            if (proximity == 0 || players[x].state == PlayerController.PlayerState.DEAD)
            {
                continue; //proximity is zero because it is the self player
            }
            
          

            
            if (x == 0)
            {
                closestProximity = proximity;
                target = players[x].gameObject;
            }
            else if (proximity < closestProximity)
            {

                closestProximity = proximity;
                target = players[x].gameObject;
            }
            

        }
    }
}
