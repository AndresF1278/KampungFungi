using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CololesLERP : MonoBehaviour
{
    public Color colorA;
    public Color colorB;
    public int numeroDeIntervalos = 10;

    public float t = 0.0f;
    [SerializeField] private float intervaloSize;
  
    [SerializeField] private int intervaloActual = 0;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = colorA;
        intervaloSize = 1.0f / numeroDeIntervalos;
    }

    private void Update()
    {
        if (t < intervaloSize && intervaloActual <= numeroDeIntervalos)
        {
            t += Time.deltaTime;
            Color interpolatedColor = Color.Lerp(colorA, colorB, t);
            GetComponent<SpriteRenderer>().color = interpolatedColor;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && intervaloActual< numeroDeIntervalos) 
        {
            
            intervaloSize = (1.0f + intervaloActual) / numeroDeIntervalos;
            intervaloActual++;
        }





        
    }
}
