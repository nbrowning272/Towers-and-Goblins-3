using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float health = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;

    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Goblin"))
        {
            Goblin goblin = other.gameObject.GetComponent<Goblin>();
            if (!goblin.red && !goblin.boss)
            {
                health--;
                Debug.Log(health);
                //Destroy(other.gameObject);
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else if (goblin.red)
            {
                health-= 3;
                Debug.Log(health);
                //Destroy(other.gameObject);
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else if (goblin.boss)
            {
                health -= 5;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
            
        }

    }
}
