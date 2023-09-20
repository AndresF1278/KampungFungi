using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AsignarBotones : MonoBehaviour
{

    [SerializeField] Button botonPlay, botonOpciones, botonSalir, botonTutorial, botonCreditos;
    [SerializeField] Color coloInactico;

    private void Start()
    {

       AudioManager.instance.PlayMusic("MusicaJuegoInterfaces");
        if (GameManager.instance.realizoTutorial)
        {
            botonPlay.interactable = true;
        }
        else
        {
            botonPlay.interactable = false;
            botonPlay.GetComponent<Image>().color = coloInactico;
        }
        botonPlay.onClick.AddListener(BotonIniciarJuego);
        botonPlay.onClick.AddListener(DesctivarTutorial);
        botonOpciones.onClick.AddListener(CambiarOpciones);
        botonSalir.onClick.AddListener(BotonSalir);
        botonCreditos.onClick.AddListener(Creditos);
        botonTutorial.onClick.AddListener(ActivarTutorial);
        botonTutorial.onClick.AddListener(BotonIniciarJuego);

    }

    public void CambiarOpciones()
    {
        CambiarScenaNombre("OpcionesPrincipales");
    }

    public void CambiarScenaNombre(string nombreScena)
    {
        AudioManager.instance.playSfx("BotonInterfaz");
        SceneController.instance.IrScenaConNombre(nombreScena);
        
    }

    public void ActivarTutorial()
    {
        GameManager.instance.TutorialActivo = true;
        GameManager.instance.realizoTutorial = true;
    }

    public void Creditos()
    {
        //AudioManager.instance.musicSource.mute = true;
        SceneController.instance.IrScenaConNombre("Creditos");
    }


    public void DesctivarTutorial()
    {
        GameManager.instance.TutorialActivo = false;
    }

    public void BotonIniciarJuego()
    {
        AudioManager.instance.playSfx("BotonInterfaz");
        UiManager.instance.TransicionMenuCerrar();
        Image fade = GameObject.Find("PanelFade").GetComponent<Image>();
        fade.color = Color.clear;

        botonPlay.onClick.RemoveAllListeners();
        botonOpciones.onClick.RemoveAllListeners();
        botonSalir.onClick.RemoveAllListeners();
        botonTutorial.onClick.RemoveAllListeners();
        botonCreditos.onClick.RemoveAllListeners();
        Invoke("iniciarJuego", 1.2f);
    }

    private void iniciarJuego()
    {
        SceneController.instance.EscenaIniciarJuego();
    }


    public void BotonSalir()
    {
        GameManager.instance.ExitGame();
    }
}
