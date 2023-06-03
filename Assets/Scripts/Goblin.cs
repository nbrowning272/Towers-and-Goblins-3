using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class Goblin : MonoBehaviour
{
    public GameObject player;
    public FirstPersonController controller;
    public Transform wall;
    public Transform playerMovement;
    public float speed = 0.2f;
    public float rotateSpeed = 0.0025f;
    public float health = 3f;
    public float maxHealth;
    private Rigidbody rb;
    public LineOfSight lineOfSight;
    public GoblinHealthBar goblinHealthBar;
    public GameManager gameManager;
    public GameObject managerObject;
    public GameObject playerCam;
    public Upgrade upgrade;
    public Player playerScript;
    public bool red = false;
    public bool boss = false;
    public string goblinColour;
    float distance;
    float timer = 2.2f;
    float oSpeed;
    public float attackTimer = 0.9f;
    public float attackInterval = 1.617f;
    public float goblinDamage;

    //public TrapControl trapControl;
    //public GameObject shortTrap;



    //[SerializeField] public Transform[] walls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineOfSight = GetComponent<LineOfSight>();
        goblinHealthBar.UpdateGoblinBar(maxHealth, health);
        managerObject = GameObject.FindGameObjectWithTag("GameManager");
        playerCam = GameObject.FindGameObjectWithTag("CinemachineTarget");
        upgrade = playerCam.GetComponent<Upgrade>();
        gameManager = managerObject.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        oSpeed = speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        //trapControl = shortTrap.GetComponent<TrapControl>();
    }



    // Update is called once per frame
    void Update()
    {
        if (GetPlayer() != null)
        {
            player = GetPlayer();
        }

        //Debug.Log(player.GetComponent<Player>().health);

        controller = GameObject.Find("PlayerCapsule").GetComponent<FirstPersonController>();
        SetAnimations();
        if (health <= 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                gameManager.salvage += Random.Range(3, 6);
                Destroy(gameObject);

            }
        }

        if (!wall && !player)
            GetWall();
        // Rotate towards target
        else if (wall && player == null)
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
        transform.rotation = Quaternion.Slerp(transform.rotation, wallDirection, 0.08f);

    }
    private void RotateTowardsPlayer()
    {
        Vector3 playerOffset = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Quaternion playerDirection = Quaternion.LookRotation(playerOffset - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, playerDirection, 0.2f);
    }

    public void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!red && !boss)
            {
                //Debug.Log(gameManager.wallHealth);
                gameManager.wallHealth--;
                Destroy(gameObject);
            }
            else if (red)
            {
                //Debug.Log(gameManager.wallHealth);
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

    }

    // ANIMATIONS

    Animator animator;
    [Header("Green Animations")]
    public const string GREENRUN = "greenRun";
    public const string GREENLEFT = "greenLeft";
    public const string GREENRIGHT = "greenRight";
    public const string GREENATTACK = "greenAttack";
    public const string GREENDEATH = "greenDeath";

    [Header("Blue Animations")]
    public const string BLUERUN = "blueRun";
    public const string BLUELEFT = "blueLeft";
    public const string BLUERIGHT = "blueRight";
    public const string BLUEATTACK = "blueAttack";
    public const string BLUEDEATH = "blueDeath";

    [Header("Red Animations")]
    public const string REDRUN = "redRun";
    public const string REDLEFT = "redLeft";
    public const string REDRIGHT = "redRight";
    public const string REDATTACK = "redAttack";
    public const string REDDEATH = "redDeath";

    [Header("Boss Animations")]
    public const string BOSSWALK = "bossWalk";
    public const string BOSSDAMAGE = "bossDamage";
    public const string BOSSDEATH = "bossDeath";

    string currentAnimationState;


    public void ChangeAnimationState(string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    void SetAnimations()
    {
        if (player)
        {
            playerScript = player.GetComponent<Player>();
            distance = Vector3.Distance(player.transform.position, transform.position);
        }

        if (controller)
        {
            //Blue animation changes
            if (goblinColour == "blue")
            {
                if (controller.GoblinChecker() == gameObject)
                {
                    Debug.Log("found");
                    if (controller.attacking && controller.attackCount == 0 && health > 1)
                    {
                        speed = 0;
                        ChangeAnimationState(BLUERIGHT);
                    }
                    else if (controller.attacking && controller.attackCount == 1 && health > 1)
                    {
                        speed = 0;
                        ChangeAnimationState(BLUELEFT);
                    }
                    else if (distance < 1.7 && !controller.attacking && health > 0 && player)
                    {
                        Debug.Log("done 2");
                        speed = 0;
                        GoblinAttack();
                        ChangeAnimationState(BLUEATTACK);
                    }
                    else if (health <= 0)
                    {
                        speed = 0;
                        ChangeAnimationState(BLUEDEATH);

                    }
                    else
                    {
                        speed = oSpeed;
                        ChangeAnimationState(BLUERUN);
                    }
                }
                else if (controller.GoblinChecker() != gameObject && player)
                {
                    if (distance < 1.7 && health > 0)
                    {
                        speed = 0;
                        GoblinAttack();
                        ChangeAnimationState(BLUEATTACK);
                    }
                    else if (health <= 0)
                    {
                        speed = 0;
                        ChangeAnimationState(BLUEDEATH);

                    }
                    else
                    {
                        speed = oSpeed;
                        ChangeAnimationState(BLUERUN);
                    }
                }
                else if (health <= 0)
                {
                    speed = 0;
                    ChangeAnimationState(BLUEDEATH);

                }
                else
                {
                    speed = oSpeed;
                    ChangeAnimationState(BLUERUN);
                }
            }

            //Green animation changes
            else if (goblinColour == "green")
            {
                if (controller.GoblinChecker() == gameObject)
                {
                    Debug.Log("found");
                    if (controller.attacking && controller.attackCount == 0 && health > 1)
                    {
                        speed = 0;
                        ChangeAnimationState(GREENRIGHT);
                    }
                    else if (controller.attacking && controller.attackCount == 1 && health > 1)
                    {
                        speed = 0;
                        ChangeAnimationState(GREENLEFT);
                    }
                    else if (distance < 1.7 && !controller.attacking && health > 0 && player)
                    {
                        Debug.Log("done 2");
                        speed = 0;
                        GoblinAttack();
                        ChangeAnimationState(GREENATTACK);
                    }
                    else if (health <= 0)
                    {
                        speed = 0;
                        ChangeAnimationState(GREENDEATH);

                    }
                    else
                    {
                        speed = oSpeed;
                        ChangeAnimationState(GREENRUN);
                    }
                }
                else if (controller.GoblinChecker() != gameObject && player)
                {
                    if (distance < 1.7 && health > 0)
                    {
                        speed = 0;
                        GoblinAttack();
                        ChangeAnimationState(GREENATTACK);
                    }
                    else if (health <= 0)
                    {
                        speed = 0;
                        ChangeAnimationState(GREENDEATH);

                    }
                    else
                    {
                        speed = oSpeed;
                        ChangeAnimationState(GREENRUN);
                    }
                }
                else if (health <= 0)
                {
                    speed = 0;
                    ChangeAnimationState(GREENDEATH);

                }
                else
                {
                    speed = oSpeed;
                    ChangeAnimationState(GREENRUN);
                }

            }
            //Red animation changes
            else if (goblinColour == "red")
            {
                if (controller.GoblinChecker() == gameObject)
                {
                    Debug.Log("found");
                    if (controller.attacking && controller.attackCount == 0 && health > 1)
                    {
                        speed = 0;
                        ChangeAnimationState(REDRIGHT);
                    }
                    else if (controller.attacking && controller.attackCount == 1 && health > 1)
                    {
                        speed = 0;
                        ChangeAnimationState(REDLEFT);
                    }
                    else if (distance < 1.7 && !controller.attacking && health > 0 && player)
                    {
                        Debug.Log("done 2");
                        speed = 0;
                        GoblinAttack();
                        ChangeAnimationState(REDATTACK);
                        
                        //playerScript.health--;
                    }
                    else if (health <= 0)
                    {
                        speed = 0;
                        ChangeAnimationState(REDDEATH);

                    }
                    else
                    {
                        speed = oSpeed;
                        ChangeAnimationState(REDRUN);
                    }
                }
                else if (controller.GoblinChecker() != gameObject && player)
                {
                    if (distance < 1.7 && health > 0)
                    {
                        speed = 0;
                        GoblinAttack();
                        ChangeAnimationState(REDATTACK);
                        
                    }
                    else if (health <= 0)
                    {
                        speed = 0;
                        ChangeAnimationState(REDDEATH);

                    }
                    else
                    {
                        speed = oSpeed;
                        ChangeAnimationState(REDRUN);
                    }
                }
                else if (health <= 0)
                {
                    speed = 0;
                    ChangeAnimationState(REDDEATH);

                }
                else
                {
                    speed = oSpeed;
                    ChangeAnimationState(REDRUN);
                }


            }
        }
        
    }
    public void GoblinAttack()
    {
        if (attackTimer < attackInterval)
        {
            attackTimer += Time.deltaTime;
        }
        else
        {
            player.GetComponent<Player>().health -= goblinDamage;
            attackTimer = 0;
        }
    }
}
