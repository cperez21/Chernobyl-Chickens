using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShaderFunctions : MonoBehaviour
{
    public Renderer rend;
    public Color radiationColor;
    public float shadowRecoverRate; //This is just used for damage, recommended 0.05f
    [Header("These values will read out, do not change")]
    public float targetShadowSize; //used as the target value to recover back to.
    public float startShadowSize; // the original, standard cell shading value.
    public float currentRadiationSize;
    private float finalShadowSize = 1f;
    private float currentShadowSize;
    //shader references have funky names. I tried to change them but it broke a lot of stuff, so I made strings so they can be addressed more easily. -Cullen
    private string shadowSize = "Rim Amount";
    private string shadowColor = "Color";

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

        if (currentRadiationSize > startShadowSize && rend.material.GetColor(shadowColor) != Color.red) //if radiated and you are not red from damage
        {
            rend.material.SetColor(shadowColor, radiationColor); //sets it green
            targetShadowSize = currentRadiationSize;
        }
        else if(currentRadiationSize < startShadowSize && rend.material.GetColor(shadowColor) != Color.red) //if your radiation is lower than the standard black shading
        {
            targetShadowSize = startShadowSize; //reset cell shading to normal
            rend.material.SetColor(shadowColor, Color.black); //reset cell shading back to black
        }

        //Damage Flash Recovery begin
        if (rend.material.GetColor(shadowColor) == Color.red && rend.material.GetFloat(shadowSize) >= targetShadowSize) 
        {
            currentShadowSize = rend.material.GetFloat(shadowSize); //Shadow size begins to recover back to target
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
                rend.material.SetColor(shadowSize, Color.black); //non-irradiated you go back to black
            }

                rend.material.SetFloat(shadowSize, targetShadowSize); //sets 

        }
        //Damage Flash Recovery End
    }

    void DamageFlash() //Player flashes red when taking a hit.
    {

        rend.material.SetColor(shadowColor, Color.red); //changes toon shading to red.
        rend.material.SetFloat(shadowSize, finalShadowSize); //sets character to all red (max setting).
        currentShadowSize = rend.material.GetFloat(shadowSize);


    }

    void SetShaderColor(Color color)
    {
        rend.material.SetColor(shadowColor, color);
    }
}
