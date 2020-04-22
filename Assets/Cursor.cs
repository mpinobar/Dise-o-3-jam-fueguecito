using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Cursor : MonoBehaviour
{
    public enum Cuadrante
    {
        TopRight, TopLeft, BottomRight, BottomLeft
    }

    List<Cuadrante> cuadrantes;

    private Cuadrante cursorCuadranteActual = Cuadrante.TopRight;

    // Start is called before the first frame update
    void Start()
    {
        cuadrantes = new List<Cuadrante>();
    }

    // Update is called once per frame
    void Update()
    {
        cursorCuadranteActual = ComprobarCuadrante();
        ComprobarCirculoHecho();
        ComprobarCambioDeCuadrante();
        

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
                    print("Se ha dado una vuelta completa");
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

    private Cuadrante ComprobarCuadrante()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (pos.x >= 0 && pos.y >= 0)
        {
            return Cuadrante.TopRight;
        }
        else if (pos.x >= 0 && pos.y < 0)
        {
            return Cuadrante.BottomRight;
        }
        else if (pos.x < 0 && pos.y >= 0)
        {
            return Cuadrante.TopLeft;
        }
        else
        {
            return Cuadrante.BottomLeft;
        }
    }
}
