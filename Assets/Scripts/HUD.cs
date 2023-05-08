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

    public Player player;
    public GameManager gameManager;
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
    }
}
