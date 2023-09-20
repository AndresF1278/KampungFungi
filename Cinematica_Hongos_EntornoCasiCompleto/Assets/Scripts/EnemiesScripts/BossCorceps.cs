using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCorceps : MonoBehaviour
{
    public enum EstadoAtaque
    {
        bulletHell,
        salto
    }
    
    
    
    [SerializeField] private float jumpForce;

    [SerializeField] private float tiempoEntreSalto;

    [SerializeField] private  float radioTocaSuelo;

    [SerializeField] private GameObject puntoSalto;

    [SerializeField] private LayerMask capaDeDeteccion;

    public Animator animator;
    public Vector3 velocidadRotacion = new Vector3(0, 30, 0);

    private Rigidbody rb;

    [SerializeField] private GameObject target;
    [SerializeField] private bool puedeSaltar;
    [SerializeField] private bool salto;
    [SerializeField] private List<GameObject> puntosDisparo;
    [SerializeField] private List<GameObject> balasPool;
    [SerializeField] private int balasIniciales = 100;
    [SerializeField] private GameObject prefabBala;
    private int[] lastUsedBulletIndex = new int[4];
   [SerializeField] private EstadoAtaque estadoAtaque;

    private float tiempoEntreCadaEstado = 5;
    private float tiempoEntreDisparo = 0.2f;
    [SerializeField] private bool puedeDisparar;
     public bool activarAtaque;
    public bool muerte;
    
    private void Start()
    {
        muerte = false;
        rb = GetComponent<Rigidbody>();
        puedeSaltar = true;
        salto = false;
        animator = GetComponent<Animator>();
        puedeDisparar = true;
        for(int i = 0; i < balasIniciales; i++)
        {
            GameObject balaCreada =  Instantiate(prefabBala, transform.position, Quaternion.identity);
            balaCreada.SetActive(false);
            balasPool.Add(balaCreada);
        }
        tiempoEntreCadaEstado = 0;
        activarAtaque = false;
        target = GameObject.FindAnyObjectByType<PlayerController>().gameObject;

       

        

    }

    private void Update()
    {
        if (muerte)
        {
            StopAllCoroutines();
        }

        if (activarAtaque && !muerte)
        {
            SeleccionarAtaque();

            if (estadoAtaque == EstadoAtaque.salto)
            {
                SaltoDisparo();
            }
            else
            {
                //bullet
                BulletHell();
            }
        }
      
        
        

        TocaSuelo();
    }


    public void Muerte()
    {
        muerte = true;
        animator.SetTrigger("Muerte");
        //RondasEnemigos.instance.MuerteEnemigos();
    }
    public void ActivarAtaqueAlEntrarAPelear()
    {
        Invoke("ActivarElAtaque", 2);
    }

    public void ActivarElAtaque()
    {
        activarAtaque = true;
    }

    private void SeleccionarAtaque()
    {
        tiempoEntreCadaEstado -= Time.deltaTime;

        if(tiempoEntreCadaEstado <= 0)
        {
            float randomNum = Random.Range(0, 1.1f);

            if(randomNum <= 0.5f)
            {
                estadoAtaque = EstadoAtaque.bulletHell;
            }
            else
            {
                estadoAtaque = EstadoAtaque.salto;
            }

            tiempoEntreCadaEstado = 10;
        }
    }


    public void ShootFromAllPoints()
    {
        for (int shooterIndex = 0; shooterIndex < 4; shooterIndex++)
        {
            GameObject bullet = GetNextBullet(shooterIndex);
            if (bullet != null)
            {
                // Configura la posición y dirección del disparo y activa la bala.
                bullet.transform.position = puntosDisparo[shooterIndex].transform.position;
                bullet.transform.rotation = puntosDisparo[shooterIndex].transform.rotation;
                bullet.SetActive(true);
            }
        }
    }


    GameObject GetNextBullet(int shooterIndex)
    {
        for (int i = 0; i < balasIniciales; i++)
        {
            int index = (lastUsedBulletIndex[shooterIndex] + i) % balasIniciales;
            if (!balasPool[index].activeSelf)
            {
                lastUsedBulletIndex[shooterIndex] = index;
                return balasPool[index];
            }
        }

        // Si no se encuentra una bala disponible, puedes manejarlo de acuerdo a tus necesidades (puede ser crear una nueva o reciclar una existente).
        return null;
    }


    void BulletHell()
    {
        if (!muerte)
        {
            transform.Rotate(velocidadRotacion * Time.deltaTime);
        }
        



        if (puedeDisparar)
        {
            animator.SetTrigger("Bullet");
            ShootFromAllPoints();

            puedeDisparar = false;
            StartCoroutine(TiempoEntreDisparos());
        }
        
    }

    IEnumerator TiempoEntreDisparos()
    {
        yield return new WaitForSeconds(tiempoEntreDisparo);
        puedeDisparar = true;
    }



    public void SaltoDisparo()
    {
        if (puedeSaltar && TocaSuelo())
        {
            animator.SetTrigger("Salto");
            animator.SetBool("tocaSuelo", false);
            Invoke("FuerzaSalto", 0.3f);
            StartCoroutine(TiempoEntreSalto());
            puedeSaltar = false;
        }
    }

    public void FuerzaSalto()
    {
        Vector3 directionToTarget = target.transform.position - transform.position;
        directionToTarget.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1000 * Time.deltaTime);

        // Aplica una fuerza para saltar hacia el objetivo.
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(directionToTarget.normalized * jumpForce, ForceMode.VelocityChange);
        rb.AddForce(Vector3.up * (jumpForce + 12), ForceMode.VelocityChange);

        // Puedes usar una corutina para volver a habilitar el salto después de un tiempo si lo deseas.
        
        Invoke("Cae", 0.3F);
    }

    IEnumerator TiempoEntreSalto()
    {
        yield return new WaitForSeconds(tiempoEntreSalto);
        puedeSaltar = true;
    }

    public void Cae()
    {
        
        salto = true;
    }

    public void DisparaCruz()
    {
     

        ShootFromAllPoints();



    }

    public bool TocaSuelo()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioTocaSuelo, capaDeDeteccion);
        if (colliders.Length > 0) // Verificar si hay elementos en el array
        {
            if (salto)
            {
                DisparaCruz();
                CinemachineMovimientoCamara.Instance.MoverCamara(5, 5, 0.5f);
                animator.SetBool("tocaSuelo", true);
                Debug.Log("CAE EL BOSS");
                AudioManager.instance.playSfx("SaltoEnemigo");
                salto = false;
            }

            return true;
        }
        else
        {
            return false;
        }



    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoSalto.transform.position, radioTocaSuelo);
    }

    
}
