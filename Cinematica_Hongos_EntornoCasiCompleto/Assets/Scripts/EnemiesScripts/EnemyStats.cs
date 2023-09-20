using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public enum TipoDeEnemigo {Distancia, Mele, Estatico }

    [SerializeField] private TipoDeEnemigo tipoDeEnemigo;
    [SerializeField] private float velocidadDeMovimiento;
    [SerializeField] private float dañoAtaque;
    [SerializeField] private float vidaMaxima;
    private float cadenciaDeAtaque;

    public TipoDeEnemigo TipoDeEnemigo1 { get => tipoDeEnemigo; set => tipoDeEnemigo = value; }
    public float VelocidadDeMovimiento { get => velocidadDeMovimiento; set => velocidadDeMovimiento = value; }
    
    public float VidaMaxima { get => vidaMaxima; set => vidaMaxima = value; }
    public float CadenciaDeAtaque { get => cadenciaDeAtaque; set => cadenciaDeAtaque = value; }
}
