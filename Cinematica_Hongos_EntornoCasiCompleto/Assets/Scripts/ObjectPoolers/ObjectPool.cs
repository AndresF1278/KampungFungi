using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DificultadEnemigo
{

    Facil,
    normal,
    dificil

};


[System.Serializable]
public class ObjectPool
{
    public string nombrePool;
    public GameObject prefabEnemigo;
    public int tamañoPool;
    public DificultadEnemigo dificultad;
    

}

[System.Serializable]
public class DatosRondas
{
    public string nombrePool;
    public float probavilidadSpawn;
}