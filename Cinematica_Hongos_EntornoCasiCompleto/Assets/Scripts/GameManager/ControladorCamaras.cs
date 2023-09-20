using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ControladorCamaras : MonoBehaviour
{
    public static ControladorCamaras instance;

    [SerializeField] private GameObject canvasVida;
    [SerializeField] private Slider sliderVida;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }


    [SerializeField] private Camera camAldea;
    [SerializeField] private Camera camBosque;
    [SerializeField] private Camera camTienda;


    public void ActivarCamaraAldea()
    {
      camAldea.gameObject.SetActive(true);
      camBosque.gameObject.SetActive(false);
      camTienda.gameObject.SetActive(false);
        
        if (GameManager.instance.scenaActual == GameManager.ScenaActual.bosque)
        {
            AudioManager.instance.PlayMusic("MusicaJuego");
        }
        GameManager.instance.scenaActual = GameManager.ScenaActual.aldea;
      
        
    }

    public void ActivarCamaraBosque()
    {
        camBosque.gameObject.SetActive(true);
        camAldea.gameObject.SetActive(false);
        camTienda.gameObject.SetActive(false);
        GameManager.instance.scenaActual = GameManager.ScenaActual.bosque;

        if (TutorialManager.Instance.tutorialBotoniniciar)
        {
            TutorialManager.Instance.TutorialBotonInicio();
        }

        if(GameManager.instance.NumDia % 4 == 0)
        {
            canvasVida = GameObject.Find("BarraDeVidaBoss");
            canvasVida.GetComponent<Canvas>().enabled = true;

            Barrera barrera = GameObject.Find("BARRERABosque")?.GetComponent<Barrera>();
            //RondasEnemigos.instance.EnemigosGenerados = 1;
            //RondasEnemigos.instance.enemigosPorRonda = 1;
            barrera.SubirBarrera();
            GameObject.FindAnyObjectByType<BossCorceps>().ActivarAtaqueAlEntrarAPelear();
            AudioManager.instance.playSfx("SonidoBoss");
        }
        AudioManager.instance.PlayMusic("Pelea");
        
    }

    public void ActivarCamaraTienda()
    {
        camTienda.gameObject.SetActive(true);
        camBosque.gameObject.SetActive(false);
        camAldea.gameObject.SetActive(false);
        GameManager.instance.scenaActual = GameManager.ScenaActual.tienda;

        if (TutorialManager.Instance.tutorialTienda)
        {
            TutorialManager.Instance.DesactivarTutorial();
            TutorialManager.Instance.tutorialTienda = false;
        }
       
    }

}
