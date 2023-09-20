using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Barrera : MonoBehaviour
{
    [SerializeField] private Transform PuntoArriba;
    [SerializeField] private Transform PuntoAbajo;




    public void SubirBarrera()
    {
        transform.DOMoveY(PuntoArriba.position.y,1 ).SetEase(Ease.Linear);
       
    }

    
    public void BajarBarrera()
    {
        transform.DOMoveY(PuntoAbajo.position.y, 1).SetEase(Ease.Linear);
        
    }
}
