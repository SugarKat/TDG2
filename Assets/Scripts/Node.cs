using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject building;
    [HideInInspector]
    public BuildingBlueprint buildingBlueprint;
    [HideInInspector]

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (building == null)
        {
            BuildBuilding(buildManager.GetBuildingToBuild(), buildManager.GetBuildingRotation());                 
        }
    }
    void BuildBuilding (BuildingBlueprint blueprint,Quaternion _rot)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not Enough Money To Build");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _building = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), _rot);
        building = _building;

        AstarPath.active.UpdateGraphs(building.GetComponent<Collider>().bounds);

        buildingBlueprint = blueprint;

        if(buildManager.buildEffect != null)
        {
            GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), _rot);
            Destroy(effect, 5f);
        }

        BuildingsHealth bHealth = building.GetComponent<BuildingsHealth>();
        bHealth.Destruction += ClearBuildingData;
    }
    void ClearBuildingData()
    {
        buildingBlueprint = null;

    }
    public void SellBuilding ()
    {
        PlayerStats.Money += buildingBlueprint.sellWorth;

        Destroy(building);
        ClearBuildingData();
    }
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (building != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoneyToBuild)
        {
            rend.material.color = hoverColor;
            buildManager.SelectNode(this);
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
            buildManager.SelectNode(this);
        }
        
    }
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
