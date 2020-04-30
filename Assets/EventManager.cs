using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : TemporalSingleton<EventManager>
{
    [SerializeField] float duracionEvento = 3;
    float delayEvento;
    [SerializeField] int frecuenciaAparicionPolilla = 2;
    [SerializeField] float porcentajeAumentoFrecuenciaPorEvento = 10f;
    int numEvento;
    int eventoActual;

    float temporizador;


    // Start is called before the first frame update
    void Start()
    {
        temporizador = duracionEvento;
        delayEvento = duracionEvento * 0.33f;
        CambiarEvento();
    }

    // Update is called once per frame
    void Update()
    {
        temporizador -= Time.deltaTime;
        if(temporizador <= 0)
        {
            CambiarEvento();
            temporizador = duracionEvento;
            delayEvento = duracionEvento * 0.33f;
        }
    }

    private void CambiarEvento()
    {        
        numEvento++;

        if (numEvento % frecuenciaAparicionPolilla == 0)
        {
            MothSpawner.Instance.SpawnMoth();
        }

        duracionEvento *= (1 - porcentajeAumentoFrecuenciaPorEvento * 0.01f);
        if (eventoActual == 0)
        {
            print(RainScript.Instance.name);
            RainScript.Instance.StopSpawningRaing();
        }
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
