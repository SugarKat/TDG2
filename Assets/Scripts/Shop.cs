using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public GameObject Turret;

    void Start ()
    {
        buildManager = BuildManager.instance;
    }

}
