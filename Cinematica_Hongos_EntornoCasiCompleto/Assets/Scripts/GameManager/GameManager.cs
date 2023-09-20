using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool estaEnMejora  = false;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 PosInicioPlayer;
    public float tiempoDelDia;
    [Header("Economia")]
    [SerializeField] private int monedas;
    [SerializeField] private int monedasIniciales = 5000;
    private int monedasPorGanar = 1;
    private bool puedeDuplicar;
    public int porcentageGanancia;
    public bool TutorialActivo;
    public bool realizoTutorial;
    public int Monedas { get => monedas; set => monedas = value; }
    public bool Dia { get => dia; set => dia = value; }
    public int MonedasPorGanar { get => monedasPorGanar; set => monedasPorGanar = value; }

    
    public int NumDia { get => numDia; set => numDia = value; }
    public bool PuedeDuplicar { get => puedeDuplicar; set => puedeDuplicar = value; }
    public int MonedasGanadasActuales { get => monedasGanadasActuales; set => monedasGanadasActuales = value; }

    private int monedasGanadasActuales;
    public int multiplicadorMonedas;

    public ScenaActual scenaActual;
    [Space]
    [Header("Variables dia y noche")]
    [SerializeField] private bool dia;
    [SerializeField] private int numDia;

   public bool enJuego;


    public delegate void AumentarDia();
    public event AumentarDia eventAumentarDia;

    public delegate void GameOver();
    public event GameOver gameOverEvent;


    public enum ScenaActual
    {
        aldea,
        tienda,
        bosque
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        monedas = monedasIniciales;
        PuedeDuplicar = false;
        MonedasGanadasActuales = monedasPorGanar;
        enJuego = false;
        dia = true;
        numDia = 1;
        monedasPorGanar = 10;
        multiplicadorMonedas = 1;

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && enJuego)
        {
            pausarJuego();
        }
    }





    public void SiguienteDia()
    {
        Invoke("CambioEstadoDia", 1.5f);

        NumDia++;
        UiManager.instance.MostrarTextDia();
        UiManager.instance.TransicionDia();
        if(GameObject.FindAnyObjectByType<ItemController>() != null)
        {
            GameObject.FindAnyObjectByType<ItemController>().eliminarItems();
            GameObject.FindAnyObjectByType<ItemController>().GenerarItemAleatorio();

        }
        player.GetComponent<PlayerController>().TPNuevoDia();
        eventAumentarDia();
        eventAumentarDia -= null;
    }

    public void CambioEstadoDia()
    {
        dia = true;
    }
    public void RestarMonedas(int precioItem)
    {

        Monedas -= precioItem;
        UiManager.instance.ActualizarDinero();
        UiManager.instance.MostrarSumaDinero(true,precioItem);
    }

    public void GanarDinero()
    {
        float n = (MonedasPorGanar * porcentageGanancia) / 100;
        if (n < 1 && n > 0.5f)
        {
            n = 1;
        }


        if (puedeDuplicar)
        {
            MonedasGanadasActuales = (monedasPorGanar + ((int)n)) * multiplicadorMonedas;
            monedas += MonedasGanadasActuales;
        }
        else
        {
            MonedasGanadasActuales = monedasPorGanar + ((int)n);
            monedas += MonedasGanadasActuales;
        }
        UiManager.instance.ActualizarDinero();
        UiManager.instance.MostrarSumaDinero(false, null);
        player.GetComponent<PlayerController>().GenerarCanvasDinero();
    }


    public void AumentarInterezDinero(int aumentoDeDinero)
    {
        monedasPorGanar += aumentoDeDinero;
    }

    public void IniciarJuego()
    {
        

        enJuego = true;
        PosInicioPlayer = GameObject.Find("PosInicioJugador").transform.position;
        player = Instantiate(playerPrefab, PosInicioPlayer, Quaternion.identity);
        scenaActual = ScenaActual.aldea;
        UiManager.instance.DesactivarGameOver();
        UiManager.instance.MostrarTextDia();
        UiManager.instance.ActivarCanvasHud();
        UiManager.instance.DesactivarUIRondas();
        UiManager.instance.ActivarControles();
        UiManager.instance.TransicionMenuAbrir();
        RondasEnemigos.instance.DatosInicioRonda();
        ResetearDatosIniciales();
        PlayerController.instance.Nuevapartidapersonaje();
        UiManager.instance.ActualizarDinero();
        
    }


    public void TerminarJuego()
    {
        ResetearDatosIniciales();
        enJuego = false;
        numDia = 1;
        UiManager.instance.DesactivarTextoInteraccion();
        UiManager.instance.DesactivarGameOver();
        RondasEnemigos.instance.dificultadActual = 0;
        RondasEnemigos.instance.DatosInicioRonda();
        UiManager.instance.canvasHud.enabled = false;
        SceneController.instance.IrMenu();
        Destroy(GameObject.FindAnyObjectByType<PlayerController>().gameObject);
        
       
        UiManager.instance.TransicionMenuCerrar();

    }



    public void ResetearDatosIniciales()
    {
        PlayerController.instance.gameObject.GetComponent<PlayerAttack>().poolBalas.Clear();
        dia = true;
        numDia = 1;
        UiManager.instance.MostrarTextDia();
        monedas = monedasIniciales;
        MonedasPorGanar = 10;
        PuedeDuplicar = false;
        multiplicadorMonedas = 1;
    }
    
    public void pausarJuego()
    {
        if(estaEnMejora){

            return;

        }

        if(UiManager.instance.canvasPausa.gameObject.activeInHierarchy == true || UiManager.instance.canvasAudio.gameObject.activeInHierarchy == true)
        {
            UiManager.instance.canvasPausa.gameObject.SetActive(false);
            UiManager.instance.canvasAudio.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else if(enJuego)
        {
            UiManager.instance.canvasPausa.gameObject.SetActive(true);
            UiManager.instance.canvasAudio.gameObject.SetActive(false);
            Time.timeScale = 0;

        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}