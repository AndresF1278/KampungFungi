using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineMovimientoCamara : MonoBehaviour
{
    public static CinemachineMovimientoCamara Instance;

    private CinemachineVirtualCamera mCam;

    private CinemachineBasicMultiChannelPerlin mBasicMultiChannelPerlin;

    private float tiempoMovimiento;
    private float tiempoMovimientoTotal;
    private float intesidadInicial;


    private void Awake()
    {
        Instance = this;
        mCam = GetComponent<CinemachineVirtualCamera>();
        mBasicMultiChannelPerlin = mCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); 
    }


    public void MoverCamara(float intesidad,float frecuencia, float tiempo )
    {
        mBasicMultiChannelPerlin.m_AmplitudeGain = intesidad;
        mBasicMultiChannelPerlin.m_FrequencyGain = frecuencia;
        intesidadInicial = intesidad;
        tiempoMovimientoTotal = tiempo;
        tiempoMovimiento = tiempo;
    }


    private void Update()
    {
        if (tiempoMovimiento> 0)
        {
            tiempoMovimiento -= Time.deltaTime;
            mBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(intesidadInicial,
                0, 1 - (tiempoMovimiento / tiempoMovimientoTotal));
        }
    }




}
