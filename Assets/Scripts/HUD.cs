using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public string healthText;
    public Text hTextElement;

    public string salvageText;
    public Text sTextElement;

    public string wallHealthText;
    public Text wTextElement;

    public string waveHealthText;
    public Text wHTextElement;

    public string instructionText;
    public Text iTextElement;

    public string interactText;
    public Text intTextElement;

    public Player player;
    public GameManager gameManager;
    public EnemySpawner enemySpawner;
    public PlaceObject lightningTrap;
    public RangedTrap rangedTrap;
    public BombTrap bombTrap;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        healthText = "Health: " + player.health;
        hTextElement.text = healthText;

        salvageText = "Salvage: " + gameManager.salvage;
        sTextElement.text = salvageText;

        wallHealthText = "Wall Health: " + gameManager.wallHealth + "/" + gameManager.maxWallHealth;
        wTextElement.text = wallHealthText;

        if (enemySpawner.waitTimer == (10 + (enemySpawner.currentWave * 2.5f)))
        {
            waveHealthText = "Wave: " + enemySpawner.currentWave;
            wHTextElement.text = waveHealthText;
        }
        else
        {
            waveHealthText = "Grace Period: \n" + enemySpawner.waitTimer.ToString("F1") + " Seconds Remaining";
            wHTextElement.text = waveHealthText;
        }
        instructionText = TrapInstruction();
        iTextElement.text = instructionText;



        intTextElement.text = interactText;
    }

    public string TrapInstruction()
    {
        if (rangedTrap.traps > 0 && lightningTrap.traps < 1 && bombTrap.traps < 1)
        {
            if (rangedTrap.ghostTrap == false)
            {
                return "2 - Ranged Trap";
            }
            else return "RMB - Place Trap";
        }
        else if (rangedTrap.traps < 1 && lightningTrap.traps > 0 && bombTrap.traps < 1)
        {
            if (lightningTrap.ghostTrap == false)
            {
                return "1 - Lightning Trap";
            }
            else return "RMB - Place Trap";
        }
        else if (rangedTrap.traps > 0 && lightningTrap.traps > 0 && bombTrap.traps < 1)
        {
            if (lightningTrap.ghostTrap == false && rangedTrap.ghostTrap == false)
            {
                return "1 - Lightning Trap\n2 - Ranged Trap";
            }
            else return "RMB - Place Trap";
        }
        else if (rangedTrap.traps > 0 && bombTrap.traps > 0 && lightningTrap.traps < 1)
        {
            if (rangedTrap.ghostTrap == false && bombTrap.ghostTrap == false)
            {
                return "2 - Ranged Trap\n3 - Bomb Trap";
            }
            else return "RMB - Place Trap";
        }
        else if (lightningTrap.traps > 0 && bombTrap.traps > 0 && rangedTrap.traps < 1)
        {
            if (lightningTrap.ghostTrap == false && bombTrap.ghostTrap == false)
            {
                return "1 - Lightning Trap\n3 - Bomb Trap";
            }
            else return "RMB - Place Trap";
        }
        else if (lightningTrap.traps > 0 && bombTrap.traps > 0 && rangedTrap.traps > 0)
        {
            if (lightningTrap.ghostTrap == false && bombTrap.ghostTrap == false && rangedTrap.ghostTrap == false)
            {
                return "1 - Lightning Trap\n2 - Ranged Trap\n3 - Bomb Trap";
            }
            else return "RMB - Place Trap";
        }
        else if (lightningTrap.traps < 1 && bombTrap.traps > 0 && rangedTrap.traps < 1)
        {
            if (bombTrap.ghostTrap == false)
            {
                return "3 - Bomb Trap";
            }
            else return "RMB - Place Trap";
        }
        else return "";
    }
}
