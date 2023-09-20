using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TiendaUI : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> textosPrecios;
   public void AñadirTextoDePrecio(int indiceTexto, ItemRecolectable itemSelect)
    {
        textosPrecios[indiceTexto].text = itemSelect.Precio.ToString();
    }
}
