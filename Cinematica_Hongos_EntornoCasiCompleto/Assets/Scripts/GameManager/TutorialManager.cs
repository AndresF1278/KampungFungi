using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    public bool estaTutorialMovimiento;
    public bool tutorialBotoniniciar;
    public bool tutorialBotonPausar;
    public bool tutorialMejoraCasa;
    public bool tutorialTienda;


    [Header("Paneles")]
    public GameObject panelTutoMovimiento;
    public GameObject panelTutoBoton;

    [Space]
    [Header("Textos")]
    public GameObject textMovimiento;
    public GameObject textReloj;
    public GameObject textMejorarCasa;
    public GameObject textIrTienda;
    public GameObject textBotonActivar;
    public GameObject textBotonPausar;
    public GameObject textRondas;

    [Space]
    [Header("Flechas")]
    public GameObject flechaTienda;
    public GameObject flechaCasa;
    public GameObject flechaBosque;
    public GameObject flechaReloj;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        if (GameManager.instance.TutorialActivo)
        {
            TutorialMovimiento();
        }
        
    }

    public void TutorialMovimiento()
    {
        DesactivarTutorial();
        panelTutoMovimiento.SetActive(true);
        textMovimiento.SetActive(true);
        estaTutorialMovimiento = true;
    }

    public void TutorialReloj()
    {
        DesactivarTutorial();
        panelTutoMovimiento.SetActive(true);
        textReloj.SetActive(true);
        flechaReloj.SetActive(true);
        flechaBosque.SetActive(true);
        tutorialBotoniniciar = true;
    }

    public void TutorialBotonInicio()
    {
        DesactivarTutorial();
        panelTutoBoton.SetActive(true);
        textBotonActivar.SetActive(true);
        tutorialBotonPausar = true;
        tutorialBotoniniciar = false;
    }


    public void TutorialBotonPausar()
    {
        DesactivarTutorial();
        panelTutoBoton.SetActive(true);
        textBotonPausar.SetActive(true);
        tutorialBotonPausar = false;
        StartCoroutine(tiempoDesactivarBotonPausar());
    }

    public void RondasTutorial()
    {
        DesactivarTutorial();
        panelTutoBoton.SetActive(true);
        textRondas.SetActive(true);
        tutorialMejoraCasa = true;
    }

    public void TutorialMejorarCasas()
    {
        DesactivarTutorial();
        panelTutoMovimiento.SetActive(true);
        textMejorarCasa.SetActive(true);
        flechaCasa.SetActive(true);
        
      //  tutorialMejoraCasa = false;
    }

    public void TutorialIrTienda() {

        DesactivarTutorial();
        panelTutoMovimiento.SetActive(true);
        textIrTienda.SetActive (true);
        flechaTienda.SetActive(true);

    }

    public void DesactivarTutorial()
    {
        panelTutoMovimiento.SetActive(false);
        textMovimiento.SetActive(false);
        textReloj.SetActive(false);
        panelTutoBoton.SetActive(false);
        textBotonActivar.SetActive(false);
        textBotonPausar.SetActive(false);
        textMejorarCasa.SetActive(false);
        textIrTienda.SetActive(false);
        flechaBosque.SetActive(false);
        flechaCasa.SetActive(false);
        flechaTienda.SetActive(false);
        flechaReloj.SetActive(false);
        estaTutorialMovimiento = false;
    }


    IEnumerator tiempoDesactivarBotonPausar()
    {
        yield return new  WaitForSeconds(6);
        DesactivarTutorial();
        RondasTutorial();
    }
}
