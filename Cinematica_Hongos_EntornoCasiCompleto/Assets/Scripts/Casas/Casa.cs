using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

using TMPro;
using UnityEngine.UI;
using TMPro;
using System;
using System.Runtime.CompilerServices;

public class Casa : MonoBehaviour
{
    public int mejoraActual;
    public bool CasaComprada;
    public List<GameObject> casas;
    public List<int> preciosCasas;
    [SerializeField] GameObject partuculaContruir;
    [SerializeField] GameObject canvasCasa;
    [SerializeField] Button botonAccionMejora;
    //[SerializeField] Button[] botonesSeleccionMejora;
    [SerializeField] Button botonSalir;

     [SerializeField] string[] titulosMejoras;
    [TextArea(2, 10)] [SerializeField] string[] descripcionMejoras;
    [SerializeField] TMP_Text titulo;
    [SerializeField] TMP_Text descripcion;
    [SerializeField] TMP_Text precio;
    [SerializeField] GameObject tronco;
    [SerializeField] Color ColorDesactivado;
    private bool estaMejorando;
    [SerializeField] private TMP_Text textMonedas;
    private int precioActual;
    private void Start()
    {
        mejoraActual = -1;

        precioActual = 0;
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && estaMejorando)
        {
            DesactivarCanvas();
        }
    }

    /// <summary>
    /// Este metodo se encarga de crear la casa, y si ya esta creada ejecuta la mejora
    /// </summary>
    public void MejorarCasa()
        {
            if(GameManager.instance.Monedas >= preciosCasas[precioActual])
            {
                if (mejoraActual < casas.Count-1)
                {
                    if(!CasaComprada)
                    {
                        mejoraActual++;
                        casas[mejoraActual].SetActive(true);
                        tronco.SetActive(false);
                        CasaComprada = true;
                        
                }
                    else
                    {
                        
                    casas[mejoraActual].SetActive(false);
                    mejoraActual++;
                        
                        casas[mejoraActual].SetActive(true);
                    }
                    
                    DesactivarCanvas();
                    GameManager.instance.RestarMonedas(preciosCasas[mejoraActual]);
                     
                      AudioManager.instance.playSfx("ComprarCasa");
                //GameManager.instance.MonedasPorGanar += 30;

                partuculaContruir.GetComponent<VisualEffect>().enabled = true;
                    partuculaContruir.GetComponent<VisualEffect>().Play();

                    if (GameManager.instance.Monedas < 0)
                    {
                        GameManager.instance.Monedas = 0;
                        UiManager.instance.ActualizarDinero();
                    }

                switch (mejoraActual)
                {

                    case 0:
                        MejoraCompra();
                        break;
                    case 1:
                        Mejora1();
                        break;
                    case 2:
                        Mejora2();
                        break;
                }

                }

            precioActual++;

        } else
        {
            UiManager.instance.MostrarDineroInsuficiente();
        }
        
        }


        public void ActivarCanvas() {

        AudioManager.instance.playSfx("BotonInterfaz");

        if (TutorialManager.Instance.tutorialMejoraCasa)
        {
            TutorialManager.Instance.DesactivarTutorial();
            TutorialManager.Instance.tutorialMejoraCasa = false;
            TutorialManager.Instance.tutorialTienda = true;
        }

            if(mejoraActual == 2)
            {
            // activar texto ya tienes la mejora maxima
            UiManager.instance.MostrarMejoraMaxima();
            return;
            }


             estaMejorando = true;
            GameManager.instance.estaEnMejora = estaMejorando;
             PlayerMovement player = GameObject.FindAnyObjectByType<PlayerMovement>();
             player.estaEnDialogo = true;
             player.GetComponent<Rigidbody>().velocity = Vector3.zero;

            botonSalir.onClick.AddListener(DesactivarCanvas);

            botonAccionMejora.onClick.RemoveAllListeners();
            botonAccionMejora.onClick.AddListener(MejorarCasa);

           
            textMonedas.text = GameManager.instance.Monedas.ToString();
            UiManager.instance.DesactivarCanvasHud();
            canvasCasa.SetActive(true);


                if (CasaComprada == true)
                {
                    titulo.text = titulosMejoras[mejoraActual + 1];
                    descripcion.text = descripcionMejoras[mejoraActual + 1];
                    precio.text = preciosCasas[mejoraActual + 1].ToString();
                    Debug.Log("sapo");
                }
                else
                {
                    titulo.text = titulosMejoras[0];
                    descripcion.text = descripcionMejoras[0];
                    precio.text = preciosCasas[0].ToString();
                     Debug.Log("rana");
                }
           
    }

        public void DesactivarCanvas()
        {
        

        if (TutorialManager.Instance.tutorialTienda)
        {
            TutorialManager.Instance.TutorialIrTienda();
            TutorialManager.Instance.tutorialTienda = true;
            //TutorialManager.Instance.tutorialTienda = true;
            //TutorialManager.Instance.tutorialTienda = false;
        }


        canvasCasa.SetActive(false);
            PlayerMovement player = GameObject.FindAnyObjectByType<PlayerMovement>();
            player.estaEnDialogo = false;
             estaMejorando = false;
             UiManager.instance.ActivarCanvasHud();
             GameManager.instance.estaEnMejora = estaMejorando;

    }

    public void MejoraCompra()
    {
        GameManager.instance.MonedasPorGanar += 3;
        Debug.Log("Compra");
    }
    public void Mejora1()
    {
        GameManager.instance.MonedasPorGanar += 1;
        GameManager.instance.porcentageGanancia += 20;
        Debug.Log("Mejora1");
    }
    public void Mejora2()
    {
        GameManager.instance.MonedasPorGanar += 2;
        GameManager.instance.multiplicadorMonedas *= 2;
        GameManager.instance.PuedeDuplicar = true;
        Debug.Log("Mejora2");
    }

}
