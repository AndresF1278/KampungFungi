using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

//using UnityEngine.UIElements;

public class HealthPlayer : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    const int dañoRecibido = 1;

    public Renderer rendererJugador;
    public float duracionParpadeo = 0.1f;
    public int cantidadParpadeos = 5;

    private bool golpeado = false;
    private Animator animator;
    [SerializeField] private LayerMask LayerEnemigo;
    [SerializeField] private LayerMask LayerPlayer;
    [SerializeField] private Renderer materialPlayer;
    [SerializeField] private Renderer[] ojosRenderer;
    private bool parpadeoActivo = false;
    [SerializeField] private Color colorDamage;

    [SerializeField] Volume volumeURP;
    [SerializeField] VolumeProfile volumeProfile;
    [SerializeField] Vignette vignette;
    
    private void Start()
    {
      volumeURP = GameObject.Find("PostProsesado").gameObject.GetComponent<Volume>();
        volumeURP.profile.TryGet(out vignette);

       animator = GetComponent<Animator>();
        hearts = UiManager.instance.corazones;
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
               
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
       
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //GameManager.instance.TerminarJuego();
            animator.SetTrigger("Muerte");
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            UiManager.instance.ActivarGameOver();
           
        }
        AudioManager.instance.playSfx("DañoHongo");
        UpdateHearts();
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHearts();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){


            if (!golpeado && !UiManager.instance.estaTransicion)
            {
                TakeDamage(dañoRecibido);
                Golpeado();
            }
            

          

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {


            if (!golpeado && !UiManager.instance.estaTransicion)
            {
                TakeDamage(dañoRecibido);
                Golpeado();
            }


          

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!golpeado && !UiManager.instance.estaTransicion)
            {
                TakeDamage(dañoRecibido);
                Golpeado();
            }
            
        }
    }

    private void Golpeado()
    {
       

        golpeado = true;

        Material[] materiales = materialPlayer.materials;

        for (int i = 0; i < materiales.Length; i++)
        {
            materiales[i].color = colorDamage;
        }


        Physics.IgnoreLayerCollision(gameObject.layer, LayerEnemigo);
        StartCoroutine(RealizarParpadeo());
        StartCoroutine(TiempoDesactivarColision());
        StartCoroutine(TiempoGolpeado());
        vignette.active = true;

    }


    IEnumerator TiempoGolpeado()
    {
        yield return new WaitForSeconds(0.9f);

        Material[] materiales = materialPlayer.materials;

        for (int i = 0; i < materiales.Length; i++)
        {
            materiales[i].color = Color.white;
        }
        GetComponent<Collider>().excludeLayers = 0;
        golpeado = false;
            
    }
    
    IEnumerator TiempoDesactivarColision()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Collider>().excludeLayers = LayerEnemigo;
    }

    private IEnumerator RealizarParpadeo()
    {
        for (int i = 0; i < cantidadParpadeos; i++)
        {
            rendererJugador.enabled = !rendererJugador.enabled;
            ojosRenderer[0].enabled = !ojosRenderer[0].enabled;
            ojosRenderer[1].enabled = !ojosRenderer[1].enabled;
            yield return new WaitForSeconds(duracionParpadeo);
        }

        rendererJugador.enabled = true;
        ojosRenderer[0].enabled = true;
        ojosRenderer[1].enabled = true;
        parpadeoActivo = false;
        vignette.active = false;
    }

    private void IniciarDatosDeLaVida()
    {

    }


}
