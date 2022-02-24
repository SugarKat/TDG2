using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControler))]
public class PlayerEntityManager : MonoBehaviour
{
    public static PlayerEntityManager instance;

    PlayerControler plC;

    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        plC = GetComponent<PlayerControler>();
    }

    public void ToogleControler()
    {
        plC.enabled = !plC.enabled;
    }
}
