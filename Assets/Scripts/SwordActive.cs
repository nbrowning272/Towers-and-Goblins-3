using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordActive : MonoBehaviour
{
    public GameObject sword;
    public Upgrade upgrade;
    

    public bool swordActive = false;
    // Start is called before the first frame update
    void Start()
    {
        //sword = GameObject.Find("Sword");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//&& upgrade.swordLevel >= 1)
        {
            swordActive = true;
        }

        SwordTimer();

    }

    private void SwordTimer()
    {
        if (swordActive)
        {
            StartCoroutine(TurnOnSword());
        }
    }

    private IEnumerator TurnOnSword()
    {
        sword.SetActive(true);
        yield return new WaitForSeconds(1);
        sword.SetActive(false);
        swordActive = false;
    }
    
}
