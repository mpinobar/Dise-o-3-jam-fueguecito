using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Cursor : MonoBehaviour
{
    public Transform vela;
    public Vector3 velocity;
    private Vector3 lastPosition;
    private float inverseTime;
    public static Moth grabbedMoth;
    public enum Cuadrante
    {
        TopRight, TopLeft, BottomRight, BottomLeft
    }

    List<Cuadrante> cuadrantes;

    private Cuadrante cursorCuadranteActual = Cuadrante.TopRight;

    // Start is called before the first frame update
    void Start()
    {
        inverseTime = 1 / Time.deltaTime;   
        cuadrantes = new List<Cuadrante>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        velocity = transform.position - lastPosition;
        velocity *= inverseTime;
        cursorCuadranteActual = ComprobarCuadrante();
        ComprobarCirculoHecho();
        ComprobarCambioDeCuadrante();        
        lastPosition = transform.position;
    }

    private void ComprobarCambioDeCuadrante()
    {        
        if (cuadrantes.Count == 0)
        {
            cuadrantes.Add(cursorCuadranteActual);
            return;
        }
        if (cursorCuadranteActual != cuadrantes[cuadrantes.Count - 1])
        {
            cuadrantes.Add(cursorCuadranteActual);            
            if (cuadrantes.Count > 4)
            {
                cuadrantes.RemoveAt(0);
            }                      
        }
    }

    private bool ComprobarCirculoHecho()
    {
        if(cuadrantes.Count == 4)
        {
            //si el cuadrante en el que estoy es el mismo que el de hace
            if (cursorCuadranteActual == cuadrantes[0])
            {
                bool hayRepetidos = false;
                //por cada uno de los cuadrantes disponibles
                for (int i = 0; i < 4; i++)
                {
                    Cuadrante cuadranteAComprobar = (Cuadrante)i;
                    int numCuadrantes = 0;

                    //compruebo que no haya repetidos
                    for (int j = 0; j < cuadrantes.Count; j++)
                    {
                        if (cuadranteAComprobar == cuadrantes[j])
                            numCuadrantes++;
                    }
                    if (numCuadrantes != 1)
                    {
                        hayRepetidos = true;
                    }
                }
                if (!hayRepetidos)
                {
                    //Se ha dado una vuelta completa
                    OnCircleCompleted();
                    cuadrantes.Clear();
                    return true;
                }
                
            }
            return false;
        }
        else
        {
            return false;
        }
        

    }
    private void OnCircleCompleted()
    {
		ProtectionFromRain.Instance.HealProtection();
        GetComponent<AudioSource>().Play();
    }

    private Cuadrante ComprobarCuadrante()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (pos.x >= vela.position.x && pos.y >= vela.position.y)
        {
            return Cuadrante.TopRight;
        }
        else if (pos.x >= vela.position.x && pos.y < vela.position.y)
        {
            return Cuadrante.BottomRight;
        }
        else if (pos.x < vela.position.x && pos.y >= vela.position.y)
        {
            return Cuadrante.TopLeft;
        }
        else
        {
            return Cuadrante.BottomLeft;
        }
    }
}
