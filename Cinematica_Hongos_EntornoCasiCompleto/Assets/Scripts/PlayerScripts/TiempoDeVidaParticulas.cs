using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempoDeVidaParticulas : MonoBehaviour
{
    private float tiempoDeVida = 2;


    private void Update()
    {
         tiempoDeVida -= Time.deltaTime;

        if(tiempoDeVida <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        tiempoDeVida = 2;
    }



}
