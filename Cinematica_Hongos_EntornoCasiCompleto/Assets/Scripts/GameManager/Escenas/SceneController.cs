using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SceneShop()
    {
        if (SceneManager.GetActiveScene().name == "ShopScene")
        {
            SceneManager.LoadScene("SceneMain");
        }
        else
        {
            SceneManager.LoadScene("ShopScene");
        }      
    }

    public void IrScenaConNombre(string nombreScena)
    {
        AudioManager.instance.playSfx("BotonInterfaz");
        SceneManager.LoadScene(nombreScena);
        if(GameObject.FindAnyObjectByType<PlayerAttack>() != null)
        {
            PlayerController.instance.gameObject.GetComponent<PlayerAttack>().poolBalas.Clear();
        }
        
    }
    public void IrScenaSiquiente()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        int buildIndex = activeScene.buildIndex;
        SceneManager.LoadScene(buildIndex + 1);

    }
    public void IrMenu()
    {

        AudioManager.instance.playSfx("BotonInterfaz");
        Invoke("CambioMenu", 1.6f);
    }

    public void CambioMenu()
    {
        

        GameManager.instance.enJuego = false;
        SceneManager.LoadScene("InterfazInicial");
        UiManager.instance.TransicionMenuAbrir();



    }


    public void EscenaIniciarJuego()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
       
        
       
        SceneManager.LoadScene("AldeaGamePlay");
        AudioManager.instance.PlayMusic("MusicaJuego");
        
        Debug.Log("Inicia la carga de la escena"); 
    }

    private void fadeIniciar()
    {
     
    }
    private IEnumerator WaitAndCallDespuesDeCargarEscena()
    {
        // Esperar un pequeño período de tiempo (puedes ajustar el tiempo si es necesario).
        yield return new WaitForSeconds(0.1f);

        // Llamar a DespuesDeCargarEscena.
        DespuesDeCargarEscena();
    }

    public void DespuesDeCargarEscena()
    {

        if (GameManager.instance != null)
        {
            GameManager.instance.IniciarJuego();
           
            
        }
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "AldeaGamePlay")
        {
            // Desuscribirse primero del evento.
            SceneManager.sceneLoaded -= OnSceneLoaded;
            
            // Llamar al Coroutine para esperar y luego llamar a DespuesDeCargarEscena().
            StartCoroutine(WaitAndCallDespuesDeCargarEscena());
        }
    }
}
