using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class resourceController : MonoBehaviour
{
    [Space]
    public parametrics parametrics;
    [Space]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI materialText;

    [Space]
    public int currentGold;
    private int maxGold;

    public int currentFood;
    private int maxFood;
        
    public int currentMaterials;
    private int maxMaterials;

    private void Start()
    {
        currentGold = parametrics.startingGold;
        currentFood = parametrics.startingFood;
        currentMaterials = parametrics.startingMaterials;


    }

    private void Update()
    {
        //update maxcap
        maxGold = parametrics.maxGold;
        maxFood = parametrics.maxFood;
        maxMaterials = parametrics.maxMaterials;

        //update texto en displays resources

        goldText.text = "$" + currentGold + " / " + maxGold;
        foodText.text = "¢" + currentFood + " / " + maxFood;
        materialText.text = "¥" + currentMaterials + " / " + maxMaterials;

        //aumentar resources cheatcode

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentGold += 1000;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentFood += 1000;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentMaterials += 1000;
        }

        //disminuir resources cheatcode

        if (Input.GetKeyDown(KeyCode.Z ))
        {
            currentGold -= 1000;
        }
        if (Input.GetKeyDown(KeyCode.X ))
        {
            currentFood -= 1000;
        }
        if (Input.GetKeyDown(KeyCode.C ))
        {
            currentMaterials -= 1000;
        }

        //hardcap 0
        if (currentGold<=0)
        {
            currentGold = 0;
        }
        if (currentFood <= 0)
        {
            currentFood = 0;
        }
        if (currentMaterials <= 0)
        {
            currentMaterials = 0;
        }

        //harcap at max capacity

        if (currentGold >= maxGold)
        {
            currentGold = maxGold;
        }
        if (currentFood >= maxFood)
        {
            currentFood = maxFood;
        }
        if (currentMaterials >= maxMaterials)
        {
            currentMaterials = maxMaterials;
        }

    }
}
