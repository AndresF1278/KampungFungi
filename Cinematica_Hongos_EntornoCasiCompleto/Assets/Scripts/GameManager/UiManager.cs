using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("HUD")]
    [SerializeField] private TMP_Text text_numRondas;
    [SerializeField] private TMP_Text text_numEnemigos;
    [SerializeField] public TMP_Text text_dinero;
    [SerializeField] public TMP_Text text_dineroGanado;
    [SerializeField] public GameObject dineroImagen;
    [SerializeField] public GameObject panelDetrasHoja;
    [SerializeField] private TMP_Text text_dia;
    [SerializeField] private List<TMP_Text> preciosTienda;
    [SerializeField] private GameObject textoInteractuar;
    [SerializeField] private GameObject canvasStats;
    [SerializeField] private GameObject textDineroInsuficiente;
    [SerializeField] private GameObject textMaximaMejora;
    public List<TMP_Text> textosEstadisticas = new List<TMP_Text> (4);
    public List<TMP_Text> textosEstadisticasSuma = new List<TMP_Text> (4);
    public RectTransform panelStats;

    public GameObject manecilla;
  
    public Image[] corazones;
    HealthPlayer healthPlayer;

    [Space]
    [Header("Canvas")]
    public Canvas canvasHud;
    public Canvas canvasPausa;
    public Canvas canvasAudio;
    public Canvas canvasControles;
    public GameObject canvasGameOver;
    public Button ButtonGameOver;
    public GameObject transicionDia;
    public GameObject transicionTienda;
    public GameObject transicionBosque;
    public GameObject fade;

    public bool estaTransicion;


    [Space]
    [Header("CanvasAudio")]
    public Slider musicSlider, sfxSlider;
    public List<TMP_Text> PreciosTienda { get => preciosTienda; set => preciosTienda = value; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        canvasHud.enabled = false;
        canvasPausa.gameObject.SetActive(false);
        canvasControles.gameObject.SetActive(false);
        //ActualizarDinero();    
        ShowNumRondasText(RondasEnemigos.instance.rondaActual, RondasEnemigos.instance.cantidadDeRondas);
        ShowNumEnemigoText(RondasEnemigos.instance.EnemigosDerrotados, RondasEnemigos.instance.enemigosPorRonda);

    }


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return) && canvasControles.gameObject.activeInHierarchy == true) {

            Time.timeScale = 1;
            DesactivarControles();


        }

        if (estaTransicion)
        {
            PlayerMovement player;
            player = GameObject.FindAnyObjectByType<PlayerMovement>();
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }


    public void panelStatsNormal()
    {
        panelStats.sizeDelta = new Vector2(87.0059f, 220.0284f);
    }
    public void panelStatsGrande()
    {
        panelStats.sizeDelta = new Vector2(120.4574f, 220.0284f);
    }

    public void MostrarDineroInsuficiente()
    {
        StopAllCoroutines();
        DesactivarMostrarDineroInsuficiente();
        textDineroInsuficiente.SetActive(true);
        StartCoroutine(TiempoDesactivarDineroInsuficiente());
    }

    public void DesactivarMostrarDineroInsuficiente()
    {  
            textDineroInsuficiente.SetActive(false);
            
    }


    IEnumerator TiempoDesactivarDineroInsuficiente()
    {
        yield return new WaitForSeconds(1.3f);
        DesactivarMostrarDineroInsuficiente();

    }


    public void MostrarMejoraMaxima()
    {
        StopAllCoroutines();
        DesactivarMostrarMejoraMaxima();
        textMaximaMejora.SetActive(true);
        StartCoroutine(TiempoDesactivarMostrarMejoraMaxima());
    }

    public void DesactivarMostrarMejoraMaxima()
    {
        textMaximaMejora.SetActive(false);

    }


    IEnumerator TiempoDesactivarMostrarMejoraMaxima()
    {
        yield return new WaitForSeconds(1.3f);
        DesactivarMostrarMejoraMaxima();

    }



    public void ActivarUIRondas()
    {
        text_numRondas.gameObject.SetActive(true);
        text_numEnemigos.gameObject.SetActive(true);
    }

    public void DesactivarUIRondas()
    {
        text_numRondas.gameObject.SetActive(false);
        text_numEnemigos.gameObject.SetActive(false);
    }

    public void ShowNumRondasText(int ronda, int rondaMaxima)
    {
        text_numRondas.gameObject.SetActive(true);
        text_numRondas.text = $"Ronda {ronda} / {rondaMaxima}";
    }

    public void ShowNumRondasTextBoss()
    {
        text_numRondas.gameObject.SetActive(true);
        text_numRondas.text = $"Ronda de Jefe";
    }
  

    public void ShowNumEnemigoText( int enemigosDerrotados, int enemigosPorRonda)
    {
        text_numEnemigos.gameObject.SetActive(true);
        text_numEnemigos.text = $"Enemigos {enemigosDerrotados} / {enemigosPorRonda}";
    
    
    }

    public void DesactivarNumEnemigos()
    {
        text_numEnemigos.gameObject.SetActive(false);
    }

    public void ShowNumEnemigoJefe()
    {
        text_numEnemigos.gameObject.SetActive(true);
        text_numEnemigos.text = $"Enemigos {0} / {1}";
    }

    public void MostrarTextDia()
    {
        text_dia.text = $"Dia: {GameManager.instance.NumDia}";
    }




    public void MostrarPreciosItems(TMP_Text text, int precios)
    {

        text.text = $"{precios}$";

    }

    public void ActualizarDinero()
    {
        text_dinero.text = GameManager.instance.Monedas.ToString();
    }

    public void MostrarSumaDinero(bool restaDinero, int? dineroRestado = 0)
    {
        StopCoroutine(tiempoDesactivarSuma());
        
        if (restaDinero)
        {
            text_dineroGanado.color = Color.red;
            text_dineroGanado.text = $"- {dineroRestado}";
        }
        else
        {
            text_dineroGanado.color = Color.green;
            text_dineroGanado.text = $"+ {GameManager.instance.MonedasGanadasActuales}";
        }

        text_dineroGanado.gameObject.SetActive(true);
        StartCoroutine(tiempoDesactivarSuma());
    }

    IEnumerator tiempoDesactivarSuma()
    {
        yield return new WaitForSeconds(1);
        text_dineroGanado.gameObject.SetActive(false);
    }

    public void MostrarTextoInteraccion()
    {

        textoInteractuar.SetActive(true);

    }


    public void DesactivarTextoInteraccion()
    {
        textoInteractuar.SetActive(false);
    }


    public void ActivarInterfazPausa()
    {
        canvasPausa.gameObject.SetActive(true);
        canvasAudio.gameObject.SetActive(false);
    }

    public void ActivarConfigAudio()
    {
        AudioManager.instance.playSfx("BotonInterfaz");
        canvasAudio.gameObject.SetActive(true);
        canvasPausa.gameObject.SetActive(false);
    }


    public void ActivarControles()
    {
        AudioManager.instance.playSfx("BotonInterfaz");
        canvasAudio.gameObject.SetActive(false);
        canvasControles.gameObject.SetActive(true);
        // Time.timeScale = 0;
        GameObject.FindAnyObjectByType<PlayerMovement>().estaEnDialogo = true;
        GameManager.instance.estaEnMejora = true;

    }

    public void DesactivarControles()
    {
        canvasControles.gameObject.SetActive(false);
        GameObject.FindAnyObjectByType<PlayerMovement>().estaEnDialogo = false;
        GameManager.instance.estaEnMejora = false;
    }


    public void ActivarCanvasHud()
    {
        canvasHud.enabled = true;
    }

    public void DesactivarCanvasHud()
    {
        canvasHud.enabled = false;
    }
    public void ActivarCanvasControles()
    {
        canvasControles.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ToggleSfx()
    {
        AudioManager.instance.ToggleSFX();
    }

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }


    public void MusicVolume(Slider sliderMusic)
    {
        if (Time.timeScale == 0 || !GameManager.instance.enJuego)
        {
            musicSlider = sliderMusic;
            AudioManager.instance.MusicVolume(musicSlider.value);
        }

            
    }
    public void SFXVolume(Slider sliderSfx)
    {
        if (Time.timeScale == 0 || !GameManager.instance.enJuego)
        {
            AudioManager.instance.playSfx("BotonInterfaz");
            sfxSlider = sliderSfx;
            float volume = sfxSlider.value * 0.1f;

            AudioManager.instance.SFXVolume(volume);

        }

    }


    public void DesactivarCanvasStats()
    {
        canvasStats.SetActive(false);
    }

    public void ActivarCanvasStats()
    {
        canvasStats.SetActive(true);
    }

   
    public void DesactivarHojas()
    {
        text_dinero.gameObject.SetActive(false);
        panelDetrasHoja.gameObject.SetActive(false);
        dineroImagen.gameObject.SetActive(false);
    }

    public void ActivarHojas()
    {
        text_dinero.gameObject.SetActive(true);
        panelDetrasHoja.gameObject.SetActive(true);
        dineroImagen.gameObject.SetActive(true);
    }


    public void TransicionTienda()
    {   
        
        transicionTienda.SetActive(true);
        Invoke("ControladorCamaras.instance.ActivarCamaraTienda", 1.4f);
        StartCoroutine(TiempoDesactivarTransicion());
        StartCoroutine(DesactivarModoDialogo());
    }


    public void TransicionBosque()
    {
        transicionBosque.SetActive(true);
        Invoke("ControladorCamaras.instance.ActivarCamaraBosque", 1.4f);
       
        StartCoroutine(TiempoDesactivarTransicion());
        StartCoroutine(DesactivarModoDialogo());
    }

    public void TransicionDia()
    {
        transicionDia.gameObject.SetActive(true);
        Invoke("ActivarCamaraAldea", 1.4f);
        GameObject.FindAnyObjectByType<PlayerMovement>().estaEnDialogo = true;
        StartCoroutine(TiempoDesactivarTransicion());
        StartCoroutine(DesactivarModoDialogo());
    }

    IEnumerator DesactivarModoDialogo()
    {
        yield return new WaitForSeconds(4F);
        if(GameObject.FindAnyObjectByType<PlayerMovement>() != null)
        {
            GameObject.FindAnyObjectByType<PlayerMovement>().estaEnDialogo = false;
        }
       
    }


    IEnumerator TiempoDesactivarTransicion()
    {
        yield return new WaitForSeconds(4F);
        DesactivarTransicion();
    }

    public void ActivarCamaraAldea()
    {
        ControladorCamaras.instance.ActivarCamaraAldea();
        //Debug.Log("CambioCamara");
    }
    
    public void DesactivarTransicion()
    {
        transicionDia.gameObject.SetActive(false);
        transicionBosque.SetActive(false);
        transicionTienda.SetActive(false);
    }

    public void ActivarGameOver()
    {
        GameObject.FindAnyObjectByType<PlayerAttack>().enabled = false;
        GameObject.FindAnyObjectByType<PlayerMovement>().muerto = true;
        GameObject.FindAnyObjectByType<PlayerMovement>().GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(tiempoGameOver());
    }

    IEnumerator tiempoGameOver()
    {
        yield return new WaitForSeconds(2f);
        canvasGameOver.SetActive(true);
        AudioManager.instance.playSfx("Muerte");
        DesactivarCanvasHud();
        ButtonGameOver.onClick.AddListener(GameManager.instance.TerminarJuego);
    }

    public void DesactivarGameOver()
    {
        canvasGameOver.SetActive(false);
        

    }
    public void TransicionMenuCerrar()
    {
        //fade.GetComponent<Fade>().enabled = true;
        fade.GetComponent<Fade>().FadeIn();

    }


    public void TransicionMenuAbrir()
    {
        //fade.GetComponent<Fade>().enabled = true;
        fade.GetComponent<Fade>().FadeOut();
        Debug.Log("abre");

    }
}