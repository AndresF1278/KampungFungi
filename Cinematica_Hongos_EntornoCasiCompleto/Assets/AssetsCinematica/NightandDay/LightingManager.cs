using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPresent dayPreset;
    [SerializeField] private LightingPresent nightPreset;
    [SerializeField] private float cycleDuration = 60f; // Duración del ciclo día-noche en segundos
    [SerializeField] private float tiempoNoche; // Duración del ciclo día-noche en segundos

    [SerializeField] private bool isDay = true; // Indica si es de día o de noche
    [SerializeField]private float timeOfDay = 0f; // Tiempo actual del día en el ciclo
    [SerializeField] Barrera barreraAldea;
    PlayerMovement playerMovement;


    [Space]
    [Header("Noche")]
    public int numeroDeIntervalos;
    public float t = 0.0f;
    [SerializeField] private float intervaloSize;
    [SerializeField] private int intervaloActual ;
    


    [Space]
    [Header("Canvas")]
    [SerializeField] private GameObject manecilla;

    private void Start()
    {
        GameManager.instance.eventAumentarDia += ResetIntervalos;
        //  GameManager.instance.eventAumentarDia += TiempoCero;
        if (GameManager.instance.TutorialActivo)
        {
            cycleDuration = 6;
        }
        else
        {
            cycleDuration = GameManager.instance.tiempoDelDia;

        }
        

        tiempoNoche = cycleDuration * 0.75f;
        manecilla = UiManager.instance.manecilla;
        intervaloActual = 1;
        numeroDeIntervalos = RondasEnemigos.instance.cantidadDeRondas;
        intervaloSize = 1.0f / numeroDeIntervalos;
        t = 0;
    }


    private void TiempoCero()
    {
        timeOfDay = cycleDuration;
    }

    private void Update()
    {
        if(GameObject.FindAnyObjectByType<PlayerMovement>() != null)
        {
            playerMovement = GameObject.FindAnyObjectByType<PlayerMovement>();
        }
        
        MoverManecilla();
        UpdateLighting();
        VolverDia();

       
    }

    private void UpdateLighting()
    {
        if (GameObject.FindAnyObjectByType<PlayerMovement>() != null)
        {
            if (GameManager.instance.Dia && !playerMovement.estaEnDialogo && !TutorialManager.Instance.estaTutorialMovimiento)
            {
                timeOfDay += Time.deltaTime;

                if (timeOfDay >= cycleDuration)
                {
                    timeOfDay = cycleDuration;
                    GameManager.instance.Dia = false; // Cambiar a la fase de noche cuando termine el ciclo de día
                }
            }
        }
        

        // Calcula un valor entre 0 y 1 para representar el progreso del ciclo día-noche
        float blendFactor = Mathf.Clamp01(timeOfDay / cycleDuration);

        Color ambientColor = Color.Lerp(dayPreset.ambientColor, nightPreset.ambientColor, blendFactor);
        Color directionalColor = Color.Lerp(dayPreset.directionalColor, nightPreset.directionalColor, blendFactor);
        Color fogColor = Color.Lerp(dayPreset.fogColor, nightPreset.fogColor, blendFactor);

        if (directionalLight != null)
        {
            directionalLight.color = directionalColor;
        }

        RenderSettings.ambientLight = ambientColor;
        RenderSettings.fogColor = fogColor;

        if(!GameManager.instance.Dia)
        {
           barreraAldea.BajarBarrera();
            
            if(GameManager.instance.NumDia % 4 == 0)
            {
                UiManager.instance.DesactivarNumEnemigos();
                UiManager.instance.ShowNumRondasTextBoss();
            }
            else
            {
                UiManager.instance.ActivarUIRondas();
            }

        }
        else
        {
           barreraAldea.SubirBarrera();
            UiManager.instance.DesactivarUIRondas();
        }
    }

    public void MoverManecilla()
    {
        if(manecilla != null)
        {
            float currentRotation = (timeOfDay / cycleDuration) * 180f;
            Vector3 rotacion = new Vector3(0, 0, currentRotation);

            manecilla.transform.localRotation = Quaternion.Euler(rotacion);
        }
       
    }

    public void VolverDia()
    {
        

        if (!GameManager.instance.Dia)
        {
            if (intervaloActual <= numeroDeIntervalos)
            {
                if (RondasEnemigos.instance.puedeAmanecer)
                {
                   
                    intervaloSize = 1.0f / numeroDeIntervalos; 

                    float targetTimeOfDay = timeOfDay - intervaloSize * cycleDuration ; 

                   
                    DOTween.To(() => timeOfDay, value => timeOfDay = value, targetTimeOfDay, intervaloSize)
                        .SetEase(Ease.InOutSine);

                    
                    intervaloActual++;

                   
                    RondasEnemigos.instance.puedeAmanecer = false;
                    numeroDeIntervalos = RondasEnemigos.instance.cantidadDeRondas;
                }
            }
        }

    }


    private void ResetIntervalos()
    {
        intervaloActual = 1;
    }
}
