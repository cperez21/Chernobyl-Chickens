using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShaderFunctions : MonoBehaviour
{
    public Renderer rend;
    public Color radiationColor;
    private Color targetColor;
    public float shadowRecoverRate; //This is just used for damage, recommended 0.05f
    [Header("These values will read out, do not change")]
    public float targetShadowSize; //used as the target value to recover back to.
    public float startShadowSize; // the original, standard cell shading value.
    public float currentRadiationSize;
    private float finalShadowSize = 1f;
    private float currentShadowSize;
    //shader references have funky names. I tried to change them but it broke a lot of stuff, so I made strings so they can be addressed more easily. -Cullen
    private string shadowSize = "Vector1_9AB3F732";
    private string shadowColor = "Color_C2BC5537";

    // Start is called before the first frame update
    void Start()
    {
        
        targetShadowSize = rend.material.GetFloat(shadowSize);
        startShadowSize = rend.material.GetFloat(shadowSize);
        

    }

    // Update is called once per frame
    void Update()
    { //this is a hot mess -cullen
       
        currentRadiationSize = gameObject.GetComponent<PlayerController>().radiationCount;
        currentShadowSize = rend.material.GetFloat(shadowSize);

        //Damage Flash Recovery begin
        if (rend.material.GetColor(shadowColor) == Color.red) //this is a gross about of ifs and elses
        {
            if (currentShadowSize > targetShadowSize)
            {
                //Shadow size begins to recover back to target
                rend.material.SetFloat(shadowSize, currentShadowSize - shadowRecoverRate);
            }
            else
            {
                if (currentRadiationSize > startShadowSize) //irradiated you go back to green
                {
                    rend.material.SetColor(shadowColor, Color.green);
                }
                else
                {
                    rend.material.SetColor(shadowColor, Color.black); //non-irradiated you go back to black
                }

                rend.material.SetFloat(shadowSize, targetShadowSize);
            }
        }




        if (currentRadiationSize > startShadowSize && rend.material.GetColor(shadowColor) != Color.red) //if radiated and you are not red from damage
        {
            rend.material.SetColor(shadowColor, radiationColor); //sets it green
            targetShadowSize = currentRadiationSize;
        }
        if (currentRadiationSize < startShadowSize && rend.material.GetColor(shadowColor) != Color.red) //if your radiation is lower than the standard black shading
        {
            targetShadowSize = startShadowSize; //reset cell shading to normal
                                                //rend.material.SetColor(shadowColor, Color.black); //reset cell shading back to black
        }

        


        /*
        if (currentRadiationSize > startShadowSize) //establishes what color cell shading resets to after damage
        {
            targetColor = radiationColor; //if irradiated
            rend.material.SetColor(shadowColor, targetColor);
            targetShadowSize = currentRadiationSize;
        }
        else
        {
            targetColor = Color.black; //if radiation is lower than normal cell shading
            targetShadowSize = startShadowSize;
        }


      if(currentShadowSize > startShadowSize)//Starts resetting back to normal
        {
            rend.material.SetFloat(shadowSize, currentShadowSize - shadowRecoverRate); //recovering back to target
        }
        else // reached target
        {
            rend.material.SetFloat(shadowSize, targetShadowSize);
            rend.material.SetColor(shadowColor, targetColor);
        }
        */
















        //Damage Flash Recovery End
    }

    void DamageFlash() //Player flashes red when taking a hit.
    {

        rend.material.SetColor(shadowColor, Color.red); //changes toon shading to red.
        rend.material.SetFloat(shadowSize, finalShadowSize); //sets character to all red (max setting).
        


    }

    void SetShaderColor(Color color)
    {
        rend.material.SetColor(shadowColor, color);
    }
}
