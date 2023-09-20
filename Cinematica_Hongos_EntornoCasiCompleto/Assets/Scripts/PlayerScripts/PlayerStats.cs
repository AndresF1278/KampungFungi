using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerStats", order = 2)]
public class PlayerStats : ScriptableObject
{
    
    [SerializeField] private float velocidadPlayer;//
    [SerializeField] private float dañoPlayer;//
    [SerializeField] private int vidaMaximaPlayer;//
    [SerializeField] private int balasPorSegundo;
    [SerializeField] private int velocidadDeLaBalaPlayer;
    [SerializeField] private int rangoDeDisparoPlayer;//

    public float VelocidadPlayer { get => velocidadPlayer; set => velocidadPlayer = value; }
    public float DañoPlayer { get => dañoPlayer; set => dañoPlayer = value; }
    public int VidaMaximaPlayer { get => vidaMaximaPlayer; set => vidaMaximaPlayer = value; }
    public int BalasPorSegundo { get => balasPorSegundo; set => balasPorSegundo = value; }
    public int VelocidadDeLaBalaPlayer { get => velocidadDeLaBalaPlayer; set => velocidadDeLaBalaPlayer = value; }
    public int RangoDeDisparoPlayer { get => rangoDeDisparoPlayer; set => rangoDeDisparoPlayer = value; }
}
