using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;

    [Header("parametros Spawn")]
    [SerializeField] private float tiempoEntreEnemigo;
    [SerializeField] bool puedeSpawn;
    [SerializeField] float tiempoParaSalirEnemigo;
    
    [Space]
    [Header("Parametros Pos Random")]
    [SerializeField] float posXMin;
    [SerializeField] float posXMax;
    [SerializeField] float posZMin;
    [SerializeField] float posZMax;

    [Space]
    [Header("VFX Spawn")]
    [SerializeField] private int cantidadPoolInicial;
    [SerializeField] private GameObject portalEnemigo;
    [SerializeField] private List<GameObject> poolPortales;

    public bool presionoBoton;

    public bool PuedeSpawn { get => puedeSpawn; set => puedeSpawn = value; }
    public float TiempoEntreEnemigo { get => tiempoEntreEnemigo; set => tiempoEntreEnemigo = value; }

    private GameObject enemigoDescartado;
    private GameObject portalDescartado;
    [SerializeField]  public List<GameObject> enemigosActivos = new List<GameObject>();
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        PoolPortalEnemigos();
        
    }

    private void Update()
    {    
       SpawnEnemigoRandom();
    }



    void SpawnEnemigoRandom(){
        if (PuedeSpawn && !RondasEnemigos.instance.RondaFinalizada && GameManager.instance.NumDia % 4 != 0)
        {
            // Calcular la suma total de las probabilidades
            float totalProbability = 0f;
            foreach (float probability in ObjectPoolerManager.instance.probabilidades)
            {
                totalProbability += probability;
            }

            // Generar un número aleatorio entre 0 y la suma total de probabilidades
            float randomValue = Random.Range(0f, totalProbability);

            // Encontrar el índice del objeto correspondiente al número aleatorio
            int spawnIndex = -1;
            float cumulativeProbability = 0f;
            for (int i = 0; i < ObjectPoolerManager.instance.probabilidades.Count; i++)
            {
                cumulativeProbability += ObjectPoolerManager.instance.probabilidades[i];
                if (randomValue <= cumulativeProbability)
                {
                    spawnIndex = i;
                    break;
                }
            }

            // Instanciar el objeto correspondiente
            if (spawnIndex != -1)
            {
                GameObject enemigoSeleccionado = ObjectPoolerManager.instance.
                   listasEnemigosCreados[spawnIndex].enemigosCreados.Find(objeto => !objeto.activeSelf && !enemigosActivos.Contains(objeto));

                if (enemigoSeleccionado != null)
                {
                    RondasEnemigos.instance.EnemigosGenerados++;
                    PosisionSpawnAleatoria(enemigoSeleccionado);
                    
                    enemigosActivos.Add(enemigoSeleccionado); // Agregar el enemigo a la lista de activos
                    // Puedes hacer algo con el objeto instanciado aquí si lo deseas
                }
                else
                {
                    Debug.Log("No se pudo encontrar un objeto para instanciar o ya está activo.");
                    enemigosActivos.Clear();
                }
            }
            else
            {
                Debug.LogError("No se pudo encontrar un objeto para instanciar.");
               
            }

            //StartCoroutine(TiempoEntreCadaSpawn());
          //  PuedeSpawn = false;
        }
      
    }
    void PosisionSpawnAleatoria(GameObject enemigoSeleccionado)
    {
        float Posx = Random.Range(posXMin, posXMax);
        float Posz = Random.Range(posZMin, posZMax);

        Vector3 PosRandom = new Vector3(Posx, 2.49f, Posz);
        enemigoSeleccionado.transform.position = PosRandom;

        //Crear Portal en SCENA
        

        StartCoroutine(TiempoParaSalir(enemigoSeleccionado));
        
       
    }


    IEnumerator TiempoEntreCadaSpawn()
    {
        yield return new WaitForSeconds(TiempoEntreEnemigo);
        
            PuedeSpawn = true;
       
    }
    IEnumerator TiempoParaSalir(GameObject enemigo)
    {
        SpawnPortal(enemigo.transform.position);
        yield return new WaitForSeconds(tiempoParaSalirEnemigo);
        enemigo.SetActive(true);
        

        AudioManager.instance.playSfx("SalidaEnemigos");
        
        

    }
    private void PoolPortalEnemigos()
    {
        for (int i = 0; i < cantidadPoolInicial; i++)
        {
            GameObject portalCreado = Instantiate(portalEnemigo, Vector3.zero, Quaternion.identity);
            portalCreado.SetActive(false);
            poolPortales.Add(portalCreado);
        }
    }

    private void SpawnPortal(Vector3 pos)
    {
        GameObject portalInScene = poolPortales.Find(portal => !portal.activeSelf && portal != portalDescartado);
        portalInScene.transform.position = pos;
        portalInScene.SetActive(true);
        
        portalDescartado = portalInScene;
    }

    public void RestarTiempoEntreEnemigo(float tiempoRestado)
    {
       if( tiempoEntreEnemigo - tiempoRestado < 0 )
        {
            tiempoEntreEnemigo = 0;
        }
        else
        {
            tiempoEntreEnemigo -= tiempoRestado;
        }
        //tiempoEntreEnemigo = Mathf.Round(tiempoEntreEnemigo * 10) / 10;
    }

    
}



