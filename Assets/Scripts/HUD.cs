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

    public Player player;
    public GameManager gameManager;
    public EnemySpawner enemySpawner;

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
            waveHealthText = "Grace Period: \n" + enemySpawner.waitTimer.ToString("F2") + " Seconds Remaining";
            wHTextElement.text = waveHealthText;
        }
    }
}
