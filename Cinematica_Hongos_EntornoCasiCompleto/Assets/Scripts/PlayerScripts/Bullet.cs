using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rbBullet;

    [SerializeField] private float speed;
    [SerializeField] private float radioSeguimiento;
    private float dañoDeBala;
    private float tiempoDeVida = 0;
    [SerializeField] private float tiempoMaximo;
    public  bool puedeEnvenenar = false ;
    public  bool puedeCongelar = false ;
    public  bool balaSigue = false ;
    public  bool balaDentroDeRango = false ;
    [SerializeField] LayerMask capaDeDeteccion;
    public Renderer render;
    public float DañoDeBala { get => dañoDeBala; set => dañoDeBala = value; }
    public float Speed { get => speed; set => speed = value; }
    public float TiempoMaximo { get => tiempoMaximo; set => tiempoMaximo = value; }

    public Color colorActual;

    [HideInInspector] public TrailRenderer trailRenderer;
    private void Start()
    {
        rbBullet = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
        if(GetComponentInChildren<TrailRenderer>() != null)
        {
            trailRenderer = GetComponentInChildren<TrailRenderer>();
        }
        
      
        

    }

    private void Update()
    {
        tiempoDeVida += Time.deltaTime;
        if(tiempoDeVida>= TiempoMaximo)
        {
            gameObject.SetActive(false);
            
        }
    }
    void DesactivarBala()
    {
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {

        direccionBala();
    }

    private void OnEnable()
    {
        if(trailRenderer != null)
        {
            trailRenderer.Clear();          // Borra cualquier segmento de trail existente.
            trailRenderer.emitting = true;

        }
        

        GameObject.FindAnyObjectByType<StatsController>().ActualizarStatsBalas(this);
        
    }
    private void OnDisable()
    {
        tiempoDeVida = 0;

        if(trailRenderer != null)
        {
            trailRenderer.emitting = false;
           
            
        }

        PlayerController.instance.GenerarParticulaMuereBala(transform.position);
        GameObject.FindAnyObjectByType<StatsController>().ActualizarStatsBalas(this);
        AudioManager.instance.playSfx("BalaSeDestruye");

    }

    public void direccionBala()
    {
        GameObject objetoMasCercano = DetectarObjetoMasCercano();

    

        if (balaSigue && objetoMasCercano != null)
        {
            Vector3 direccion = (objetoMasCercano.transform.position - transform.position).normalized;
            rbBullet.velocity = direccion * Speed;
            
        }
        else
        {
            rbBullet.velocity = transform.forward * Speed; // Si no hay objetivo cercano, sigue en línea recta.
        }
    }

   


    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        PlayerController.instance.GenerarParticulaMuereBala(transform.position);
        PlayerController.instance.GenerarParticulaChoque(transform.position);
        AudioManager.instance.playSfx("BalaGolpea");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<HealthManager>().RecibirDaño(DañoDeBala);
            PlayerController.instance.GenerarParticulaChoque(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
            
            if (puedeEnvenenar)
            {
                Envenenar(collision.gameObject.GetComponent<HealthManager>());
            }

            if (puedeCongelar && collision.gameObject.GetComponent<BossCorceps>()== null)
            {

                Congelar(collision.gameObject.GetComponent<EnemyMovement>());
            }
            gameObject.SetActive(false);
        }
    }

    public void Envenenar(HealthManager vidaEnemigo)
    {
        vidaEnemigo.estaEnvenenado = true;
    }


    public void Congelar(EnemyMovement enemyMovement)
    {
        float numRandom = Random.Range(0, 10);

        if(numRandom <= 3)
        {
            enemyMovement.Congelado();
        }

        
    }

    public void CambiarColorBalaCongelar()
    {
        Color hielo = new Color(0f / 255f, 0f / 118f, 255f / 255f, 1f); // Color hielo en valores [0, 1]
        
            colorActual = hielo;
        
        
       


    }

    public void CambiarColorBalaEnvenenada()
    {
        Color veneno = new Color(56f / 255f, 20f / 255f, 65f / 255f, 1f); // Color veneno en valores [0, 1]

        
            colorActual = veneno;
  
        // Asigna directamente el color al material compartido
         

        
    }

    public void CambiarColorBalaTeledirijida()
    {
        Color psiquico = new Color(195f / 255f, 0f / 255f, 247f / 255f, 1f); // Color psiquico en valores [0, 1]

        
            colorActual = psiquico;
       
         
        // Asigna directamente el color al material compartido
        

       
    }

    public GameObject DetectarObjetoMasCercano()
    {
        GameObject objetoMasCercano = null;
        float distanciaMasCorta = Mathf.Infinity;

        // Realizar la detección de objetos en la esfera
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioSeguimiento, capaDeDeteccion);

        // Iterar a través de los resultados y encontrar el objeto más cercano
        foreach (Collider collider in colliders)
        {
            float distancia = Vector3.Distance(transform.position, collider.transform.position);
            if (distancia < distanciaMasCorta)
            {
                distanciaMasCorta = distancia;
                objetoMasCercano = collider.gameObject;
            }
        }

        return objetoMasCercano;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radioSeguimiento);
        
    }


}
