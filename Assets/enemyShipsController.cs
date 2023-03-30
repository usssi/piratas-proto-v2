using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemyShipsController : MonoBehaviour
{
    public GameObject[] enemyShipArray;
    public parametrics parametros;

    public enemyShip enemyShip;

    public GameObject canvasAtackPlan;


    //planning stage info
    [Space]
    [Header("Planning Stage Info")]
    public TextMeshProUGUI shipNameText;
    public TextMeshProUGUI shipTypeText;
    public TextMeshProUGUI shipLevelText;
    public TextMeshProUGUI shipConditionText;

    public TextMeshProUGUI shipCrewCountText;
    public TextMeshProUGUI shipCrewIqext;
    public TextMeshProUGUI shipCrewStText;

    public TextMeshProUGUI shipGoldText;
    public TextMeshProUGUI shipFoodText;
    public TextMeshProUGUI shipMaterialText;

    private void Start()
    {
        parametros = FindObjectOfType<parametrics>();

        enemyShipArray = GameObject.FindGameObjectsWithTag("enemyship");

        HideMoreShipsThanMax();
    }

    public void ActionPlanearAtaque()
    {
        canvasAtackPlan.transform.localScale = Vector3.one;

        UpdatePlanningStageInfo();
    }


    void UpdatePlanningStageInfo()
    {
        shipNameText.text = enemyShip.shipName;
        shipTypeText.text = "Barco " + enemyShip.shipType;
        shipLevelText.text = "Lvl: " + enemyShip.shipLevel.ToString();
        shipConditionText.text = enemyShip.shipCondition.ToString() + "%";

        shipCrewCountText.text = enemyShip.crewCount.ToString();
        shipCrewIqext.text = enemyShip.crewIq.ToString();
        shipCrewStText.text = enemyShip.crewSt.ToString();

        shipGoldText.text = "$" + enemyShip.resourceGold.ToString();
        shipFoodText.text = "¢" + enemyShip.resourceFood.ToString();
        shipMaterialText.text = "¥" + enemyShip.resourceMaterial.ToString();
    }

    void HideMoreShipsThanMax()
    {
        for (int i = 0; i < enemyShipArray.Length; i++)
        {
            enemyShipArray[i].SetActive(true);

            if (i > parametros.maxCrewmates)
            {
                enemyShipArray[i].SetActive(false);
            }
        }
    }

    public void ActualizarAllShips()
    {
        foreach (var item in enemyShipArray)
        {
            item.GetComponent<DisplayEnemyShips>().GenerateEnemyShip();
        }
        HideMoreShipsThanMax();
    }

}

