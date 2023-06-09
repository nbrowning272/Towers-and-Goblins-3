using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [Header("Station Scripts")]
    public WeaponStation weaponStation;
    public WallStation wallStation;
    public TowerStation towerStation;

    [Header("Sword")] 
    public int swordLevel = 1;
    public float swordCost;
    public string swordCostText;
    public float swordDamage = 1;
    public GameObject Sword1;
    public GameObject Sword2;
    public GameObject Sword3;

    [Header("Wall")]
    public int wallLevel = 1;
    public float wallCost;
    public string wallCostText;
    public GameObject wallObject1;
    public GameObject wallObject2;
    public GameObject wallObject3;
    public GameObject core1;
    public GameObject core2;
    public GameObject core3;

    [Header("Tower")]
    public int towerLevel = 1;
    public float towerCost;
    public string towerCostText;
    public GameObject towerObject1;
    public GameObject towerObject2;
    public GameObject towerObject3;
    public TrapControl towerControl;


    

    [Header("Traps")]
    public float lightningTrapCost = 15;
    public float rangedTrapCost = 20;
    public float bombTrapCost = 15;
    public PlaceObject placeObject;
    public RangedTrap rangedTrap;
    public BombTrap bombTrap;
    public float trapMaintenanceCost = 5f;

    [Header("Others")]
    public GameManager gameManager;
    public HUD hud;
    // Start is called before the first frame update
    void Start()
    {
        placeObject = GetComponent<PlaceObject>();
        rangedTrap = GetComponent<RangedTrap>();
        bombTrap = GetComponent<BombTrap>();
        towerControl = towerObject1.GetComponent<TrapControl>();
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            hud.interactText = "";

            if (hit.collider.gameObject.tag != "Trap1" && hit.collider.gameObject.tag != "RangedTrap")
            {
                GameObject[] lightningTraps = GameObject.FindGameObjectsWithTag("Trap1");
                GameObject[] rangedTraps = GameObject.FindGameObjectsWithTag("RangedTrap");
                foreach (GameObject lightningTrap in lightningTraps)
                {
                    Transform HealthBarT = lightningTrap.transform.Find("HealthBar");
                    GameObject HealthBar = HealthBarT.gameObject;
                    HealthBar.SetActive(false);
                }
                foreach (GameObject rangedTrap in rangedTraps)
                {
                    Transform HealthBarT = rangedTrap.transform.Find("HealthBar");
                    GameObject HealthBar = HealthBarT.gameObject;
                    HealthBar.SetActive(false);
                }
            }

            //ANVIL
            if (hit.collider.gameObject.tag == "Anvil")
            {
                SwordCost();
                hud.interactText = "E - Upgrade Sword - " + swordCostText;
                
                if (Input.GetKeyDown(KeyCode.E) && gameManager.salvage >= swordCost)
                {
                    gameManager.salvage -= swordCost;
                    swordLevel++;
                }
            }

            // LIGHTNING TRAPS
            if (hit.collider.gameObject.tag == "BuyLightning")
            {
                hud.interactText = "E - Buy Lightning Trap - " + lightningTrapCost + " Salvage";
                if (Input.GetKeyDown(KeyCode.E) && gameManager.salvage >= lightningTrapCost)
                {
                    gameManager.salvage -= lightningTrapCost;
                    placeObject.traps++;
                }
            }

            // RANGED TRAPS
            if (hit.collider.gameObject.tag == "BuyRanged")
            {
                hud.interactText = "E - Buy Ranged Trap - " + rangedTrapCost + " Salvage";
                if (Input.GetKeyDown(KeyCode.E) && gameManager.salvage >= rangedTrapCost)
                {
                    gameManager.salvage -= rangedTrapCost;
                    rangedTrap.traps++;
                }
            }

            // BOMB TRAPS
            if (hit.collider.gameObject.tag == "BuyBomb")
            {
                hud.interactText = "E - Buy Bomb Trap - " + bombTrapCost + " Salvage";
                if (Input.GetKeyDown(KeyCode.E) && gameManager.salvage >= bombTrapCost)
                {
                    gameManager.salvage -= bombTrapCost;
                    bombTrap.traps++;
                }
            }

            // WALL
            if (hit.collider.gameObject.tag == "WallStation")
            {
                WallCost();
                hud.interactText = "E - Upgrade Wall - " + wallCostText;
                if (Input.GetKeyDown(KeyCode.E) && gameManager.salvage >= wallCost)
                {
                    gameManager.salvage -= wallCost;
                    gameManager.wallHealth *= 1.5f;
                    gameManager.wallHealth = Mathf.Ceil(gameManager.wallHealth);
                    gameManager.maxWallHealth *= 1.5f;
                    gameManager.maxWallHealth = Mathf.Ceil(gameManager.maxWallHealth);
                    wallLevel++;
                }
            }

            // TOWER
            if (hit.collider.gameObject.tag == "Tower")
            {
                TowerCost();
                hud.interactText = "E - Upgrade Tower - " + towerCostText;
                if (Input.GetKeyDown(KeyCode.E) && gameManager.salvage >= towerCost)
                {
                    gameManager.salvage -= towerCost;
                    towerLevel++;
                    TowerCost();
                    towerControl.fireRate += 0.25f;
                    
                    
                }
            }

            // TRAP MAINTENANCE
            if (hit.collider.gameObject.tag == "Trap1" || hit.collider.gameObject.tag == "RangedTrap")
            {
                GameObject trapM = hit.transform.gameObject;
                Transform HealthBarT = trapM.transform.Find("HealthBar");
                GameObject HealthBar = HealthBarT.gameObject;
                HealthBar.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && gameManager.salvage >= trapMaintenanceCost)
                {
                    if (hit.collider.gameObject.tag == "Trap1")
                    {
                        trapMaintenanceCost = 5;
                    }
                    if (hit.collider.gameObject.tag == "RangedTrap")
                    {
                        trapMaintenanceCost = 7;
                    }
                    gameManager.salvage -= trapMaintenanceCost;

                    TrapControl trapControl = trapM.GetComponent<TrapControl>();
                    trapControl.trapTimer = 45;
                    Debug.Log(trapControl.trapTimer);

                }
            }
            

                
        }
    }
    public void SwordCost()
    {
        if (swordLevel == 1)
        {
            swordCostText = "30 Salvage";
            swordCost = 30;
            swordDamage = 1;
        }
        if (swordLevel == 2)
        {
            Sword1.SetActive(false);
            Sword2.SetActive(true);
            swordCostText = "50 Salvage";
            swordCost = 50;
            swordDamage = 2;
        }
        if (swordLevel == 3)
        {
            Sword2.SetActive(false);
            Sword3.SetActive(true);
            swordCostText = "Maxed Out";
            swordCost = 1000;
            swordDamage = 3;
        }
    }
    public void WallCost()
    {
        if (wallLevel == 1)
        {
            wallCostText = "25 Salvage";
            wallCost = 25;
            
        }
        if(wallLevel == 2)
        {
            wallObject1.SetActive(false);
            wallObject2.SetActive(true);
            core1.SetActive(false);
            core2.SetActive(true);
            wallCostText = "30 Salvage";
            wallCost = 30;
        }
        if (wallLevel == 3)
        {
            wallObject2.SetActive(false);
            wallObject3.SetActive(true);
            core2.SetActive(false);
            core3.SetActive(true);
            wallCostText = "Maxed Out";
            wallCost = 1000;
        }
    }
    public void TowerCost()
    {
        if (towerLevel == 1)
        {
            towerControl.level1 = true;
            towerCostText = "20 Salvage";
            towerCost = 20;
        }
        if (towerLevel == 2)
        {
            towerObject1.SetActive(false);
            towerObject2.SetActive(true);
            towerControl = towerObject2.GetComponent<TrapControl>();
            towerControl.level1 = false;
            towerCostText = "50 Salvage";
            towerCost = 50;
        }
        if (towerLevel == 3)
        {
            towerControl.level1 = false;
            towerObject2.SetActive(false);
            towerObject3.SetActive(true);
            towerControl = towerObject3.GetComponent<TrapControl>();
            towerCostText = "Maxed Out";
            towerCost = 1000;
        }

    }
    public void TrapMaintenanceCost()
    {
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f))
        //{
        //    if (hit.collider.gameObject.tag == "WeaponStation")
        //    {
        //        //WEAPON STATION
        //        weaponStation.wText = "U: Upgrade Sword - " + swordCostText + "\nJ: Buy Lightning Trap - 15 Salvage \nN: Buy Ranged Lightning Trap - 20 Salvage";// \nB: Bomb Trap - 10 Salvage";
        //        if (Input.GetKeyDown(KeyCode.U) && gameManager.salvage > swordCost){
        //            gameManager.salvage -= swordCost;
        //            swordLevel++;
        //        }
        //        if (Input.GetKeyDown(KeyCode.J) && gameManager.salvage > lightningTrapCost)
        //        {
        //            gameManager.salvage -= lightningTrapCost;
        //            placeObject.traps++;
        //        }
        //        if (Input.GetKeyDown(KeyCode.N) && gameManager.salvage > lightningTrapCost)
        //        {
        //            gameManager.salvage -= lightningTrapCost;
        //            rangedTrap.traps++;
        //        }
        //    }
        //    if (hit.collider.gameObject.tag != "WeaponStation")
        //    {
        //        weaponStation.wText = "";
        //    }

        //    // WALL
        //    if (hit.collider.gameObject.tag == "WallStation")
        //    {
        //        wallStation.wText = "H: Upgrade Wall - " + wallCostText;
        //        if (Input.GetKeyDown(KeyCode.H) && gameManager.salvage > wallCost)
        //        {
        //            gameManager.salvage -= wallCost;
        //            gameManager.wallHealth *= 1.5f;
        //            gameManager.wallHealth = Mathf.Ceil(gameManager.wallHealth);
        //            gameManager.maxWallHealth *= 1.5f;
        //            gameManager.maxWallHealth = Mathf.Ceil(gameManager.maxWallHealth);
        //            wallLevel++;
        //        }
        //    }
        //    if (hit.collider.gameObject.tag != "WallStation")
        //    {
        //        wallStation.wText = "";
        //    }

        //    // TOWER
        //    if (hit.collider.gameObject.tag == "Tower")
        //    {
        //        towerStation.tText = "Y: Upgrade Tower - " + towerCostText;
        //        if (Input.GetKeyDown(KeyCode.Y) && gameManager.salvage > towerCost)
        //        {
        //            gameManager.salvage -= towerCost;
        //            towerControl.fireRate += 0.25f;
        //            towerLevel++;
        //        }
        //    }
        //    if (hit.collider.gameObject.tag != "Tower")
        //    {
        //        towerStation.tText = "";
        //    }
        //    if (hit.collider.gameObject.tag == "Trap1" || hit.collider.gameObject.tag == "RangedTrap")
        //    {
        //        GameObject trapM = hit.transform.gameObject;
        //        Transform HealthBarT = trapM.transform.Find("HealthBar");
        //        GameObject HealthBar = HealthBarT.gameObject;
        //        HealthBar.SetActive(true);
        //        if (Input.GetKeyDown(KeyCode.E) && gameManager.salvage > trapMaintenanceCost)
        //        {
        //            if (hit.collider.gameObject.tag == "Trap1")
        //            {
        //                trapMaintenanceCost = 5;
        //            }
        //            if (hit.collider.gameObject.tag == "RangedTrap")
        //            {
        //                trapMaintenanceCost = 7;
        //            }
        //            gameManager.salvage -= trapMaintenanceCost;

        //            TrapControl trapControl = trapM.GetComponent<TrapControl>();
        //            trapControl.trapTimer = 45;
        //            Debug.Log(trapControl.trapTimer);

        //        }
        //    }
    }
}

