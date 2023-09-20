using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiendaManager: MonoBehaviour
{
    public static TiendaManager instance;
    private StatsController statsController;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        statsController = GameManager.FindAnyObjectByType<StatsController>();
    }


    public void ComprarItem(ItemRecolectable itemSeleccionado)
    {
        if(GameManager.instance.Monedas>= itemSeleccionado.Precio)
        {
            itemSeleccionado.AñadirEstadisticas();
            GameManager.instance.RestarMonedas(itemSeleccionado.Precio);
           // GameManager.instance.Monedas -= itemSeleccionado.Precio;
            itemSeleccionado.gameObject.SetActive(false);
            UiManager.instance.ActualizarDinero();
            AudioManager.instance.playSfx("ComprarItem");

        }
        else
        {
            UiManager.instance.MostrarDineroInsuficiente();
        }
    }

}
