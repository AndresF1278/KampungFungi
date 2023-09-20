using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnemies : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo;
    [SerializeField] private float tiempo;
   
    void Update()
    {
        tiempo += Time.deltaTime;
        if(tiempo >= tiempoMaximo)
        {
            gameObject.SetActive(false);
        }

    }

    private void OnDisable()
    {
        tiempo = 0;
    }
}
