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
        //DontDestroyOnLoad(this.gameObject);
        
    }

   
    
    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        
        //Exits game by hitting Esc
        if (Input.GetButtonDown("Exit"))
        {
            Application.Quit();
        }



        //Debug.Log("current scene is " + currentScene.name);
        //Loads Main Menu
        if (Input.GetButtonDown("MainMenu") && currentScene.name != "MenuScene")
        {



           //timer += Time.deltaTime;
            
            
            //Not currently being used, was used to move objects from staying in the background after loading new scene.
           /* objects = currentScene.GetRootGameObjects();
            for(int x = 0; x < objects.Length; x++)
            {
                Destroy(objects[x]);
            }
            */
            
            SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync(currentScene);






        }
       
    }
    
public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync(currentScene);
    }


public void QuitGame()
    {
        Application.Quit();
    }

}


