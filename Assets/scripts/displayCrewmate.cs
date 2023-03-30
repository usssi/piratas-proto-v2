using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class displayCrewmate : MonoBehaviour
{
    public crewmember crewmate;
    public parametrics parameters;
    public resourceController recursivos;
    public crewController crewcontroller;

    public TextMeshProUGUI nombreText;
    public TextMeshProUGUI iqText;
    public TextMeshProUGUI stText;

    public int costoMejora;

    private void Start()
    {
        UpdateInfo();

        crewcontroller = FindObjectOfType<crewController>();
    }

    public void UpdateInfo()
    {
        if (crewmate != null)
        {
            nombreText.text = crewmate.crewName;
            iqText.text = crewmate.inteligencia.ToString();
            stText.text = crewmate.fuerza.ToString();
        }
        else
        {
            nombreText.text = "nombre";
            iqText.text = "inteligencia";
            stText.text = "fuerza";
        }
    }

    public void ActionMejorarCrewmate()
    {
        CalcularCostoDeMejora();

        if (recursivos.currentGold >= costoMejora && recursivos.currentFood >= costoMejora)
        {
            crewmate.inteligencia++;
            crewmate.fuerza++;

            UpdateInfo();

            recursivos.currentFood -= costoMejora;
            recursivos.currentGold -= costoMejora;
        }
        else
        {
            Debug.Log("necestias: " + costoMejora + " $ y ¢");
        }
    }

    public void ActionDescartarCrewmate()
    {
        //gameObject.transform.localScale = Vector3.zero;

        crewcontroller.ActionDescartarCrewmatesReclutados(this.gameObject);

        crewmate = null;
        UpdateInfo();

        gameObject.SetActive(false);
    }

    public void CalcularCostoDeMejora()
    {
        costoMejora = ((crewmate.inteligencia+1) * (crewmate.fuerza+ 1)) / 4 ;
    }
}
