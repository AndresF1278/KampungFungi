using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarObjeto : MonoBehaviour
{
    private float tiempoDeVida = 5;
    [SerializeField] float tiempoActual = 0;
   [SerializeField] GameObject ObjetoActivar;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        tiempoActual += Time.deltaTime;

        if(tiempoActual> tiempoDeVida)
        {
            ObjetoActivar.SetActive(true);
        }

    }
}
