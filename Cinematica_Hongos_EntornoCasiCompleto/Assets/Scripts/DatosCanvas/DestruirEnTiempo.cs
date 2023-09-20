using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirEnTiempo : MonoBehaviour
{
    [SerializeField] private float tiempoParaDestruir;


    private void Update()
    {
        tiempoParaDestruir -= Time.deltaTime;

        if (tiempoParaDestruir <= 0)
        {
            gameObject.SetActive(false);
        }
        
    }


    private void OnDisable()
    {
        tiempoParaDestruir = 2;
    }
}
