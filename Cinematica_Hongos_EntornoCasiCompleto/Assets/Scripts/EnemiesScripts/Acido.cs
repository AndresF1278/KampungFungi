using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acido : MonoBehaviour
{
    [SerializeField] private float tiempoDeVida;
    float tiempoActual;

    private void Update()
    {

        tiempoActual += Time.deltaTime;
        

        if(tiempoActual > tiempoDeVida) { 
            
            gameObject.SetActive(false);
        
        }
    }


    private void OnDisable()
    {
        tiempoActual = 0;
    }

}
