using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EstadoDelJuego
{
    Batalla,
    EnJuego

}
public class GameStateController : MonoBehaviour
{
    public static GameStateController instance; 
    public EstadoDelJuego estadoActual;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
           Destroy(gameObject);
        }
    }


    public delegate void EstadoPelea();
    public EstadoPelea eventEstadoPelea;

    public delegate void SalirEstadoPelea();
    public SalirEstadoPelea eventSalirEstadoPelea;



    public void CambiarEstadoActual(EstadoDelJuego estado)
    {
        estadoActual = estado;

        switch (estadoActual)
        {
            case EstadoDelJuego.Batalla:
                eventEstadoPelea();
                break;
            case EstadoDelJuego.EnJuego:
                eventSalirEstadoPelea();
                break;
        }
    }


}
