using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionObjeto : MonoBehaviour
{
    public float rotationSpeed = 30f;



    private void Update()
    {

 
        float deltaTime = Time.deltaTime;

        // Calcular el ángulo de rotación en función de la velocidad y el tiempo
        float rotationAmount = rotationSpeed * deltaTime;

        // Rotar el objeto alrededor de su eje vertical (eje Y)
        transform.Rotate(Vector3.up, rotationAmount);
    }

}
