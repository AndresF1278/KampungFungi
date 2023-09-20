using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaPortal : MonoBehaviour
{
    private ControladorCamaras controladorCamaras;
    [SerializeField] private bool necesitaInteracion;
    public enum SiguienteScena
    {
        aldea,
        tienda,
        bosque
    }
    
    [SerializeField] private GameObject lugarProximo;
    private PlayerMovement player;
    [SerializeField] private SiguienteScena siguienteScena;
    [SerializeField] private bool puedoInteractuar;


    private void Start()
    {
        controladorCamaras = GameObject.FindAnyObjectByType<ControladorCamaras>();
        puedoInteractuar = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !necesitaInteracion)
        {
            other.GetComponent<PlayerMovement>().estaEnDialogo = true;
            TransicionCompleta();
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (necesitaInteracion)
            {
                UiManager.instance.MostrarTextoInteraccion();

                if (Input.GetKey(KeyCode.E) && puedoInteractuar)
                {
                    other.GetComponent<PlayerMovement>().estaEnDialogo = true;
                    TransicionCompleta();
                    UiManager.instance.DesactivarTextoInteraccion();
                    puedoInteractuar = false;
                    StartCoroutine(TiempoParaInteractuar());
                }
            }

        }
    }
    IEnumerator TiempoParaInteractuar()
    {
        yield return new WaitForSeconds(2);
        puedoInteractuar = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UiManager.instance.DesactivarTextoInteraccion();

        }
    }

        IEnumerator DesactivarLaTransicion()
    {
        yield return new WaitForSeconds(2f);
        UiManager.instance.DesactivarTransicion();
    }
    
    public void  TransicionCompleta()
    {

        Transicion();
        Invoke("CambioDeLugar", 1.4f);
        Invoke("CambioDeCamara", 1.4f);

    }

    


    public void CambioDeLugar()
    {

        GameObject.FindAnyObjectByType<PlayerController>().transform.position = lugarProximo.transform.position;
    }    

    public void Transicion()
    {
        UiManager.instance.estaTransicion = true;
        switch (siguienteScena)
        {
            case SiguienteScena.aldea:
                //controladorCamaras.ActivarCamaraAldea();
                UiManager.instance.TransicionDia();
                break;
            case SiguienteScena.bosque:
                UiManager.instance.TransicionBosque();
                break;
            case SiguienteScena.tienda:
                //controladorCamaras.ActivarCamaraTienda();
                UiManager.instance.TransicionTienda();
                break;
           
        }
        StartCoroutine(EsperaAcabaTransicion());
    }

    public void CambioDeCamara()
    {
        switch (siguienteScena)
        {
            case SiguienteScena.aldea:
                controladorCamaras.ActivarCamaraAldea();

                break;
            case SiguienteScena.bosque:
                controladorCamaras.ActivarCamaraBosque();
                break;
            case SiguienteScena.tienda:
                controladorCamaras.ActivarCamaraTienda();
                break;

        }
    }

    IEnumerator EsperaAcabaTransicion()
    {
        yield return new WaitForSeconds(4);
        UiManager.instance.estaTransicion = false;

    }
}
