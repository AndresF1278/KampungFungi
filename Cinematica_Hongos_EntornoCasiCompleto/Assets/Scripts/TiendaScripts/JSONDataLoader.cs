using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemCargado
{
    public int id;
    public string nombre;
    public string descripcion;
    public Atributos atributos;
    public int precio;
    public string nombreModelo;
}
[System.Serializable]
public class Atributos
{
    public int curacion;
    public float dano;
    public float rango;
    public float balasPorSegundo;
    public float velocidadDeDisparo;
    public float velocidadDeMovimiento;
    public string habilidadEspecial;
}

[System.Serializable]
public class ItemList
{
    public List<ItemCargado> items;
}

public class JSONDataLoader : MonoBehaviour
{
    public TextAsset jsonFile;
    public ItemList itemList;

    private void Awake()
    {
        string jsonString = jsonFile.text;
        itemList = JsonUtility.FromJson<ItemList>(jsonString);
    }
    private void Start()
    {

    }

}
