using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float distanciaDelPlayer;
    [SerializeField] private Vector3 direccionPlayer;
    [SerializeField] private float distanciaAtaque;
    [SerializeField] private bool puedeAtacar;
    [SerializeField] private GameObject balasEnemy;
    [SerializeField] private List<GameObject> poolBalas;
    [SerializeField] private GameObject puntoDeDisparo;
    [SerializeField] private float speedBullet;
    private NavMeshAgent navmesh;

    private void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        puedeAtacar = true;
    }


    private void Update()
    {
      
        if(GameObject.FindAnyObjectByType<PlayerMovement>() != null)
        {
            player = GameObject.FindAnyObjectByType<PlayerMovement>().transform;
        }
        AtaqueEnemigo();

    }


    public virtual void AtaqueEnemigo()
    {
        if(player != null)
        {
            distanciaDelPlayer = Vector3.Distance(gameObject.transform.position, player.position);
        }
      

        if (distanciaDelPlayer < distanciaAtaque && puedeAtacar)
        {
           
            ShotEnemy();
            puedeAtacar = false;
            StartCoroutine(tiempoEntreBala());
        }

    }

    IEnumerator tiempoEntreBala()
    {
        yield return new WaitForSeconds(2);
        puedeAtacar = true;
    }


    public void ShotEnemy()
    {
        direccionPlayer = (player.position - transform.position).normalized;
        if (poolBalas.Find(objeto => !objeto.activeSelf) == false)
        {
            GameObject bala = Instantiate(balasEnemy, Vector3.zero, transform.rotation);
            bala.SetActive(false);
            poolBalas.Add(bala);

            ResetearBala(direccionPlayer);

        }
        else
        {
            ResetearBala(direccionPlayer);
        }
    }

    public void ResetearBala(Vector3 direccion)
    {
        var objeto1 = poolBalas.Find(objeto => !objeto.activeSelf);
        objeto1.GetComponent<Rigidbody>().velocity = Vector3.zero;

        objeto1.transform.position = puntoDeDisparo.transform.position;
        objeto1.transform.rotation = transform.rotation;

        objeto1.SetActive(true);

        objeto1.GetComponent<BulletEnemy>().DireccionPlayer = direccion;
        
    }


    private void OnDisable()
    {
        puedeAtacar = true;
    }

}



    
