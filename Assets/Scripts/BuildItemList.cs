using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildItemList : MonoBehaviour
{
    public new TextMeshProUGUI name;
    public TextMeshProUGUI price;
    public Image icon;

    BuildingBlueprint selection;

    public void Setup(BuildingBlueprint blueprint)
    {
        selection = blueprint;
        name.text = selection.name;
        icon.sprite = selection.image;
        price.text = selection.cost.ToString();
    }
    public void SelectBuilding()
    {
        BuildManager.instance.SelectBuildingToBuild(selection);
    }
}
