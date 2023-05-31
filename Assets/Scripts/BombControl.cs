using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour
{
    Animator animator;
    GameObject goblinObject;
    Goblin goblin;
    GameObject bomb;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bomb = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goblin")
        {
            animator.SetTrigger("goblinBombTrigger");
            StartCoroutine(Explosion(gameObject.transform.position, 7.5f));
        }
    }

    IEnumerator Explosion(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        yield return new WaitForSeconds(1);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider != null)
            {
                if (hitCollider.tag == "Goblin")
                {
                    goblin = hitCollider.GetComponentInParent<Goblin>();
                    if (goblin != null)
                    {
                        goblin.health -= 5;
                    }
                }
            } 
        }
        Destroy(gameObject);
    }
}
