using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goblin : MonoBehaviour
{
    public GameObject player;
    public Transform wall;
    public Transform playerMovement;
    public float speed = 0.2f;
    public float rotateSpeed = 0.0025f;
    public float health = 10f;
    public float maxHealth;
    private Rigidbody rb;
    public LineOfSight lineOfSight;
    public GoblinHealthBar goblinHealthBar;
    public GameManager gameManager;
    public GameObject managerObject;
    public GameObject playerCam;
    public Upgrade upgrade;
    public bool red = false;
    public bool boss = false;
    

    //public TrapControl trapControl;
    //public GameObject shortTrap;



    //[SerializeField] public Transform[] walls;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        rb = GetComponent<Rigidbody>();
        lineOfSight = GetComponent<LineOfSight>();
        goblinHealthBar.UpdateGoblinBar(maxHealth, health);
        managerObject = GameObject.FindGameObjectWithTag("GameManager");
        playerCam = GameObject.FindGameObjectWithTag("CinemachineTarget");
        upgrade = playerCam.GetComponent<Upgrade>();
        gameManager = managerObject.GetComponent<GameManager>();

        //trapControl = shortTrap.GetComponent<TrapControl>();
    }



    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gameManager.salvage += Random.Range(3, 6);
            Destroy(gameObject);
        }
        // Get target
        if (!player)
            player = GetPlayer();

        if (!wall && !player)
            GetWall();
        // Rotate towards target
        if (wall && !player)
            AttackWall();
        else
        {
            RotateTowardsPlayer();
        }
        goblinHealthBar.UpdateGoblinBar(maxHealth, health);
    }
    private void FixedUpdate()
    {
        // Move to target
        rb.velocity = transform.forward * speed;
    }


    private void GetWall()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        wall = walls[Random.Range(0, walls.Length)].transform;
    }

    GameObject GetPlayer()
    {
        if (lineOfSight.Objects.Count > 0)
        {
            return lineOfSight.Objects[0];
        }
        //player = GameObject.FindGameObjectWithTag("Player");
        return null;

    }

    private void AttackWall()
    {
        Vector3 offset = new Vector3(wall.transform.position.x, wall.transform.position.y - 0.65738f, wall.transform.position.z);
        Quaternion wallDirection = Quaternion.LookRotation(offset - transform.position);
        float wallTime = 0;
        while (wallTime < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, wallDirection, wallTime);
            wallTime += Time.deltaTime * rotateSpeed;
        }
    }
    private void RotateTowardsPlayer()
    {
        Quaternion playerDirection = Quaternion.LookRotation(player.transform.position - transform.position);
        float time = 0;
        while (time < 1)
        //if (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, playerDirection, time);
            time += Time.deltaTime * rotateSpeed;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            if (upgrade.swordLevel == 1)
            {
                health--;
            }
            if (upgrade.swordLevel == 2)
            {
                health-= 2;
            }
            if (upgrade.swordLevel == 3)
            {
                health-= 3;
            }
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!red && !boss)
            {
                Debug.Log(gameManager.wallHealth);
                gameManager.wallHealth--;
                Destroy(gameObject);
            }
            else if (red)
            {
                Debug.Log(gameManager.wallHealth);
                gameManager.wallHealth-= 3;
                Destroy(gameObject);
            }
            else if (boss)
            {
                gameManager.wallHealth -= 5;
                Destroy(gameObject);
            }
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            health--;
        }
        if (other.gameObject.CompareTag("LongBullet"))
        {
            Destroy(other.gameObject);
            health -= 2;
        }
        //if (other.gameObject.CompareTag("ShortRadius"))
        //{
        //    trapControl.goblin = gameObject.transform;
        //}

    }
    //public void OnTriggerStay(Collider other)
    //{
        
    //}
    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("ShortRadius"))
    //    {
    //        trapControl.goblin = null;
    //    }
    //}
}
