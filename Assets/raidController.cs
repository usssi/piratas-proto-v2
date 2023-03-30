using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class raidController : MonoBehaviour
{

    private parametrics parameters;
    private enemyShipsController enemyShipController;
    private crewController crewController;
    private shipManager shipManager;
    private resourceController resourceController;

    public Slider foodSlider;
    public Slider materialSlider;
    public TextMeshProUGUI foodBoostText;
    public TextMeshProUGUI materialBoostText;

    public TextMeshProUGUI victoryChance;
    public float winningPercentaje;

    public float pesoCrew;
    public float pesoIQ;
    public float pesoSt;

    public string attackMode;

    public float tuAVG;
    public float enemyAVG;

    public TextMeshProUGUI rtesultado;



    private void Start()
    {
        parameters = FindObjectOfType<parametrics>();
        enemyShipController = FindObjectOfType<enemyShipsController>();
        crewController = FindObjectOfType<crewController>();
        shipManager = FindObjectOfType<shipManager>();
        resourceController = FindObjectOfType<resourceController>();

        ActionNormalAttack();
    }

    private void Update()
    {
        foodSlider.maxValue = resourceController.currentFood;
        materialSlider.maxValue = resourceController.currentMaterials;

        foodBoostText.text = "-¢" + foodSlider.value;
        materialBoostText.text = "-¥" + materialSlider.value;


        ChanceWinningCalculation();
        victoryChance.text = "%" + winningPercentaje.ToString("F0");
    }

      
    public void ActionNormalAttack()
    {
        pesoCrew = 33.3f;
        pesoIQ = 33.3f;
        pesoSt = 33.3f;
        attackMode = "normal";
    }

    public void ActionIQAttack()
    {
        pesoCrew = 20;
        pesoIQ = 60;
        pesoSt = 20;
        attackMode = "iq";
    }

    public void ActionSTAttack()
    {
        pesoCrew = 20;
        pesoIQ = 20;
        pesoSt = 60;
        attackMode = "st";
    }

    public void ActionGitanoAttack()
    {
        pesoCrew = 60;
        pesoIQ = 20;
        pesoSt = 20;
        attackMode = "gitana";
    }

    void ChanceWinningCalculation()
    {
        if (enemyShipController.enemyShip != null)
        {
            int tuCrew = parameters.currentCrewmates;
            int tuIq = crewController.totalIq;
            int tuST = crewController.totalSt;

            int enemyCrew = enemyShipController.enemyShip.crewCount;
            int enemyIQ = enemyShipController.enemyShip.crewIq;
            int enemyST = enemyShipController.enemyShip.crewSt;

            tuAVG = ((tuCrew * pesoCrew) + (tuIq * pesoIQ) + (tuST * pesoSt)) / (pesoCrew + pesoIQ + pesoSt);

            if (enemyShipController.enemyShip.shipType == "Comercial")
            {
                enemyAVG = ((enemyCrew * 20) + (enemyIQ * 60) + (enemyST * 20)) / (20 + 60 + 20);

            }
            else if (enemyShipController.enemyShip.shipType == "Pirata")
            {
                enemyAVG = ((enemyCrew * 60) + (enemyIQ * 20) + (enemyST * 20)) / (60 + 20 + 20);

            }
            else if (enemyShipController.enemyShip.shipType == "Militar")
            {
                enemyAVG = ((enemyCrew * 20) + (enemyIQ * 20) + (enemyST * 60)) / (20 + 20 + 60);

            }

            winningPercentaje = ((tuAVG * 100) / (tuAVG + enemyAVG));

            winningPercentaje += (shipManager.shipCondition - enemyShipController.enemyShip.shipCondition)/2;

            winningPercentaje += (foodSlider.value + materialSlider.value) / 2;
        }

    }


    public void ActionAtaqueFinal()
    {
        int result = Random.Range(0, 100);

        resourceController.currentFood -= (int)foodSlider.value;
        resourceController.currentMaterials-= (int)materialSlider.value;


        if (result<= winningPercentaje)
        {
            resourceController.currentGold += enemyShipController.enemyShip.resourceGold;
            resourceController.currentFood += enemyShipController.enemyShip.resourceFood;
            resourceController.currentMaterials += enemyShipController.enemyShip.resourceMaterial;

            rtesultado.text = "ganaste!: " + enemyShipController.enemyShip.resourceGold + " de oro. " + enemyShipController.enemyShip.resourceFood
                + " de comida. y " + enemyShipController.enemyShip.resourceMaterial + " de material. ";

            enemyShipController.enemyShip = null;
            enemyShipController.ActualizarAllShips();
        }
        else
        {
            resourceController.currentGold -= enemyShipController.enemyShip.resourceGold/2;
            resourceController.currentFood -= enemyShipController.enemyShip.resourceFood/2;
            resourceController.currentMaterials -= enemyShipController.enemyShip.resourceMaterial/2;

            rtesultado.text = "perdiste!: " + enemyShipController.enemyShip.resourceGold/2 + " de oro. " + enemyShipController.enemyShip.resourceFood/2
                + " de comida. y " + enemyShipController.enemyShip.resourceMaterial/2 + " de material. ";

            enemyShipController.enemyShip = null;
            enemyShipController.ActualizarAllShips();   
        }

    }
}
