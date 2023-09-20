using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{

    public EnemyStats estadisticasEnemigos;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    [Tooltip("Este es un atributo de ejemplo")]
    [SerializeField] private float speed;
    [SerializeField] private Vector3 positionPlayer;
    [SerializeField] private float fuerzaEmpuje;
    private bool puedeEmpujar;
    public bool estaCongelado;
    [SerializeField] float tiempoRecuperacion;
    [SerializeField] private GameObject hielo;

    private void Start()
    {
        navMeshAgent = gameObject?.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
        puedeEmpujar = true;
        
 
    }
    private void Update()
    {
        if (GameObject.FindAnyObjectByType<PlayerMovement>() != null && navMeshAgent.enabled == true) {

            positionPlayer = GameObject.FindAnyObjectByType<PlayerMovement>().transform.position;
            navMeshAgent.destination = positionPlayer;
        }

       
    }


    public void Empuje()
    {
        if (puedeEmpujar)
        {
            navMeshAgent.enabled = false;
            rb.isKinematic = false;
            rb.AddForce(-transform.forward * fuerzaEmpuje, ForceMode.Impulse);
            Invoke("ActivarNavMesh", tiempoRecuperacion);
            puedeEmpujar = false;

        }
     

      
    }


    private void ActivarNavMesh()
    {
        rb.isKinematic = true;
        navMeshAgent.enabled = true;
        puedeEmpujar = true;
    }

    public void Congelado()
    {
        estaCongelado = true;
        navMeshAgent.speed = 0;
        
        hielo.SetActive(true);

        if(GetComponent<EnemyAttack>() != null)
        {
            GetComponent<EnemyAttack>().enabled = false;

        }
    }

    private void OnDisable()
    {
        estaCongelado = false;

        StatsEnemyController statsController = GetComponent<StatsEnemyController>();
        if (statsController != null && statsController.enemyStats != null)
        {
            navMeshAgent.speed = statsController.enemyStats.VelocidadDeMovimiento;
        }
        gameObject.GetComponent<Animator>().enabled = true;
        hielo.SetActive(false);

        if (GetComponent<EnemyAttack>() != null)
        {
            GetComponent<EnemyAttack>().enabled = true;

        }

       

    }
    private void OnEnable()
    {
        estaCongelado = false;
        StatsEnemyController statsController = gameObject?.GetComponent<StatsEnemyController>();

        if (statsController != null  && gameObject.GetComponent<NavMeshAgent>() != null)
        {
            navMeshAgent = gameObject?.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = statsController.enemyStats.VelocidadDeMovimiento;
        }
        else
        {
            // Manejar el caso en el que statsController o enemyStats sean null.
            // Esto podría incluir establecer una velocidad predeterminada.
        }

        gameObject.GetComponent<Animator>().enabled = true;
        hielo.SetActive(false);

        if (GetComponent<EnemyAttack>() != null)
        {
            GetComponent<EnemyAttack>().enabled = true;
        }

        
    }
}

