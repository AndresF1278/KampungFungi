
using UnityEngine;
using TMPro;



public class ItemRecolectable : MonoBehaviour
{
    private int id;
    private string nombre;
    private string descripcion;
    private Atributos atributos;
    private int precio;
    [SerializeField] private GameObject ItemMerh;
    public float rotationSpeed = 30f;
    private GameObject player;
    private Canvas CanvasDescripciones;
    private TMP_Text textNombreItem;
    private TMP_Text textDescriptionItem;
    [SerializeField] private Bullet balaPrefab;
    [SerializeField] private float distanciaPlayer;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public Atributos Atributos { get => atributos; set => atributos = value; }
    public int Precio { get => precio; set => precio = value; }
    public int Id { get => id; set => id = value; }

    private void Start()
    {
        CanvasDescripciones = GameObject.Find("CanvasDescripciones").GetComponent<Canvas>();
        textNombreItem = GameObject.Find("NombreItemText").GetComponent<TMP_Text>();
        textDescriptionItem = GameObject.Find("Description").GetComponent<TMP_Text>();
    }

    public void AñadirEstadisticas()
    {
        GameObject.FindAnyObjectByType<ItemController>().excludedItemIds.Add(id);
        HabilidadEspecial();
        GameObject.FindAnyObjectByType<StatsController>().AñadirStatsPlayer(atributos.velocidadDeMovimiento, atributos.dano,
            atributos.rango, atributos.velocidadDeDisparo, atributos.balasPorSegundo, atributos.curacion);
        
        DesactivarCanvasTienda();
    }

    private void Update()
    {

        


        float deltaTime = Time.deltaTime;

        // Calcular el ángulo de rotación en función de la velocidad y el tiempo
        float rotationAmount = rotationSpeed * deltaTime;

        // Rotar el objeto alrededor de su eje vertical (eje Y)
        ItemMerh.transform.Rotate(Vector3.forward, rotationAmount);

    }

    public void ItemDescription()
    {
     
      CanvasDescripciones.enabled = true;
      textNombreItem.text = nombre;
      textDescriptionItem.text = descripcion;

    }

    public void DesactivarCanvasTienda()
    {
        CanvasDescripciones.enabled = false;
    }


    public void HabilidadEspecial()
    {
        if(atributos.habilidadEspecial != "null")
        {
            switch (atributos.habilidadEspecial)
            {
                case "Congelar":
                    Congelar();
                    break;
                case "Envenenar":
                    Envenenar();
                    break;
                case "BalaConSeguimiento":
                    BalaConSeguimiento();
                    break;
            }
        }
        

    }


    public void Envenenar()
    {
        balaPrefab.GetComponent<Bullet>().puedeEnvenenar = true;
        balaPrefab.CambiarColorBalaEnvenenada();
        
    }


    public void Congelar()
    {
        balaPrefab.GetComponent<Bullet>().puedeCongelar = true;
        balaPrefab.CambiarColorBalaCongelar();
    }

    public void BalaConSeguimiento()
    {
        balaPrefab.GetComponent<Bullet>().balaSigue = true;
        balaPrefab.CambiarColorBalaTeledirijida();
      
    }



   }




