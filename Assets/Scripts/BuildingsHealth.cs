using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsHealth : MonoBehaviour
{
    public delegate void OnDestruction();
    public event OnDestruction Destruction;

    public int startingHealth = 20;
    int currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
        BuildManager.instance.AddActiveBuilding(this);
    }
    public void TakeDamage(int damage)
    {
        Debug.LogWarning("Taking damage");
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destruction?.Invoke();
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        AstarPath.active.UpdateGraphs(GetComponent<Collider>().bounds);
        BuildManager.instance.RemoveBuilding(this);
    }
}
