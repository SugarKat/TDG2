using UnityEngine;
using System.Collections;

[System.Serializable]
public class BuildingBlueprint
{
    public string name;
    public GameObject prefab;
    public GameObject preview;
    public Sprite image;
    public int cost;

    public int sellWorth;

    public int GetSellAmount ()
    {
        return sellWorth;
    }
}
