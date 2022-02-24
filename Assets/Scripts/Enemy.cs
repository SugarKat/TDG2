using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class Enemy : MonoBehaviour
{
    public int buildingsToDestroyToCalm = 5;
    public float timeTillRageMode = 10;
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float health = 100;
    public int strenght = 1;
    public int damage = 2;
    public int worth = 15;
    public float attackDistance = 1f;
    public float attackRate = .5f;

    AIDestinationSetter desSetter;
    Seeker seeker;
    AIPath controller;

    float attackCooldown;
    float rage;
    bool attackingBuilding = false;

    int buildingsToDestroy;
    BuildingsHealth buildingToDestroy;

    private void Awake()
    {
        attackCooldown = 0;
        desSetter = GetComponent<AIDestinationSetter>();
        seeker = GetComponent<Seeker>();
        controller = GetComponent<AIPath>();
    }
    void Start()
    {
        speed = startSpeed;
        SetTarget(GameManager.instance.EndPoint);
        seeker.pathCallback += PathsVerify;
        buildingsToDestroy = 5;
        rage = timeTillRageMode;
    }
    private void Update()
    {
        if(attackingBuilding)
        {
            if(buildingToDestroy == null)
            {
                SetTarget(GameManager.instance.EndPoint);
                rage = timeTillRageMode;
                attackingBuilding = false;
            }
            if(attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }
            else if (Vector3.Distance(transform.position,desSetter.target.position) < attackDistance)
            {
                buildingToDestroy.TakeDamage(damage);
                attackCooldown = 1f / attackRate;
            }            
            return;
        }
        rage -= Time.deltaTime;
        if(rage <= 0)
        {
            GetBuildingTarget();
        }
    }
    public void PathsVerify(Path path)
    {
        List<Vector3> points = path.vectorPath;
        if(attackingBuilding)
        {
            if (Vector3.Distance(points[points.Count - 1], desSetter.target.position) > attackDistance)
            {
                GetBuildingTarget();
            }
        }
        else if (Vector3.Distance(points[points.Count - 1], GameManager.instance.EndPoint.position) > attackDistance)
        {
            GetBuildingTarget();
        }
        else
        {
            buildingToDestroy = null;
            SetTarget(GameManager.instance.EndPoint);
            attackingBuilding = false;
        }
    }
    void GetBuildingTarget()
    {
        buildingToDestroy = BuildManager.instance.GetRandomActiveBuilding();
        if (buildingToDestroy == null)
        {
            SetTarget(GameManager.instance.EndPoint);
            rage = timeTillRageMode;
            attackingBuilding = false;
        }
        else
        {
            buildingToDestroy.Destruction += BuildingDestroyed;
            SetTarget(buildingToDestroy.transform);
            attackingBuilding = true;
        }
    }
    void SetTarget(Transform _target)
    {
        desSetter.target = _target;
    }
    void BuildingDestroyed()
    {
        buildingToDestroy.Destruction -= BuildingDestroyed;
        buildingsToDestroy--;
        switch (buildingsToDestroy)
        {
            case 0:
                SetTarget(GameManager.instance.EndPoint);
                rage = timeTillRageMode;
                buildingsToDestroy = buildingsToDestroyToCalm;
                break;
            default:
                GetBuildingTarget();
                break;
        }
    }
    public void TakeDamage (float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Slow (float pct)
    {
        speed = startSpeed * (1f - pct);
    }
    void Die()
    {
        PlayerStats.Money += worth;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        seeker.pathCallback -= PathsVerify;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
