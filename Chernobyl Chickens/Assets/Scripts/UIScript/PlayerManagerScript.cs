using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerManagerScript : MonoBehaviour
{

    PlayerInputManager inputManager;
    PlayerInput[] pInput;
    InputUser iUser;
    int numOfUsers;
    
    
    // Start is called before the first frame update
    void Start()
    {
      //  inputManager = GetComponent<PlayerInputManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pInput != null)
        {

        }
       // pInput = GameObject.FindObjectsOfType<PlayerInput>();
       
        
            
            
    }
}
