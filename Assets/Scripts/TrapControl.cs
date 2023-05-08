using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControl : MonoBehaviour
{
    public Transform targetGoblin;
    float distance;
    public float maxDistance;
    public Transform orb, shootPoint;
    public GameObject bulletPrefab;
    public float fireRate, nextFire;
    public bool level1 = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        targetGoblin = FindClosestGoblin().transform;
        if (level1)
        {
            targetGoblin = null;
        }
        if (targetGoblin != null) {
            distance = Vector3.Distance(targetGoblin.position, transform.position);
            if (distance <= maxDistance)
            {
                orb.LookAt(targetGoblin);
                if (Time.time >= nextFire)
                {
                    nextFire = Time.time + 1f / fireRate;
                    Shoot();
                }

            }
        }

    }
    void Shoot()
    {
        GameObject clone = Instantiate(bulletPrefab, shootPoint.position, orb.rotation);
        clone.GetComponent<Rigidbody>().AddForce(orb.transform.forward * 5000);
        Destroy(clone, 2);
    }
    public GameObject FindClosestGoblin()
    {
        GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
        GameObject closestGoblin = null;
        float closestDistance = 0;
        bool first = true;

        foreach (GameObject goblin in goblins)
        {
            float goblinDistance = Vector3.Distance(goblin.transform.position, transform.position);
            if (first)
            {
                closestDistance = goblinDistance;
                closestGoblin = goblin;
                first = false;
            }
            else if (goblinDistance < closestDistance)
            {
                closestGoblin = goblin;
                closestDistance = goblinDistance;

            }
        }
        return closestGoblin;
    }
    
}
        
