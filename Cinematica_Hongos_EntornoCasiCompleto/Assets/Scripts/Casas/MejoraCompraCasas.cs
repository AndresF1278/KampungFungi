using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejoraCompraCasas : MonoBehaviour
{
    public Casa casaSeleccionada;
   


    public void ComprarCasa()
    {
        if (casaSeleccionada != null )
        {
            casaSeleccionada.ActivarCanvas();
            // casaSeleccionada.MejorarCasa();
        }

        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            ComprarCasa();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CompraCasa"))
        {
            casaSeleccionada = other.gameObject.GetComponent<Casa>();
            UiManager.instance.MostrarTextoInteraccion();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CompraCasa"))
        {
            casaSeleccionada = null;
            UiManager.instance.DesactivarTextoInteraccion();
        }

    }

}
