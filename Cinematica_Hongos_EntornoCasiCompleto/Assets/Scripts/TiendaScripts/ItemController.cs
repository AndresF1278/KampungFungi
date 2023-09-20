using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private JSONDataLoader jsonDataLoader;
    ItemList itemsCargados;
    [SerializeField] int cantidadItems;
    [SerializeField] List<GameObject> itemsCreadoss;
    public List<int> excludedItemIds;

    [SerializeField] private List<Transform> posItems;
    private void Start()
    {

      
        jsonDataLoader = FindAnyObjectByType<JSONDataLoader>();
        itemsCargados = jsonDataLoader.itemList;
        cantidadItems = itemsCargados.items.Count;

        GenerarItemAleatorio();

      //  GameManager.instance.eventAumentarDia += eliminarItems;
       // GameManager.instance.eventAumentarDia += GenerarItemAleatorio;
        


    }
    public void GenerarItemAleatorio()
    {

        if(excludedItemIds.Count >= 10) {
            excludedItemIds.Clear();
        }


        List<int> indicesDisponibles = new List<int>();
        for (int i = 0; i < cantidadItems; i++)
        {
            indicesDisponibles.Add(i);
        }

        for (int i = 0; i < posItems.Count; i++)
        {
            int randomIndex;
            int itemRandom;

            do
            {
                randomIndex = Random.Range(0, indicesDisponibles.Count);
                itemRandom = indicesDisponibles[randomIndex];
            }
            while (excludedItemIds.Contains(itemRandom)); // Verifica si está en la lista de excluidos

            indicesDisponibles.RemoveAt(randomIndex);
            
            GameObject prefabItem = Resources.Load<GameObject>(itemsCargados.items[itemRandom].nombreModelo);
            GameObject currentItem = Instantiate(prefabItem, posItems[i].position, prefabItem.transform.rotation);
            itemsCreadoss.Add(currentItem);
            GuardarStatsItem(currentItem, itemRandom);
            FindAnyObjectByType<TiendaUI>().AñadirTextoDePrecio(i, currentItem.GetComponent<ItemRecolectable>());
            currentItem = null;
        }
    }

    private void GuardarStatsItem(GameObject itemSelect, int IdItem)
    {
      
        ItemRecolectable statsItem = itemSelect.GetComponent<ItemRecolectable>();

        statsItem.Id = itemsCargados.items[IdItem].id;
        statsItem.Nombre = itemsCargados.items[IdItem].nombre;
        statsItem.Descripcion = itemsCargados.items[IdItem].descripcion;
        statsItem.Atributos = itemsCargados.items[IdItem].atributos;
        statsItem.Precio = itemsCargados.items[IdItem].precio;
        


    }


    public void eliminarItems()
    {
        foreach (GameObject item in itemsCreadoss)
        {
            if(item != null)
            {
                Destroy(item);
                
            }
        }
        itemsCreadoss.Clear();
    }
}
