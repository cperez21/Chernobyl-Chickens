using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    Scene currentScene;

    float timer = 0;
    GameObject[] objects;

   
    
    // Start is called before the first frame update
    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        
        
    }

   
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        //Exits game by hitting Esc
        if (Input.GetButtonDown("Exit"))
        {
            Application.Quit();
        }



        Debug.Log("current scene is " + currentScene.name);
        //Loads Main Menu
        if (Input.GetButtonDown("MainMenu") && currentScene.name != "MenuScene")
        {
            objects = currentScene.GetRootGameObjects();
            for(int x = 0; x < objects.Length; x++)
            {
                objects[x].transform.position = Vector3.up * 1000;
            }

            SceneManager.LoadSceneAsync("MenuScene",LoadSceneMode.Single);
           
            
        }
       
    }
    
public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }




}
