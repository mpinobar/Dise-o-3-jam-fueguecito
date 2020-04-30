using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : TemporalSingleton<EventManager>
{
    [SerializeField] float duracionEvento = 3;
    float delayEvento;
    [SerializeField] float tiempoEntreEventos = 2f;
    [SerializeField] int frecuenciaAparicionPolilla = 2;
    [SerializeField] float porcentajeAumentoFrecuenciaPorEvento = 10f;
    [SerializeField] float tiempoMinimo = 1f;
    int numEvento;
    int eventoActual;

    float temporizador;
    float temporizadorEspera;

    bool esperandoEntreEventos;

    // Start is called before the first frame update
    void Start()
    {
        temporizador = duracionEvento;
        delayEvento = duracionEvento * 0.33f;
        CambiarEvento();
        esperandoEntreEventos = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!esperandoEntreEventos)
        {
            temporizador -= Time.deltaTime;
            if (temporizador <= 0)
            {
                CambiarEvento();
                temporizador = duracionEvento;
                delayEvento = duracionEvento * 0.33f;
                esperandoEntreEventos = true;
            }
        }
        else
        {
            temporizadorEspera -= Time.deltaTime;
            if(temporizadorEspera <= 0)
            {
                esperandoEntreEventos = false;
                temporizadorEspera = tiempoEntreEventos;
            }
        }
        
    }

    private void CambiarEvento()
    {        
        numEvento++;

        if (numEvento % frecuenciaAparicionPolilla == 0)
        {
            MothSpawner.Instance.SpawnMoth();
        }
        tiempoEntreEventos *= (1 - porcentajeAumentoFrecuenciaPorEvento * 0.01f);
        duracionEvento *= Mathf.Max(tiempoMinimo, (1 - porcentajeAumentoFrecuenciaPorEvento * 0.01f));
        int aux = eventoActual;
        while (eventoActual == aux)
        {
            eventoActual = UnityEngine.Random.Range(0, 3);
        }
        switch (eventoActual)
        {
            case 0:
                RainScript.Instance.SpawnRain(delayEvento,duracionEvento);
                break;
            case 1:
                FingerSpawner.Instance.SpawnFinger(numEvento, duracionEvento);
                break;
            case 2:
                Spawner.Instance.SpawnWind(delayEvento, duracionEvento);
                break;
        }
    }
}
