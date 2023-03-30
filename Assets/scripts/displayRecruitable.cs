using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class displayRecruitable : MonoBehaviour
{
    public crewmember crewmate;
    public parametrics parameters;
    public crewController crewController;
    public resourceController recursos;

    public TextMeshProUGUI nombreText;
    public TextMeshProUGUI iqText;
    public TextMeshProUGUI stText;
    public TextMeshProUGUI precioText;

    private float calculoPrecio;

    private void Start()
    {
        generateCrewmamber();
    }

    public void generateCrewmamber()
    {
        crewmate = ScriptableObject.CreateInstance("crewmember") as crewmember;

        randomNameGenerator(false);
        iqCalculator();
        stCalculator();
        priceCalculator();

        nombreText.text = crewmate.crewName;
        iqText.text = crewmate.inteligencia.ToString();
        stText.text = crewmate.fuerza.ToString();
        precioText.text = "$" + crewmate.precio.ToString() + " / ¢" + crewmate.precio.ToString();
    }

    public void ActionReclutar()
    {
        //si tiene espacio
        if (parameters.currentCrewmates<parameters.maxCrewmates)
        {
            //si tiene dinero
            if (recursos.currentGold>= crewmate.precio && recursos.currentFood>= crewmate.precio)
            {
                gameObject.transform.localScale = Vector3.zero;
                crewController.temporalCrewmate = crewmate;
                crewController.ActionReclutarReclutas();

                recursos.currentFood -= crewmate.precio;
                recursos.currentGold -= crewmate.precio;

                if (parameters.DeleteNamesFromPool)
                {
                    for (int i = 0; i < parameters.pirateNames.Length; i++)
                    {
                        if (parameters.pirateNames[i] == crewmate.crewName)
                        {
                            RemoveElement(ref parameters.pirateNames, i);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("necesitas "+ crewmate.precio + " de oro y comida");
            }
        }
        else
        {
            Debug.Log("no te queda espacio para contratar");
        }
    }

    void randomNameGenerator(bool borrarDeLista)
    {
        int iR = Random.Range(0, parameters.pirateNames.Length);

        if (parameters.pirateNames[iR] == "ussi")
        {
            iR = Random.Range(0, parameters.pirateNames.Length);
            crewmate.crewName = parameters.pirateNames[iR];
        }
        else
        {
            crewmate.crewName = parameters.pirateNames[iR];
        }

        //borrar nombre de la lista para que no se repitan al generar nuevos
        if (borrarDeLista == true)
        {
            RemoveElement(ref parameters.pirateNames, iR);
        }
    }

    void iqCalculator()
    {
        if (crewmate.crewName == "ussi")
        {
            crewmate.inteligencia = (7 * (parameters.currentShipLevel + 1))/2;
        }
        else
        {
            int iR = (Random.Range(1, 10))*(parameters.currentShipLevel+1);

            crewmate.inteligencia = iR/2;
        }
    }

    void stCalculator()
    {
        if (crewmate.crewName == "ussi")
        {
            crewmate.fuerza = (8 * (parameters.currentShipLevel + 1))/2;
        }
        else
        {
            int iR = Random.Range(1, 10) * (parameters.currentShipLevel+1);

            crewmate.fuerza = iR / 2;
        }
    }

    void priceCalculator()
    {
        if (crewmate.crewName == "ussi")
        {
            crewmate.precio = 1;
        }
        else
        {
            CalculoPrecio();

            if (calculoPrecio == 0)
            {
                crewmate.precio = 1;
            }
            else
            {
                crewmate.precio = (int)calculoPrecio;
            }
        }
    }

    void CalculoPrecio()
    {
        calculoPrecio = (crewmate.inteligencia + 0.5f * crewmate.inteligencia) + (crewmate.fuerza + 0.5f * crewmate.fuerza);
    }

    private void RemoveElement<T>(ref T[] arr, int index)
    {
        for (int i = index; i < arr.Length - 1; i++)
        {
            arr[i] = arr[i + 1];
        }

        Array.Resize(ref arr, arr.Length - 1);
    }

}
