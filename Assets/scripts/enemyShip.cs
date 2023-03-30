using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new enemyShip", menuName = "enemyShip")]

public class enemyShip : ScriptableObject
{
    public string shipName;
    public string shipType;
    public int shipLevel;
    public int shipCondition;
    public int crewCount;
    public int crewIq;
    public int crewSt;
    public int resourceGold;
    public int resourceFood;
    public int resourceMaterial;
}
