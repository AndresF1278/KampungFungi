using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Boton : MonoBehaviour
{
    public bool botonPresionado = false;
    [SerializeField] private Color colorPresionado;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.instance.NumDia % 4 != 0 && !GameObject.FindAnyObjectByType<PlayerMovement>().estaEnDialogo)
        {
            if (!EnemySpawnManager.instance.PuedeSpawn && !botonPresionado)
            {
                AudioManager.instance.playSfx("BotonBatalla");
                EnemySpawnManager.instance.PuedeSpawn = true;
                RondasEnemigos.instance.RondaFinalizada = false;
                Barrera barrera = GameObject.Find("BARRERABosque").GetComponent<Barrera>();
                barrera.SubirBarrera();
                //RondasEnemigos.instance.puedeAmanecer = true;
                botonPresionado = true;
                RondasEnemigos.instance.estaPausado = false;
                ColorPresionado();
                if (TutorialManager.Instance.tutorialBotonPausar)
                {
                    TutorialManager.Instance.TutorialBotonPausar();
                }
               
                return;

            }

            if (botonPresionado)
            {
                RondasEnemigos.instance.estaPausado = true;
                AudioManager.instance.playSfx("BotonBatalla");
                ColorNormal();
            }
        }


    }

    public void ColorPresionado()
    {
        GetComponent<SpriteRenderer>().color = colorPresionado;
    }

    public void ColorNormal()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }


}
