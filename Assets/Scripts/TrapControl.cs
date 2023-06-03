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
    public float trapTimer = 45;
    public TrapHealthBar trapBar;


    // Start is called before the first frame update
    public void Start()
    {
        if (gameObject.tag != "Tower")
        {
            trapBar.UpdateTrapBar(45, trapTimer);
        }
        else level1 = true;
        
    }

    // Update is called once per frame
    public void Update()
    {
        trapTimer -= Time.deltaTime;
        if (gameObject.tag != "Tower")
        {
            trapBar.UpdateTrapBar(45, trapTimer);
        }
        TrapMaintenance();
        targetGoblin = FindClosestGoblin().transform;
        Vector3 targetGoblinOffset = new Vector3(targetGoblin.position.x - 0.2f, targetGoblin.position.y + 0.5f, targetGoblin.position.z - 0.2f);
        if (level1)
        {
            targetGoblin = null;
        }
        if (targetGoblin != null) {
            distance = Vector3.Distance(targetGoblin.position, transform.position);
            if (distance <= maxDistance)
            {
                orb.LookAt(targetGoblinOffset);
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
        clone.GetComponent<Rigidbody>().AddForce(orb.transform.forward * 20000);
        Destroy(clone, 2);
        Debug.Log(trapTimer);
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
    public void TrapMaintenance()
    {
        if (trapTimer <= 0 && gameObject.tag != "Tower")
        {
            Destroy(gameObject);
        }
    }
    
}
        
