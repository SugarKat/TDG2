using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;

    public delegate void BuildMenu();
    public event BuildMenu BMenu;
    public delegate void RotateBuilding();
    public event RotateBuilding RotateB;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            BMenu?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            RotateB?.Invoke();
        }
    }
}
