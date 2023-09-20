using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private StatsController playerStatsController;
    private HealthPlayer healthPlayer;
    public Vector2 posicionInicial;
    private Collision ColisionConItem;
    [SerializeField] private GameObject prefabCanvasDinero;
    [SerializeField] List<GameObject> poolCanvasMostrarDinero;
    [SerializeField] GameObject prefabMuerteBala;
    [SerializeField] GameObject prefabChoqueBala;
    [SerializeField] List<GameObject> efectoChoqueBala;
    [SerializeField] List<GameObject> efectoMuereBala;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        InicializarPlayer();
        playerStatsController.StatsInicialesPlayer();
       posicionInicial = GameObject.Find("PosInicioJugador").transform.position;
        //GameManager.instance.eventAumentarDia += TeleportAldea;
        //healthPlayer.hearts = UiManager.instance.corazones;
        //healthPlayer.UpdateHearts();

    }

    public void GenerarParticulaMuereBala(Vector3 zonaDeMuerte)
    {
        if (efectoMuereBala.Find(particulaM => !particulaM.activeSelf) == false)
        {
            GameObject particulaMuerte = Instantiate(prefabMuerteBala, zonaDeMuerte, Quaternion.identity);
            particulaMuerte.SetActive(true);
            efectoMuereBala.Add(particulaMuerte);

        }
        else
        {
            var particulaM = efectoMuereBala.Find(particulaM => !particulaM.activeSelf);
            particulaM.transform.position = zonaDeMuerte;
            particulaM.SetActive(true);
        }
    }

    public void GenerarParticulaChoque(Vector3 zonaDeMuerte)
    {
        if (efectoMuereBala.Find(particulaC => !particulaC.activeSelf) == false)
        {
            GameObject particulaChoque = Instantiate(prefabChoqueBala, zonaDeMuerte, Quaternion.identity);
            particulaChoque.SetActive(true);
            efectoMuereBala.Add(particulaChoque);

        }
        else
        {
            var particulaC = efectoMuereBala.Find(particulaC => !particulaC.activeSelf);
            particulaC.transform.position = zonaDeMuerte;
            particulaC.SetActive(true);
        }
    }







    private void InicializarPlayer()
    {
        playerStatsController = GetComponent<StatsController>();
        healthPlayer = GetComponent<HealthPlayer>();
    }


    public void Nuevapartidapersonaje()
    {
        GetComponent<PlayerMovement>().idScena = "Aldea";
        //healthplayer.hearts = uimanager.instance.corazones;
        //healthplayer.updatehearts();

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            UiManager.instance.MostrarTextoInteraccion();
            other.GetComponent<ItemRecolectable>().ItemDescription();
            if (Input.GetKey(KeyCode.E)) {
                TiendaManager.instance.ComprarItem(other.gameObject.GetComponent<ItemRecolectable>());
                UiManager.instance.DesactivarTextoInteraccion();
            }
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            UiManager.instance.DesactivarTextoInteraccion();
            other.GetComponent<ItemRecolectable>().DesactivarCanvasTienda();
        }
    }

    private void TeleportAldea()
    {
        posicionInicial = GameObject.Find("PosInicioJugador").transform.position;
        transform.position = new Vector3(15, 1.5f, -17);
    }

    public void TPNuevoDia()
    {
        Invoke("TeleportAldea", 1.4f);
        
        Debug.Log("Teleport");
    }

    public void GenerarCanvasDinero()
    {
        Vector3 posicionInicial = new Vector3(transform.position.x, transform.position.y +2, transform.position.z);
        if (poolCanvasMostrarDinero.Find(objeto => !objeto.activeSelf) == false)
        {
            GameObject canvas = Instantiate(prefabCanvasDinero, posicionInicial, prefabCanvasDinero.transform.rotation);
            canvas.GetComponent<HojasObtenidasNumero>().hojasGanadas = GameManager.instance.MonedasGanadasActuales;
            poolCanvasMostrarDinero.Add(canvas);
        }
        else
        {
            UtilizarCanvasPool();
        }
    }


    public void UtilizarCanvasPool()
    {
        Vector3 posicionInicial = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        var canvas = poolCanvasMostrarDinero.Find(objeto => !objeto.activeSelf);
        canvas.GetComponent<HojasObtenidasNumero>().hojasGanadas = GameManager.instance.MonedasGanadasActuales;
        canvas.transform.position = posicionInicial;
        canvas.SetActive(true);
       
    }


}
