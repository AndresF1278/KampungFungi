using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Space]
    [Header("Variables de posicion y comportamiento del disparo")]
    [SerializeField] private GameObject prefabBullet;

    //Pos 0 del array = posicion mano izquierda // Pos 1 mano derecha 
    [SerializeField] private Transform[] pointsAttack = new Transform[2];
    [SerializeField] private bool disparaManoDerecha = true;
    [SerializeField] private bool puedeDisparar = true;

    [SerializeField] private float tiempoEntreCadaBala;
    [SerializeField] private float balasPorSegundo;

    [Space]
    [Header("Variables animacion Disparo")]
    [SerializeField] GameObject manos;
    private Animator animator;
    //0 IZQUIERDA, 1 D

    [Space]
    [Header("Object pool Balas")]
    public List<GameObject> poolBalas = new List<GameObject>();

    

    public float BalasPorSegundo { get => balasPorSegundo; set => balasPorSegundo = value; }

    



    private void Start()
    {
       
        puedeDisparar = true;
        animator = GetComponent<Animator>();
        tiempoEntreCadaBala = 1 / BalasPorSegundo;

    }
    void CrearBalas()
    {    
            if (poolBalas.Find(objeto => !objeto.activeSelf) == false)
            {
                GameObject bala = Instantiate(prefabBullet, Vector3.zero, transform.rotation);
                bala.SetActive(false);
                poolBalas.Add(bala);
                AttackShot();
            }
            else
            {
                AttackShot();
            }

    }
    private void Update()
    {
        
        float horizontalInput = Input.GetAxisRaw("ShotHorizontal");
        float verticalInput = Input.GetAxisRaw("ShotVertical");

        if (horizontalInput != 0 && verticalInput == 0)
        {
            transform.rotation = Quaternion.Euler(0, horizontalInput < 0 ? -90 : horizontalInput > 0 ? 90: -90, 0);
            CrearBalas();
           
        }
        else if (verticalInput != 0 && horizontalInput == 0)
        {

            transform.rotation = Quaternion.Euler(0, verticalInput < 0 ? 180 : verticalInput > 0 ? 0 : 180, 0);
            CrearBalas();
           
        }
       
    }

     /// <summary>
    /// /// Metodo que se encarga del disparo del player// el cual dispara de forma intercalada entre mano izquierda y derecha
    /// </summary>
    void AttackShot()
    {
        if (puedeDisparar)
        {
            switch (disparaManoDerecha)
            {
                case true:
                    RestablecerBalas(0);
                    break;
                case false:
                    RestablecerBalas(1);
                    break;
            }
            puedeDisparar = false;
            StartCoroutine(CadenciaDeDisparo());
        }

       

    }
    private void RestablecerBalas(int posMano)
    {
        var objeto1 = poolBalas.Find(objeto => !objeto.activeSelf);
        objeto1.GetComponent<Rigidbody>().velocity = Vector3.zero;
        objeto1.transform.position = pointsAttack[posMano].position;
        objeto1.transform.rotation = transform.rotation;

        objeto1.SetActive(true);

        if(posMano == 0) {

            disparaManoDerecha = false;

        }
        else
        {
            disparaManoDerecha = true;
        }
    }




    /// <summary>
    /// /// Metodo encargado de determinar cuando puede disparar el jugador
    /// </summary>
    IEnumerator CadenciaDeDisparo()
    {
        tiempoEntreCadaBala = 1 / BalasPorSegundo;
        yield return new WaitForSeconds(tiempoEntreCadaBala);
        puedeDisparar = true;
    }

}

