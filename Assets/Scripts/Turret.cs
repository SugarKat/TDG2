using UnityEngine;
using System.Collections.Generic;

internal enum EnemyType
{
    Strongest,
    Fastest,
    Normal
}

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    [SerializeField]
    private static EnemyType enemyType = EnemyType.Normal;
    public float range = 12f;

    [Header("Use Bullets (default)")]

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Unity Setup fields")]

    public string enemyTag = "Enemy";

    public Transform PartToRotate;
    public float turnSpeed = 10f;

    public List<Transform> firePoint;
    int firePointToUse = 0;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        
        if (nearestEnemy != null && shortestDistance <= range)
        {
            if (target == null)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<Enemy>();
            }
        }
        else
        {
            target = null;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(target == null)
        {
            return;
        }

        LockOnTarget();

        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
                
        }

        fireCountDown -= Time.deltaTime;
	}

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint[firePointToUse].position, firePoint[firePointToUse].rotation);
        firePointToUse++;
        if(firePointToUse >= firePoint.Count)
        {
            firePointToUse = 0;
        }
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void SelectNormalEnemy()
    {
        enemyType = EnemyType.Normal;
    }

    public void SelectFastestEnemy()
    {
        enemyType = EnemyType.Fastest;
    }

    public void SelectStrongestEnemy()
    {
        enemyType = EnemyType.Strongest;
    }
}
