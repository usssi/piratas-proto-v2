using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class parametrics : MonoBehaviour
{
    [Header("RESOURCES STUFF")]
    [Space]
    public int startingGold;
    public int maxGold;

    [Space]
    public int startingFood;
    public int maxFood;

    [Space]
    public int startingMaterials;
    public int maxMaterials;

    [Space]
    [Header("SHIP STUFF")]
    [Space]

    //[HideInInspector]
    public int startingShiplvl = 0;

    public int currentShipLevel;
    public int maxShipLevel;
    public int maxShipLevelForCrewmatePlus;

    public int startingShipCost;

    [Space]
    [Header("CREWMATE STUFF")]
    public string[] pirateNames;
    [Space]
    public int currentCrewmates;
    public int maxCrewmates;
    public int maxCrewmateLvl;
    public int precioReloadSearch;

    [Space]
    [Header("random")]
    public bool DeleteNamesFromPool;


    private void Awake()
    {
        pirateNames = new string[] { "BlackbeardJack", "CaptainLiam", "PirateJane", "SeaWolfZak", "CorsairJax", "FearsomeFinn", "OceanRogue",
            "DreadPirate", "JollyRogerJ", "SeaThiefKai", "TreasureHunt", "PirateKingJ", "OceanAssassin",
            "SeaReaperSam", "PirateQueenB", "NauticalNick", "MaritimeMike", "SirenSongRae", "TheTriumph",
            "CrimsonJack", "Blackbeard", "HookHand", "IronFist", "Redbeard", "Silverhook", "BlackJack",
            "OneEyedJack", "DeadEyeDave", "Rustbeard", "WhiskerJim", "LongJohn", "Sharkbait", "Saltydog",
            "SeaDevil", "Stormchaser", "TattooJack", "WildcardJ", "SirenSong", "GhostPirate", "Blackskull",
            "Silverblade", "WanderingJack", "Nightstalker", "PirateKing", "OceanRider", "RumRunner",
            "TheCaptain", "MarauderMike", "SeaWitch", "PirateLord", "TreasureSeeker", "ThePlunderer",
            "DreadPirate", "IslandHopper", "SavageSailor", "TheBuccaneer", "SeaDragon", "OceanRanger",
            "SeaRogue", "PiratePrince", "TheRaider", "SeaAssassin", "ussi" };
    }
}
