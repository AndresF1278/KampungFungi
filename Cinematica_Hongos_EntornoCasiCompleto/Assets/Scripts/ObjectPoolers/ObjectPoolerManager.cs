using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerManager : MonoBehaviour
{
    public static ObjectPoolerManager instance;


    public List<ObjectPool> poolEnemigos;
    public List<float> probabilidades;
    public List<ListasEnemigosCreados> listasEnemigosCreados;


    private void Awake()
    {
        if(instance== null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CrearInstanciasEnemigos();
    }
    private void Start()
    {

        if (listasEnemigosCreados.Count != probabilidades.Count)
        {
            Debug.LogError("Los arrays de objetos y probabilidades deben tener la misma longitud.");
            return;
        }
    }

   private void CrearInstanciasEnemigos() {

        listasEnemigosCreados = new List<ListasEnemigosCreados>();
        foreach (ObjectPool pool in poolEnemigos)
        {
            ListasEnemigosCreados listaEnemigosCreados = new ListasEnemigosCreados();
            listaEnemigosCreados.enemigosCreados = new List<GameObject>();
            listasEnemigosCreados.Add(listaEnemigosCreados);
        }

        for (int i = 0; i < poolEnemigos.Count; i++)
        {
            for (int x = 0; x < poolEnemigos[i].tamañoPool; x++)
            {
                GameObject instanciaEnemigo = Instantiate(poolEnemigos[i].prefabEnemigo, Vector3.one, Quaternion.identity);
                listasEnemigosCreados[i].enemigosCreados.Add(instanciaEnemigo);
                instanciaEnemigo.SetActive(false);
                
                instanciaEnemigo.transform.parent = transform;
            }
        }


    }







}
[System.Serializable]
public class ListasEnemigosCreados
{
    public List<GameObject> enemigosCreados;
}






