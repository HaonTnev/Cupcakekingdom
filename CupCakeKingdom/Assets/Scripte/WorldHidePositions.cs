using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WorldHidePositions
{
    private static readonly WorldHidePositions instance = new WorldHidePositions();
    private static GameObject[] hidingSpots;

    static WorldHidePositions()
    {
        hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }

    private WorldHidePositions() {}

    public static WorldHidePositions Instance
    {
        get { return instance; }
    }

    public GameObject[] GetHidingSpots()
    {
        return hidingSpots;
    }
}
