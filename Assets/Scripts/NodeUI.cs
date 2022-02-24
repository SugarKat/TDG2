using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    public GameObject uI;

    public Text sellCost;

    private Node target;
    
    public void SetTarget (Node _target)
    {
        target = _target;
        Turret turret = target.building.GetComponent<Turret>();

        transform.position = target.GetBuildPosition();

        sellCost.text = "$" + target.buildingBlueprint.sellWorth;

        uI.SetActive(true);
    }
    
    public void Hide ()
    {
        uI.SetActive(false);
    }

    public void Sell ()
    {
        target.SellBuilding();
    }
}
