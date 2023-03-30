using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class crewController : MonoBehaviour
{
    public GameObject[] recruitablesList;
    public GameObject[] crewmatesList;

    public parametrics parametrico;
    public resourceController recursos;

    public TextMeshProUGUI precioReloadSearchReclutas;
    public TextMeshProUGUI buttonCrewText;
    public TextMeshProUGUI textoStTotal;
    public TextMeshProUGUI textoIqTotal;


    public crewmember temporalCrewmate;

    private int indexdeCrewmate = 0;

    public int[] arrayDeIndexadosBorrados;
    public List<int> listaDeIndexadosBorrados;
    public int indexDeAgregarBorrados= 0;

    public int totalIq;
    public int totalSt;



    private void Start()
    {
        recruitablesList = GameObject.FindGameObjectsWithTag("recruitable");

        crewmatesList = GameObject.FindGameObjectsWithTag("crewmate");

        //Invoke("printnames", .1f);
        EnterREclutarHideMoreThanMaxCrew();

        EsconderCrewmatesSinStats();

        listaDeIndexadosBorrados = new List<int>();

    }

    private void Update()
    {
        buttonCrewText.text = parametrico.currentCrewmates + "/" + parametrico.maxCrewmates;

        //CalcularIQYSTTotal();
        CalcularIQYSTTotal();

        //update texto reload recruitables price
        precioReloadSearchReclutas.text = "$" + parametrico.precioReloadSearch;
        if (recursos.currentGold >= parametrico.precioReloadSearch)
        {
            precioReloadSearchReclutas.color = Color.white;
        }
        else
        {
            precioReloadSearchReclutas.color = Color.red;
        }
    }


    public void ActionReclutarReclutas()
    {
        if (listaDeIndexadosBorrados.Count > 0)
        {
            ChangeNameIfSameAsAlreadyRecruited();

            crewmatesList[arrayDeIndexadosBorrados[0]].SetActive(true);
            crewmatesList[arrayDeIndexadosBorrados[0]].GetComponent<displayCrewmate>().crewmate = temporalCrewmate;

            crewmatesList[arrayDeIndexadosBorrados[0]].GetComponent<displayCrewmate>().UpdateInfo();

            listaDeIndexadosBorrados.RemoveAt(0);
            listaDeIndexadosBorrados.Sort();

            arrayDeIndexadosBorrados = listaDeIndexadosBorrados.ToArray();

            parametrico.currentCrewmates++;
        }
        else
        {
            ChangeNameIfSameAsAlreadyRecruited();

            crewmatesList[indexdeCrewmate].SetActive(true);
            crewmatesList[indexdeCrewmate].GetComponent<displayCrewmate>().crewmate = temporalCrewmate;

            crewmatesList[indexdeCrewmate].GetComponent<displayCrewmate>().UpdateInfo();

            indexdeCrewmate++;

            parametrico.currentCrewmates++;
        }

    }

    public void ActionDescartarCrewmatesReclutados(GameObject crewmateHeredado)
    {
        for (int i = 0; i < crewmatesList.Length; i++)
        {
            if (crewmatesList[i].GetComponent<displayCrewmate>().crewmate != null)
            {
                if (crewmatesList[i].GetComponent<displayCrewmate>().crewmate.crewName == crewmateHeredado.GetComponent<displayCrewmate>().crewmate.crewName)
                {
                    listaDeIndexadosBorrados.Add(i);
                    listaDeIndexadosBorrados.Sort();
                    arrayDeIndexadosBorrados = listaDeIndexadosBorrados.ToArray();

                }
            }
        }
        parametrico.currentCrewmates--;
    }

    public void EsconderCrewmatesSinStats()
    {
        foreach (var item in crewmatesList)
        {
            if (item.gameObject.GetComponent<displayCrewmate>().crewmate == null)
            {
                //Debug.Log(item.gameObject.name + " null crewmate");
                item.gameObject.SetActive(false);
            }
        }
    }

    public void buscarNuevosReclutas(bool paycheck)
    {
        if (paycheck)//si es boton de pago
        {
            if (recursos.currentGold >= parametrico.precioReloadSearch)//si te alcanza el upgrade
            {
                for (int i = 0; i < recruitablesList.Length; i++)
                {
                    recruitablesList[i].gameObject.GetComponent<displayRecruitable>().generateCrewmamber();
                }
                EnterREclutarHideMoreThanMaxCrew();
                recursos.currentGold -= parametrico.precioReloadSearch;

                resizeRecruiters();
            }
            else
            {
                Debug.Log("te falta oro");
            }
        }
        else
        {
            for (int i = 0; i < recruitablesList.Length; i++)
            {
                recruitablesList[i].gameObject.GetComponent<displayRecruitable>().generateCrewmamber();
            }
            EnterREclutarHideMoreThanMaxCrew();

            resizeRecruiters();
        }
    }

    public void EnterREclutarHideMoreThanMaxCrew()
    {
        for (int i = 0; i < recruitablesList.Length; i++)
        {
            recruitablesList[i].SetActive(true);

            if (i > parametrico.maxCrewmates)
            {
                recruitablesList[i].SetActive(false);
            }
        }
    }

    void ChangeNameIfSameAsAlreadyRecruited()
    {
        for (int i = 0; i < crewmatesList.Length; i++)
        {
            if (crewmatesList[i].GetComponent<displayCrewmate>().crewmate != null)
            {
                if (temporalCrewmate.crewName == crewmatesList[i].GetComponent<displayCrewmate>().crewmate.crewName)
                {
                    temporalCrewmate.crewName = temporalCrewmate.crewName + " jr.";
                }
            }
        }
    }

    void resizeRecruiters()
    {
        foreach (var item in recruitablesList)
        {
            if (item.transform.localScale == Vector3.zero)
            {
                item.transform.localScale = Vector3.one;
            }
        }
    }

    void CalcularIQYSTTotal()
    {
        int calculatedIq = 0;
        int calculatedSt = 0;


        for (int i = 0; i < crewmatesList.Length; i++)
        {
            if (crewmatesList[i].GetComponent<displayCrewmate>().crewmate != null)
            {
                calculatedIq += crewmatesList[i].GetComponent<displayCrewmate>().crewmate.inteligencia;
            }
        }

        for (int i = 0; i < crewmatesList.Length; i++)
        {
            if (crewmatesList[i].GetComponent<displayCrewmate>().crewmate != null)
            {
                calculatedSt += crewmatesList[i].GetComponent<displayCrewmate>().crewmate.fuerza;
            }
        }

        if (calculatedIq>totalIq)
        {
            totalIq = calculatedIq;
        }
        else
        {
            totalIq = calculatedIq;
        }

        if (calculatedSt > totalSt)
        {
            totalSt = calculatedSt;
        }
        else
        {
            totalSt = calculatedSt;
        }

        textoIqTotal.text = totalIq.ToString();
        textoStTotal.text = totalSt.ToString();

    }


}
