using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControler : MonoBehaviour
{
    public GameObject buildMenu;
    public GameObject buildListItem;
    public Transform buildListParent;

    List<GameObject> buildList = new List<GameObject>();

    public static UIControler instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        buildMenu.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerInput.instance.BMenu += ToogleBuildMenu;
    }
    private void OnDisable()
    {
        PlayerInput.instance.BMenu -= ToogleBuildMenu;
    }
    void ToogleBuildMenu()
    {
        buildMenu.SetActive(!buildMenu.activeSelf);
        GameManager.instance.ToogleCursorLock();
    }
    public void UpdateBuildMenu(BuildingBlueprint[] list)
    {
        ClearBuildList();

        foreach (var item in list)
        {
            GameObject _buildListItemGO = Instantiate(buildListItem);
            _buildListItemGO.transform.SetParent(buildListParent);

            BuildItemList _buildListItem = _buildListItemGO.GetComponent<BuildItemList>();
            _buildListItem.Setup(item);

            buildList.Add(_buildListItemGO);
        }
    }
    void ClearBuildList()
    {
        for (int i = 0; i < buildList.Count; i++)
        {
            Destroy(buildList[i]);
        }
        buildList.Clear();
    }
}
