using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TurnController
{
    // Start is called before the first frame update
    static List<UnitController> units = new List<UnitController>();
    static Queue<UnitController> unitOrder = new Queue<UnitController>();

    public static void SubscribeUnit(UnitController unit)
    {
        units.Add(unit);
    }

    public static void UnsubscribeUnit(UnitController unit)
    {
        units.Remove(unit);
    }

    public static void ShuffleOrder()
    {
        List<int> unitsValues = new List<int>();
        for (int i = 0; i < units.Count; i++)
        {
            unitsValues.Add(i);
        }

        for (int i = 0; i < unitsValues.Count; i++)
        {
            int next = Random.Range(0, unitsValues.Count);
            int nextUnitControllerIndex = unitsValues[next];
            UnitController nextUnitController = units[nextUnitControllerIndex];
            unitOrder.Enqueue(nextUnitController);
            unitsValues.RemoveAt(next);
        }
    }

    public static UnitController NextUnit()
    {
        UnitController next = unitOrder.Dequeue();
        unitOrder.Enqueue(next);

        return next;
    }
}
