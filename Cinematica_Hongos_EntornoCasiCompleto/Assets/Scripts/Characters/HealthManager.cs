using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class HealthManager : MonoBehaviour
{
    [SerializeField] private float vidaMaxima;
    [SerializeField] private float vidaActual;
    [SerializeField] private const float da�oDelVeneno = 1;
    [SerializeField] private  float tiempoEntreVeneno = 2;
    [SerializeField] private bool puedeRecibirDa�oVeneno;
    [SerializeField] public bool estaEnvenenado;
    [SerializeField] Color colorEnvenenado;
    [SerializeField] private GameObject canvasVida;
    [SerializeField] private Slider sliderVida;
    [SerializeField] private bool estaMuerto;
    public float VidaMaxima { get => vidaMaxima; set => vidaMaxima = value; }
    public float VidaActual { get => vidaActual; set => vidaActual = value; }

    private void Start()
    {
        VidaActual = VidaMaxima;
        puedeRecibirDa�oVeneno = true;
        estaEnvenenado = false;
        estaMuerto = false;
        if (gameObject.GetComponent<BossCorceps>() != null)
        {
            vidaMaxima = RondasEnemigos.instance.vidasBoss[RondasEnemigos.instance.dificultadActual];
            VidaActual = VidaMaxima;
             RondasEnemigos.instance.dificultadActual++;
            canvasVida = GameObject.Find("BarraDeVidaBoss");
            //canvasVida.GetComponent<Canvas>().enabled = true;
            
            sliderVida = canvasVida.GetComponentInChildren<Slider>();
            sliderVida.maxValue = vidaMaxima;
            sliderVida.value = vidaMaxima;
            sliderVida.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        Da�oVeneno();
    }


    public void RecibirDa�o(float da�o)
    {
        if(VidaActual - da�o <= 0)
        {
            VidaActual = 0;

            if (gameObject.tag == "Enemy" && gameObject.GetComponent<BossCorceps>() == null)
            {
                if (!estaMuerto)
                {
                    RondasEnemigos.instance.MuerteEnemigos();
                    estaMuerto =true;
                    Debug.Log("MuereEnemigo");
                    gameObject.SetActive(false);
                }
               
            }

            if (gameObject.GetComponent<BossCorceps>() != null)
            {
                if (gameObject.GetComponent<BossCorceps>().muerte == false)
                {

                    StopCoroutine(TiempoEntreMuerteBoss());
                    gameObject.GetComponent<BossCorceps>().muerte = true;
                    gameObject.GetComponent<BossCorceps>().animator.SetBool("estaMuerto", true);
                    gameObject.GetComponent<BossCorceps>().animator.SetTrigger("Muerte");
                    sliderVida.value = vidaActual;
                    StartCoroutine(TiempoEntreMuerteBoss());
                }

            }
            AudioManager.instance.playSfx("MuerteEnemigo");

        }
        else
        {
            VidaActual -= da�o;

           if( gameObject.GetComponent<BossCorceps>() == null)
            {
                GetComponent<EnemyMovement>().Empuje();
            }

            if (gameObject.GetComponent<BossCorceps>() != null)
            {
                sliderVida.value = vidaActual;
            }

        }
    }

    public void CurarVidaActual(float curacion)
    {
        if (VidaActual + curacion > VidaMaxima)
        {
            VidaActual = VidaMaxima;

        }
        else
        {
            VidaActual += curacion;
        }
    }

    public void AumentarVidaMaxima(float aumentoDeVida)
    {
        if(aumentoDeVida > 0)
        {
            VidaMaxima += aumentoDeVida;
        }
        
    }
    
   IEnumerator TiempoEntreMuerteBoss()
    {
        yield return new WaitForSeconds(2f);

            RondasEnemigos.instance.MuerteEnemigos();
            gameObject.SetActive(false);
        canvasVida.GetComponent<Canvas>().enabled = false;
        Debug.Log("Muerte boss");
        
        
    }


   
    public void Da�oVeneno()
    {
        if (puedeRecibirDa�oVeneno && estaEnvenenado)
        {

            if (VidaActual - da�oDelVeneno <= 0)
            {
                VidaActual = 0;

                if (gameObject.tag == "Enemy" && gameObject.GetComponent<BossCorceps>() == null)
                {
                    if (!estaMuerto)
                    {
                        RondasEnemigos.instance.MuerteEnemigos();
                        estaMuerto = true;
                        Debug.Log("MuereEnemigo");
                        gameObject.SetActive(false);
                    }
                }
                
                if(gameObject.GetComponent<BossCorceps>() != null )
                {
                    if( gameObject.GetComponent<BossCorceps>().muerte == false)
                    {
                        StopCoroutine(TiempoEntreMuerteBoss());
                        gameObject.GetComponent<BossCorceps>().muerte = true;
                        gameObject.GetComponent<BossCorceps>().animator.SetBool("estaMuerto", true);
                        gameObject.GetComponent<BossCorceps>().animator.SetTrigger("Muerte");
                        sliderVida.value = vidaActual;
                        StartCoroutine(TiempoEntreMuerteBoss());
                    }
                    
                }

              
            }
            else
            {
                VidaActual -= da�oDelVeneno;

                if(GetComponent<EnemyMovement>() != null)
                {
                    if (!GetComponent<EnemyMovement>().estaCongelado && gameObject.GetComponent<BossCorceps>() == null)
                    {
                        GetComponentInChildren<Renderer>().material.color = colorEnvenenado;
                    }
                }
                
                if(gameObject.GetComponent<BossCorceps>() != null)
                {
                    GetComponentInChildren<Renderer>().material.color = colorEnvenenado;
                    sliderVida.value = vidaActual;
                }

                Invoke("MaterialNormal", 1);
                puedeRecibirDa�oVeneno = false;
                StartCoroutine(TiempoEntreVeneno());
            }
            //RecibirDa�o(da�oDelVeneno);
           

        }
    }

    private void MaterialNormal()
    {
        GetComponentInChildren<Renderer>().material.color = Color.white;
    }

    IEnumerator TiempoEntreVeneno()
    {
        yield return new WaitForSeconds(tiempoEntreVeneno);

        puedeRecibirDa�oVeneno = true;
        
    }


    private void OnDisable()
    {
        
        estaEnvenenado = false;
        puedeRecibirDa�oVeneno = true;
        vidaActual = vidaMaxima;
        estaMuerto = false;
        
        if(GetComponent<BossCorceps>()== null)
        {
           GameObject.FindAnyObjectByType<EnemySpawnManager>().enemigosActivos.Remove(gameObject);
        }
    }

    private void OnEnable()
    {
        
        estaEnvenenado = false;
        puedeRecibirDa�oVeneno = true;
        vidaActual = vidaMaxima;
        estaMuerto = false;
    }


}
