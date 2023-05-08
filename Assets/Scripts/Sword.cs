using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameManager gameManager;
    public float sSalvage;
    public Goblin goblin;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Goblin"))
        { 
            goblin = other.gameObject.GetComponent<Goblin>();
            if (goblin.health <= 0)
            {
                
                Debug.Log(sSalvage);
            }


        }

    }
}
