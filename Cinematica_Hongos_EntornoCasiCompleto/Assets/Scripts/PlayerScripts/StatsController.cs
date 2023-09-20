using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StatsController : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private HealthPlayer healthManager;
    [SerializeField] private Bullet bullet;
    List<float> listaStats = new List<float>(4);
    private void Start()
    {
        AsignarComponentes();
        StatsInicialesPlayer();
        GameManager.instance.gameOverEvent += StatsInicialesPlayer; 
        
    }

    public void ActualizarStatsBalas(Bullet bala)
    {
        bala.DañoDeBala = bullet.DañoDeBala;
        bala.TiempoMaximo = bullet.TiempoMaximo;
        bala.Speed = bullet.Speed;
        bala.puedeEnvenenar = bullet.puedeEnvenenar;
        bala.puedeCongelar = bullet.puedeCongelar;
        bala.balaSigue = bullet.balaSigue;
        
        bala.colorActual = bullet.colorActual;
           
        Material materialDeLaBala = bala.GetComponent<Renderer>().material;
        materialDeLaBala.SetColor("_EmissionColor", bala.colorActual);
        materialDeLaBala.SetColor("_Color", Color.white);

        Material trailmaterial = bala.GetComponentInChildren<TrailRenderer>().material;
        trailmaterial.SetColor("_EmissionColor", bala.colorActual);
        
      

    }

    public void StatsInicialesPlayer()
    {
        playerMovement.Velocidad = playerStats.VelocidadPlayer; //cargar velocidad de movimiento del jugador
        bullet.DañoDeBala = playerStats.DañoPlayer;   //cargar daño que hara la bala del jugador
        bullet.TiempoMaximo = playerStats.RangoDeDisparoPlayer; // cargar el tiempo que cada bala estara en la scena (alcance de bala)
        bullet.Speed = playerStats.VelocidadDeLaBalaPlayer; // cargar velocidad de la bala
        bullet.puedeEnvenenar = false;
        bullet.puedeCongelar = false;
        bullet.balaSigue = false;
        bullet.colorActual = new Color(255 / 255, 255 / 255, 255 / 255, 1f);
        playerAttack.BalasPorSegundo = playerStats.BalasPorSegundo;
        healthManager.maxHealth = playerStats.VidaMaximaPlayer; // cargar vida maxima del jugador
        healthManager.currentHealth = healthManager.maxHealth;
        MostrarStats();
    }

    public void AñadirStatsPlayer(float velocidadPlayer, float daño,
       float rango, float velocidadBala, float balasPorSegundo, int vidaAdicional)
    {
        playerMovement.Velocidad += velocidadPlayer;
        bullet.DañoDeBala += daño;   //cargar daño que hara la bala del jugador
        bullet.TiempoMaximo += rango; // cargar el tiempo que cada bala estara en la scena (alcance de bala)
        bullet.Speed += velocidadBala; // cargar velocidad de la bala
        playerAttack.BalasPorSegundo += balasPorSegundo;
        healthManager.currentHealth += vidaAdicional; // cargar vida maxima del jugador
        listaStats.Clear();

        listaStats.Add(daño);
        listaStats.Add(balasPorSegundo);
        listaStats.Add(rango);
        listaStats.Add(velocidadBala);
        listaStats.Add(velocidadPlayer);
        MostrarStatsSuma(listaStats);
        healthManager.UpdateHearts();
        MostrarStats();
    }

    private void AsignarComponentes()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
        playerAttack = this.GetComponent<PlayerAttack>();
        healthManager = this.GetComponent<HealthPlayer>();
    }


    public void MostrarStats()
    {
        UiManager.instance.textosEstadisticas[0].text = bullet.DañoDeBala.ToString();
        UiManager.instance.textosEstadisticas[1].text = playerAttack.BalasPorSegundo.ToString();
        UiManager.instance.textosEstadisticas[2].text = bullet.TiempoMaximo.ToString();
        UiManager.instance.textosEstadisticas[3].text = bullet.Speed.ToString();
        UiManager.instance.textosEstadisticas[4].text = playerMovement.Velocidad.ToString();

    }

    //public void MostrarStatsSuma(List<float> stats)
    //{
    //    foreach ( float item in listaStats)
    //    {
    //        foreach (TMP_Text texto in UiManager.instance.textosEstadisticasSuma)
    //        {
    //            if(item < 0)
    //            {
    //                texto.gameObject.SetActive(true);
    //                texto.color = Color.red;
    //                texto.text = $"- {item}";
    //            }
    //            else if(item > 0) 
    //            {
    //                texto.gameObject.SetActive(true);
    //                texto.color = Color.green;
    //                texto.text = $"+ {item}";
    //            }
    //        }
    //    }
    //}

    public void MostrarStatsSuma(List<float> stats)
    {
        StopCoroutine(tiempoOcultarStats());
 
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i] != 0)
            {
                UiManager.instance.panelStatsGrande();
            }

        }
        
            for (int i = 0; i < UiManager.instance.textosEstadisticasSuma.Count; i++)
            {

                if (stats[i] == 0)
                {

                    UiManager.instance.textosEstadisticasSuma[i].gameObject.SetActive(false);
                    Debug.Log("No suma");
                }

                if (stats[i] < 0)
                {

                    UiManager.instance.textosEstadisticasSuma[i].gameObject.SetActive(false);
                    UiManager.instance.textosEstadisticasSuma[i].gameObject.SetActive(true);
                    UiManager.instance.textosEstadisticasSuma[i].color = Color.red;
                    UiManager.instance.textosEstadisticasSuma[i].text = $"- {stats[i]}";
                }
                else if (stats[i] > 0)
                {

                    UiManager.instance.textosEstadisticasSuma[i].gameObject.SetActive(false);
                    UiManager.instance.textosEstadisticasSuma[i].gameObject.SetActive(true);
                    UiManager.instance.textosEstadisticasSuma[i].color = Color.green;
                    UiManager.instance.textosEstadisticasSuma[i].text = $"+ {stats[i]}";
                }
                StartCoroutine(tiempoOcultarStats());
            }
        
            
    }

    IEnumerator tiempoOcultarStats()
    {
        yield return new WaitForSeconds(2.5f);
        UiManager.instance.panelStatsNormal();

        for (int i = 0; i < UiManager.instance.textosEstadisticasSuma.Count; i++)
        {
            UiManager.instance.textosEstadisticasSuma[i].gameObject.SetActive(false);
            
        }

    }
}
