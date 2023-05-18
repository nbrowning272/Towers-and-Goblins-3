using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public WeaponStation weaponStation;
    public WallStation wallStation;
    public TowerStation towerStation;
    public int swordLevel = 1;
    public float swordCost;
    public string swordCostText;
    public int wallLevel = 1;
    public float wallCost;
    public string wallCostText;
    public int towerLevel = 1;
    public float towerCost;
    public string towerCostText;
    public GameManager gameManager;
    public float lightningTrapCost = 15;
    public float rangedTrapCost = 20;
    public PlaceObject placeObject;
    public RangedTrap rangedTrap;
    public GameObject towerObject;
    public TrapControl towerControl;
    public float trapMaintenanceCost = 5f;
    // Start is called before the first frame update
    void Start()
    {
        placeObject = GetComponent<PlaceObject>();
        rangedTrap = GetComponent<RangedTrap>();
        towerControl = towerObject.GetComponent<TrapControl>();
        
    }

    // Update is called once per frame
    void Update()
    {
        WallCost();
        SwordCost();
        TowerCost();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            if (hit.collider.gameObject.tag == "WeaponStation")
            {
                //WEAPON STATION
                weaponStation.wText = "U: Upgrade Sword - " + swordCostText + "\nJ: Buy Lightning Trap - 15 Salvage \nN: Buy Ranged Lightning Trap - 20 Salvage";// \nB: Bomb Trap - 10 Salvage";
                if (Input.GetKeyDown(KeyCode.U) && gameManager.salvage > swordCost){
                    gameManager.salvage -= swordCost;
                    swordLevel++;
                }
                if (Input.GetKeyDown(KeyCode.J) && gameManager.salvage > lightningTrapCost)
                {
                    gameManager.salvage -= lightningTrapCost;
                    placeObject.traps++;
                }
                if (Input.GetKeyDown(KeyCode.N) && gameManager.salvage > lightningTrapCost)
                {
                    gameManager.salvage -= lightningTrapCost;
                    rangedTrap.traps++;
                }
            }
            if (hit.collider.gameObject.tag != "WeaponStation")
            {
                weaponStation.wText = "";
            }

            // WALL
            if (hit.collider.gameObject.tag == "WallStation")
            {
                wallStation.wText = "H: Upgrade Wall - " + wallCostText;
                if (Input.GetKeyDown(KeyCode.H) && gameManager.salvage > wallCost)
                {
                    gameManager.salvage -= wallCost;
                    gameManager.wallHealth *= 1.5f;
                    gameManager.wallHealth = Mathf.Ceil(gameManager.wallHealth);
                    gameManager.maxWallHealth *= 1.5f;
                    gameManager.maxWallHealth = Mathf.Ceil(gameManager.maxWallHealth);
                    wallLevel++;
                }
            }
            if (hit.collider.gameObject.tag != "WallStation")
            {
                wallStation.wText = "";
            }

            // TOWER
            if (hit.collider.gameObject.tag == "Tower")
            {
                towerStation.tText = "Y: Upgrade Tower - " + towerCostText;
                if (Input.GetKeyDown(KeyCode.Y) && gameManager.salvage > towerCost)
                {
                    gameManager.salvage -= towerCost;
                    towerControl.fireRate += 0.25f;
                    towerLevel++;
                }
            }
            if (hit.collider.gameObject.tag != "Tower")
            {
                towerStation.tText = "";
            }
            if (hit.collider.gameObject.tag == "Trap1" || hit.collider.gameObject.tag == "RangedTrap")
            {
                GameObject trapM = hit.transform.gameObject;
                Transform HealthBarT = trapM.transform.Find("HealthBar");
                GameObject HealthBar = HealthBarT.gameObject;
                HealthBar.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && gameManager.salvage > trapMaintenanceCost)
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
        }
    }
    public void SwordCost()
    {
        if (swordLevel == 1)
        {
            swordCostText = "30 Salvage";
            swordCost = 30;
        }
        if (swordLevel == 2)
        {
            swordCostText = "50 Salvage";
            swordCost = 50;
        }
        if (swordLevel == 3)
        {
            swordCostText = "Maxed Out";
            swordCost = 1000;
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
            wallCostText = "30 Salvage";
            wallCost = 30;
        }
        if (wallLevel == 3)
        {
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
            towerControl.level1 = false;
            towerCostText = "50 Salvage";
            towerCost = 50;
        }
        if (towerLevel == 3)
        {
            towerCostText = "50 Salvage";
            towerCost = 50;
        }
        if (towerLevel == 4)
        {
            towerCostText = "Maxed Out";
            towerCost = 1000;
        }
    }
    public void TrapMaintenanceCost()
    {

    }
}

