using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Vector3 goblinPosition;
    Vector3 trapPosition;
    Vector3 trapToGoblinPosition;
    bool goblinFound = false;
    GameObject goblin;
    public Bullet bullet;
    public GameObject bulletPrefab;
    float shootTimer;
    // Start is called before the first frame update
    void Start()
    {
        trapPosition = transform.position;
        bullet.rb = bullet.GetComponent<Rigidbody>();
        bullet.bullet = bulletPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (goblinFound == true)
        {
            Shoot();
            goblinPosition = goblin.transform.position;
            trapToGoblinPosition = goblinPosition - trapPosition;
            trapToGoblinPosition.Normalize();
            Vector3 velocity = trapToGoblinPosition * bullet.speed;
            bullet.rb.velocity = velocity;
            
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goblin")) 
        {
            goblin = other.gameObject;
            goblinFound = true;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
