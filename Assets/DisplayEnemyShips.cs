using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class DisplayEnemyShips : MonoBehaviour
{

    public enemyShip shipInfo;
    public parametrics parameters;
    public enemyShipsController enemyShipController;

    public TextMeshProUGUI textName;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textResources;

    private void Awake()
    {
        parameters = FindObjectOfType<parametrics>();

    }
    private void Start()
    {
        enemyShipController = FindObjectOfType<enemyShipsController>();
        GenerateEnemyShip();
    }

    private void Update()
    {

    }

    public void GenerateEnemyShip()
    {
        shipInfo = ScriptableObject.CreateInstance("enemyShip") as enemyShip;

        DetermineShipName();
        DetermineShipType();
        DetermineShipLevel();
        DetermineShipCondition();

        DetermineShipsResources();
        DetermineCrewStats();

        //textName.text = "perla negra";

        textName.text = shipInfo.shipName;
        textType.text = "Barco " + shipInfo.shipType;
        textLevel.text = "Lvl: " + shipInfo.shipLevel.ToString();
        textResources.text = "$" + shipInfo.resourceGold + " | " + "¢" + shipInfo.resourceFood + " | " + "¥" + shipInfo.resourceMaterial;
    }
    void DetermineShipName()
    {
        string[] shipNamesRandom = new string[]{
            "Inferno", "Dying Gull","Whydah","Perla Negra",
            "Squirrel","Rose Pink", "Royal Fortune", "uss Navy", 
            "Queen Anne’s Revenge", "Fancy", "Amity", "Adventure Galley"};

        int iR = Random.Range(0, shipNamesRandom.Length);
        shipInfo.shipName = shipNamesRandom[iR];
    }
    void DetermineShipType()
    {
        int type;
        type = Random.Range(0, 3);

        if (type == 0)
        {
            shipInfo.shipType = "Comercial";
        }
        else if (type ==1)
        {
            shipInfo.shipType = "Pirata";

        }
        else if (type == 2)
        {
            shipInfo.shipType = "Militar";

        }
        else
        {
            Debug.Log("type: " + type);
        }
    }
    void DetermineShipLevel()
    {
        int iR = Random.Range(-3, 3);

        iR += parameters.currentShipLevel;

        if (iR<=0)
        {
            iR = 1;
        }

        shipInfo.shipLevel = iR;
    }
    void DetermineShipCondition()
    {
        int iR = (Random.Range(10 * (shipInfo.shipLevel), 100));

        shipInfo.shipCondition = iR;
    }
    void DetermineShipsResources()
    {
        if (shipInfo.shipType == "Comercial")
        {
            //mucha comida
            int iR = (Random.Range(1, shipInfo.shipCondition)) * (shipInfo.shipLevel);

            shipInfo.resourceFood = iR - (iR/3) + (iR + (shipInfo.shipCondition / 2));


            iR = (Random.Range(1, shipInfo.shipCondition)) * (shipInfo.shipLevel);

            shipInfo.resourceGold = iR;

            iR = (Random.Range(1, shipInfo.shipCondition)) * (shipInfo.shipLevel);

            shipInfo.resourceMaterial = iR;


        }
        else if (shipInfo.shipType == "Pirata")
        {
            //mucho oro
            int iR = (Random.Range(1, shipInfo.shipCondition)) * (shipInfo.shipLevel);

            shipInfo.resourceFood = iR;

            iR = (Random.Range(1, shipInfo.shipCondition)) * (shipInfo.shipLevel);

            shipInfo.resourceGold = iR - (iR / 3) + (iR + (shipInfo.shipCondition / 2));


            iR = (Random.Range(1, shipInfo.shipCondition)) * (shipInfo.shipLevel);

            shipInfo.resourceMaterial = iR;

        }
        else if (shipInfo.shipType == "Militar")
        {
            //muhco material
            int iR = (Random.Range(1, shipInfo.shipCondition)) * (shipInfo.shipLevel);

            shipInfo.resourceFood = iR;
            //shipInfo.resourceFood -= 100 - shipInfo.shipCondition;
            iR = (Random.Range(1, shipInfo.shipCondition)) * (shipInfo.shipLevel);

            shipInfo.resourceGold = iR;

            iR = (Random.Range(1, shipInfo.shipCondition)) * (shipInfo.shipLevel);

            shipInfo.resourceMaterial = iR - (iR / 3) + (iR + (shipInfo.shipCondition/2));

        }
    }
    void DetermineCrewStats()
    {
        if (shipInfo.shipType == "Comercial")
        {
            //poca fuerza

            int iR = parameters.currentCrewmates + Random.Range(-2, shipInfo.shipLevel+2);
            if (iR < 1)
            {
                iR = 1;
            }
            shipInfo.crewCount = iR;

            iR = Random.Range(1, (shipInfo.shipLevel + Random.Range(0, 10)));

            shipInfo.crewIq = iR + (shipInfo.crewCount * 2);

            iR = Random.Range(1, (shipInfo.shipLevel + Random.Range(0, 10)));
            
            shipInfo.crewSt = iR + (shipInfo.crewCount);


        }
        else if (shipInfo.shipType == "Pirata")
        {
            //poca inteligencia
            int iR = parameters.currentCrewmates + Random.Range(-2, shipInfo.shipLevel + 2);
            if (iR < 1)
            {
                iR = 1;
            }
            shipInfo.crewCount = iR;

            iR = Random.Range(1, (shipInfo.shipLevel + Random.Range(0, 10)));

            shipInfo.crewIq = iR + (shipInfo.crewCount);

            iR = Random.Range(1, (shipInfo.shipLevel + Random.Range(0, 10)));

            shipInfo.crewSt = iR + (shipInfo.crewCount * 2);


        }
        else if (shipInfo.shipType == "Militar")
        {
            //poca cantidad
            int iR = parameters.currentCrewmates + Random.Range(-2, shipInfo.shipLevel);
            if (iR<1)
            {
                iR = 1;
            }

            shipInfo.crewCount = iR;

            iR = Random.Range(1, (shipInfo.shipLevel + Random.Range(0, 10)));

            shipInfo.crewIq = iR + (shipInfo.crewCount * 2);

            iR = Random.Range(1, (shipInfo.shipLevel + Random.Range(0, 10)));

            shipInfo.crewSt = iR + (shipInfo.crewCount * 2);

        }
    }

    public void AccionPlanearAtaqueThisShip() 
    {
        //gameObject.transform.localScale = Vector3.zero;

        enemyShipController.enemyShip = shipInfo;

        enemyShipController.ActionPlanearAtaque();


        //Debug.Log("name: " + shipInfo.shipName);
        //Debug.Log("type: " + shipInfo.shipType);
        //Debug.Log("level: " + shipInfo.shipLevel);
        //Debug.Log("condition: " + shipInfo.shipCondition);
        //Debug.Log("");
        //Debug.Log("gold: " + shipInfo.resourceGold);
        //Debug.Log("food: " + shipInfo.resourceFood);
        //Debug.Log("material: " + shipInfo.resourceMaterial);
        //Debug.Log("");
        //Debug.Log("crew cant: " + shipInfo.crewCount);
        //Debug.Log("crew iq: " + shipInfo.crewIq);
        //Debug.Log("crew st: " + shipInfo.crewSt);

    }
}
