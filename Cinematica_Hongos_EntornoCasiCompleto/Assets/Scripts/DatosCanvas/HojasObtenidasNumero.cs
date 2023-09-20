using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HojasObtenidasNumero : MonoBehaviour
{

    [SerializeField] private float velocidad;
    public float hojasGanadas;
    public TMP_Text textoHojas;
    
    void Update()
    {
        //hojasGanadas = GameManager.instance.MonedasGanadasActuales;
        textoHojas.text = $"+ {hojasGanadas}";

        // Calcular la nueva posición en función de la velocidad y el tiempo
        Vector3 newPosition = transform.position + Vector3.up * velocidad * Time.deltaTime;

        // Asignar la nueva posición al transform
        transform.position = newPosition;
    }
}
