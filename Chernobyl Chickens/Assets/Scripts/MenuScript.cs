using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    Scene currentScene;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Exits game by hitting Esc
        if (Input.GetButtonDown("Exit"))
        {
            Application.Quit();
        }

        //Loads Main Menu
        if (Input.GetButtonDown("MainMenu") && currentScene.name != "MenuScene")
        {
            //SceneManager.UnloadSceneAsync(currentScene);
            SceneManager.LoadScene("MenuScene");
            Debug.Log("Loaded scene");
        }

    }
    
public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }




}
