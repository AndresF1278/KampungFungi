using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    private PlayerMovement player;
    private bool playerEstaCerca;
    private bool inicioDialogo;
    private int lineaIndex;
    float aux;
   [SerializeField] private float tiempoTipeo = 0.05F ; 

    [SerializeField] private GameObject PanelDialogo;
    [SerializeField] private TMP_Text dialogo;

    [TextArea(4,6)]
    [SerializeField] private string[] lineasDialogo;
    [SerializeField] private GameObject signoDePregunta;
    private void Update()
    {
        if(playerEstaCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (!inicioDialogo)
            {
                ComenzarDialogo();
                

            }
            else if(dialogo.text == lineasDialogo[lineaIndex])
            {
                SiguienteLineaDialogo();
            }
            else
            {
                StopAllCoroutines();
                dialogo.text = lineasDialogo[lineaIndex];
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            UiManager.instance.MostrarTextoInteraccion();
            playerEstaCerca = true;
        }
       

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UiManager.instance.DesactivarTextoInteraccion();
            playerEstaCerca = false;
        }
    }


    private void ComenzarDialogo()
    {
        signoDePregunta.SetActive(false);
        UiManager.instance.DesactivarCanvasStats();
        inicioDialogo = true;
        PanelDialogo.SetActive(true);
        lineaIndex = 0;
        UiManager.instance.DesactivarTextoInteraccion();
        player = GameObject.FindAnyObjectByType<PlayerMovement>();
        player.estaEnDialogo = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartCoroutine(MostrarLinea());
        UiManager.instance.DesactivarHojas();
        
    }

    private void SiguienteLineaDialogo()
    {
        lineaIndex++;
        if(lineaIndex < lineasDialogo.Length)
        {
            StartCoroutine(MostrarLinea());
        }
        else
        {
            UiManager.instance.ActivarCanvasStats();
            inicioDialogo = false;
            PanelDialogo.SetActive(false);
            player = GameObject.FindAnyObjectByType<PlayerMovement>();
            player.estaEnDialogo = false;
            UiManager.instance.ActivarHojas();



        }
    }

    private IEnumerator MostrarLinea()
    {
        dialogo.text = string.Empty;

        foreach (char ch in lineasDialogo[lineaIndex])
        {
            dialogo.text += ch;
            yield return new WaitForSeconds(tiempoTipeo);
        }
    }
}
