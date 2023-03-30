using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CrewMate", menuName = "CrewMate")]
public class crewmember : ScriptableObject
{
    public string crewName;
    public int inteligencia;
    public int fuerza;
    public int precio;
}
