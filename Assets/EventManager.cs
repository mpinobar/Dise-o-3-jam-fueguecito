using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : TemporalSingleton<EventManager>
{
    [SerializeField] float duracionEvento = 3;
    [SerializeField] int frecuenciaAparicionPolilla = 2;
    [SerializeField] float decrementoDuracionPorCadaEvento = 0.2f;
    int numEvento;
    int eventoActual;

    float temporizador;


    // Start is called before the first frame update
    void Start()
    {
        temporizador = duracionEvento;
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
        }
    }

    private void CambiarEvento()
    {
        
        numEvento++;
        if (numEvento % frecuenciaAparicionPolilla == 0)
        {
            MothSpawner.Instance.SpawnMoth();
        }
        duracionEvento -= decrementoDuracionPorCadaEvento;
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
                RainScript.Instance.SpawnRain();
                break;
            case 1:
                FingerSpawner.Instance.SpawnFinger(numEvento);
                break;
            case 2:
                Spawner.Instance.SpawnWind();
                break;
        }
    }
}
