using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField]  Image imagen;
    private Tweener tweener; // Almacenar la referencia al tweener actual

    public void FadeOut()
    {
        // Si hay un tweener en curso, detenerlo
        tweener?.Kill();

        // Iniciar un nuevo tweener para el fade out
        tweener = imagen.DOFade(0, 2).OnComplete(FadeComplete);
    }

    public void FadeIn()
    {
        // Si hay un tweener en curso, detenerlo
        tweener?.Kill();

        // Iniciar un nuevo tweener para el fade in
        tweener = imagen.DOFade(1, 0.8f).OnComplete(FadeComplete);
    }

    private void FadeComplete()
    {
        // El tweener ha terminado
        tweener = null;
        Debug.Log("La animación de fade ha terminado.");
    }


}
