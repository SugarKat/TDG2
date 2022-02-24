using UnityEngine;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour
{
    List<BuildingsHealth> activeBuildings;

    public static BuildManager instance;
    public GameObject buildEffect;

    private BuildingBlueprint buildingToBuild;
    [HideInInspector]
    public Node activeNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return buildingToBuild != null; } }
    public bool HasMoneyToBuild { get { return PlayerStats.Money >= buildingToBuild.cost; } }

    GameObject previewObject;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
        activeBuildings = new List<BuildingsHealth>();
    }
    private void Start()
    {
        PlayerInput.instance.RotateB += RotateBuilding;
    }
    private void Update()
    {
        if(buildingToBuild == null)
        {
            Destroy(previewObject);
        }
        if(previewObject != null)
        {
            previewObject.transform.position = activeNode.GetBuildPosition();
        }
    }
    public void SelectNode (Node node)
    {
        activeNode = node;

        if(previewObject == null && buildingToBuild != null)
        {
            previewObject = Instantiate(buildingToBuild.preview);
        }
    }

    public void SelectBuildingToBuild(BuildingBlueprint building)
    {
        Destroy(previewObject);
        buildingToBuild = building;
    }
    public Quaternion GetBuildingRotation()
    {
        return previewObject.transform.rotation;
    }
    public BuildingBlueprint GetBuildingToBuild ()
    {
        return buildingToBuild;
    }
    void RotateBuilding()
    {
        if(previewObject != null)
        {
            previewObject.transform.Rotate(Vector3.up, 90);
        }
    }
    public void AddActiveBuilding(BuildingsHealth building)
    {
        activeBuildings.Add(building);
    }
    public void RemoveBuilding(BuildingsHealth building)
    {
        activeBuildings.Remove(building);
    }
    public BuildingsHealth GetRandomActiveBuilding()
    {
        if(activeBuildings.Count <= 0)
        {
            return null;
        }
        return activeBuildings[Random.Range(0, activeBuildings.Count)];
    }
}
