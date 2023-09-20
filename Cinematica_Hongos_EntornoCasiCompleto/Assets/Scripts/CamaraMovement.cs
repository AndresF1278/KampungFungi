using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovement : MonoBehaviour
{
   [SerializeField]  private Vector3 target;
    public float distanciaSeparacion = 5f; // Distancia deseada entre la cámara y el personaje
    public float suavidad = 0.5f; // Valor de "T" para la interpolación
    private Vector3 posicionObjetivo;


    private void Update()
    {
        if(GameObject.FindAnyObjectByType<PlayerMovement>() != null)
        {
            target = GameObject.FindAnyObjectByType<PlayerMovement>().transform.position;
            Vector3 pos = new Vector3(target.x, transform.position.y, target.z - 17);
            transform.position = Vector3.Lerp(transform.position, pos, 0.7f);
           
           

        }

    }
}
