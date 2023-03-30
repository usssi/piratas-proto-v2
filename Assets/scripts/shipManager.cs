using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class shipManager : MonoBehaviour
{
    [Space]
    public parametrics parametrics;
    public resourceController resources;
    public crewController crewControl;

    [Space]
    public TextMeshProUGUI buttonShipTxt;

    public TextMeshProUGUI shipLevelText;
    public TextMeshProUGUI costoShip;
    public TextMeshProUGUI beneficioShip;
    [Space]

    public int shipLevel;

    public int costoMejora;

    public int shipCondition;

    private void Awake()
    {
        shipLevel = parametrics.startingShiplvl;
        costoMejora = parametrics.startingShipCost;
    }

    private void Update()
    {
        //update texto en shipcanvas
        costoShip.text = "costo: $" + costoMejora + " & ¥" + costoMejora;

        shipLevelText.text = "SHIP LEVEL: " + (shipLevel+1).ToString();
        buttonShipTxt.text = "lvl: " + (shipLevel + 1).ToString();


        if (shipLevel >= parametrics.maxShipLevelForCrewmatePlus)
        {
            beneficioShip.text = "beneficio: +25% almacenamiento";
        }

        shipLevel = parametrics.currentShipLevel;

        if (resources.currentGold >= costoMejora && resources.currentMaterials >= costoMejora && shipLevel < parametrics.maxShipLevel)
        {
            costoShip.color = Color.white;
        }
        else
        {
            costoShip.color = Color.red;
        }
    }

    public void ActionUpgradeShip()
    {
        if (resources.currentGold >= costoMejora && resources.currentMaterials >= costoMejora && shipLevel < parametrics.maxShipLevel)
        {
            // si el nivel de ship es mayor al cap no mejora crewmate

            if (shipLevel < parametrics.maxShipLevelForCrewmatePlus)
            {
                shipLevel += 1;
                parametrics.currentShipLevel = shipLevel;

                resources.currentGold -= costoMejora;
                resources.currentMaterials -= costoMejora;

                costoMejora += (50 * shipLevel) / 2;

                parametrics.maxGold += parametrics.maxGold / 2;
                parametrics.maxFood += parametrics.maxFood / 2;
                parametrics.maxMaterials += parametrics.maxMaterials / 2;

                parametrics.maxCrewmates += 1;

            }
            else if (shipLevel >= parametrics.maxShipLevelForCrewmatePlus)
            {
                shipLevel += 1;
                parametrics.currentShipLevel = shipLevel;

                resources.currentGold -= costoMejora;
                resources.currentMaterials -= costoMejora;

                costoMejora += (50 * shipLevel);

                parametrics.maxGold += parametrics.maxGold / 4;
                parametrics.maxFood += parametrics.maxFood / 4;
                parametrics.maxMaterials += parametrics.maxMaterials / 4;

            }
            parametrics.precioReloadSearch += (5*shipLevel)/2;
        }
        else
        {
            Debug.Log("necesitas " + costoMejora + " de oro y material");
        }

    }
}
