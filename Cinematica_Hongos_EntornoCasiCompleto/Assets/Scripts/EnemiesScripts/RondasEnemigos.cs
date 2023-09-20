using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class RondasEnemigos : MonoBehaviour
{
    public static RondasEnemigos instance;

    [SerializeField] public int rondaActual;
    [SerializeField] public int cantidadDeRondas;
    private int enemigosIniciales = 1;
    [SerializeField] public int enemigosPorRonda;
    [SerializeField] private int sumaEnemigosPorRonda;
    [SerializeField] private int enemigosVivos;
    [SerializeField] private int enemigosGenerados;
    [SerializeField] int enemigoAux;
    [SerializeField] int rondasAux;
    [SerializeField] int enemigosDerrotados;
    [SerializeField] bool rondaFinalizada;
    private Boton boton;
    public bool estaPausado;
    [SerializeField] private GameObject Boss;
     public List<float> vidasBoss;
    public int dificultadActual;

    public bool puedeAmanecer;
    public bool RondaFinalizada { get => rondaFinalizada; set => rondaFinalizada = value; }
    public int EnemigosDerrotados { get => enemigosDerrotados; set => enemigosDerrotados = value; }
    public int EnemigosGenerados { get => enemigosGenerados; set => enemigosGenerados = value; }
    public int EnemigosVivos { get => enemigosVivos; set => enemigosVivos = value; }
    Barrera barrera;
    private void Awake()
    {
        if(instance!= null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }


    private void Start()
    {
        puedeAmanecer = false;
        GameManager.instance.eventAumentarDia += RondasNuevoDia;
        estaPausado = false;
    }

    private void Update()
    {
        barrera = GameObject.Find("BARRERABosque")?.GetComponent<Barrera>();
        
        if(GameManager.instance.NumDia % 4 == 0)
        {
            barrera.SubirBarrera();
        }
        
       
        if (enemigosGenerados >= enemigosPorRonda )
        {
            rondaFinalizada = true;

            
            if (enemigosDerrotados >= enemigosPorRonda)
            {
                //EnemySpawnManager.instance.PuedeSpawn = false;
                if (estaPausado)
                {
                    if (GameObject.Find("BARRERABosque").GetComponent<Barrera>() != null)
                    {
                        barrera.BajarBarrera();
                    }

                   

                    Debug.Log("Baja la Barrera");

                    boton = GameObject.FindFirstObjectByType<Boton>();

                    if (boton != null)
                    {
                        boton.botonPresionado = false;
                    }

                    

                    EnemySpawnManager.instance.PuedeSpawn = false;
                    
                }

                puedeAmanecer = true;
                AumentarRonda();
            }

        }

        
    }

    public void AumentarEnemigosPorRonda()
    {
        enemigosPorRonda += sumaEnemigosPorRonda;
        
    }
    
    public void AumentarRonda()
    {
        boton = GameObject.FindAnyObjectByType<Boton>();

        if (rondaActual == cantidadDeRondas)
        {
            puedeAmanecer = true;
            EnemySpawnManager.instance.PuedeSpawn = false;
            
            Barrera barrera = GameObject.FindAnyObjectByType<Barrera>();
            barrera.BajarBarrera();
            GameManager.instance.SiguienteDia();
            boton.botonPresionado = false;


            return;
        }
        puedeAmanecer = true;

        Invoke("RondaFinalizadaa", 1f);
        //rondaFinalizada = false;
        rondaActual++;
        EnemigosDerrotados = 0;
        enemigosGenerados = 0;
        
        AumentarEnemigosPorRonda();

        UiManager.instance.ShowNumEnemigoText(enemigosDerrotados, enemigosPorRonda);
        UiManager.instance.ShowNumRondasText(rondaActual, cantidadDeRondas);
        for (int i = 0; i < GameObject.FindAnyObjectByType<EnemySpawnManager>().enemigosActivos.Count; i++)
        {
            GameObject.FindAnyObjectByType<EnemySpawnManager>().enemigosActivos[i].SetActive(false);
        }
        GameObject.FindAnyObjectByType<EnemySpawnManager>().enemigosActivos.Clear();

    }
    public void RondaFinalizadaa()
    {
        rondaFinalizada = false;
    }

    public void MuerteEnemigos()
    {
        enemigosDerrotados++;
        


        EnemigosVivos = enemigosPorRonda - enemigosDerrotados;
        
        UiManager.instance.ShowNumEnemigoText(enemigosDerrotados, enemigosPorRonda);

        GameManager.instance.GanarDinero();

    }

    public void DatosInicioRonda()
    {
        if (GameManager.instance.NumDia % 4 != 0)
        {
            enemigoAux = enemigosIniciales;
            rondasAux = cantidadDeRondas;
        }

        enemigosDerrotados = 0;
        rondaActual = 1;
        enemigosGenerados = 0;
        enemigosIniciales = 1;
        cantidadDeRondas = 2;
        enemigosPorRonda = enemigosIniciales;

        if (GameManager.instance.NumDia % 4 == 0)
        {
            UiManager.instance.ShowNumRondasTextBoss();
            UiManager.instance.DesactivarNumEnemigos();
        }
        else
        {
            UiManager.instance.ShowNumEnemigoText(enemigosDerrotados, enemigosPorRonda);
            UiManager.instance.ShowNumRondasText(rondaActual, cantidadDeRondas);
        }
       
       
        boton = GameObject.FindAnyObjectByType<Boton>();
        boton.botonPresionado = false;
        rondaFinalizada = false;
    }


    public void RondasNuevoDia()
    {
        if (GameManager.instance.NumDia % 4 != 0)
        {
            enemigoAux = enemigosIniciales;
            rondasAux = cantidadDeRondas;
        }

        if (GameManager.instance.NumDia % 4 == 0)
        {
            boton.botonPresionado = true;
            boton.ColorPresionado();
            GameObject.Find("BARRERABosque").GetComponent<Barrera>().BajarBarrera();
            rondaFinalizada = false;
            enemigosGenerados = 1;
            enemigosDerrotados = 0;
            cantidadDeRondas = 1;
            EnemigosVivos = 1;
            enemigosIniciales = 1;
            enemigosPorRonda = 1;
            rondaActual = 1;
            UiManager.instance.ShowNumRondasTextBoss();
            UiManager.instance.DesactivarNumEnemigos();
            Invoke("SpawnBoss", 4);
            barrera.BajarBarrera();
            return;
        }

        //puedeAmanecer = false;
        boton.botonPresionado = false;
        boton.ColorNormal();
        GameObject.Find("BARRERABosque").GetComponent<Barrera>().BajarBarrera();
        rondaFinalizada = false;
        rondaActual = 1;
        enemigosGenerados = 0;
        enemigosDerrotados = 0;
       // enemigosIniciales = enemigoAux + 1 ;
        enemigosPorRonda = enemigoAux + 1;
        cantidadDeRondas = rondasAux + 1;
        GameObject.FindAnyObjectByType<EnemySpawnManager>().RestarTiempoEntreEnemigo(0.2f);
        cantidadDeRondas++;
        AumentarEnemigosPorRonda();
        if (TutorialManager.Instance.tutorialMejoraCasa)
        {
            TutorialManager.Instance.TutorialMejorarCasas();
        }
        
        UiManager.instance.ShowNumEnemigoText(enemigosDerrotados, enemigosPorRonda);
        UiManager.instance.ShowNumRondasText(rondaActual, cantidadDeRondas);
    }


    public void SpawnBoss()
    {
        Instantiate(Boss, new Vector3(-100, 2.23f, 18), Quaternion.identity);
      
    }
}
