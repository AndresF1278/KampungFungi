using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeclasTutorial : MonoBehaviour
{
    [SerializeField] GameObject TeclaArriba, TeclaAbajo, TeclaDerecha, teclaIzquierda;

    PlayerMovement player;
    [SerializeField] private bool Movimiento;
    private string horizontal;
    private string vertical;
    [SerializeField] private GameObject check;
    
    private void Start()
    {
        switch (Movimiento)
        {
            case true:
                horizontal = "Horizontal";
                vertical = "Vertical";
                break;
            case false:
                horizontal = "ShotHorizontal";
                vertical = "ShotVertical";
                break;
        }
    }


    private void Update()
    {
        if(GameObject.FindAnyObjectByType<PlayerMovement>() != null)
        {
            player = GameObject.FindAnyObjectByType<PlayerMovement>();

            if (player.estaEnDialogo)
            {
                return;
            }
            else
            {
                if (Movimiento)
                {
                    if(GameManager.instance.scenaActual == GameManager.ScenaActual.aldea)
                    {
                        TutoriaMovimiento();
                        ActivarCheck();

                    }
                }
                if (!Movimiento)
                {
                    if (GameManager.instance.scenaActual == GameManager.ScenaActual.bosque)
                    {
                        TutoriaMovimiento();
                        ActivarCheck();

                    }
                }

            }


        }
    }


    public void TutoriaMovimiento()
    {
       

        float horizontalAxis = Input.GetAxisRaw(horizontal);
        float verticalAxis = Input.GetAxisRaw(vertical);

        if(horizontalAxis > 0f) {

            TeclaDerecha.SetActive(false);

        }else if(horizontalAxis < 0f)
        {

            teclaIzquierda.SetActive(false);
        }

        if (verticalAxis > 0f)
        {

            TeclaArriba.SetActive(false);

        }
        else if (verticalAxis < 0f)
        {

            TeclaAbajo.SetActive(false);
        }

    }


   public void ActivarCheck()
    {
        if(!TeclaArriba.activeInHierarchy &&
            !TeclaAbajo.activeInHierarchy &&
            !teclaIzquierda.activeInHierarchy &&
            !TeclaDerecha.activeInHierarchy)
        {
            check.SetActive(true);
            Invoke("DesactivarCanvas", 1.3f);
            
        }
    }


    public void  DesactivarCanvas() {

        if (Movimiento)
        {
            if (GameManager.instance.TutorialActivo)
            {
                TutorialManager.Instance.TutorialReloj();
            }
            
        }
      
       gameObject.SetActive(false);

    
    }



}
