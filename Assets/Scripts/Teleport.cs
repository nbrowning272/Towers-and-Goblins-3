using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Teleport : MonoBehaviour
{
    public Transform portal1;
    public Transform portal2;
    FirstPersonController firstPersonController;
    public bool justTeleported;
    // Start is called before the first frame update
    void Start()
    {
        firstPersonController = gameObject.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (justTeleported == false)
        {
            if (other.gameObject.CompareTag("Portal1"))
            {
                Debug.Log("hi");
                StartCoroutine("TeleportsTo2");
                justTeleported = true;
                StartCoroutine("JustTeleported");
            }
            if (other.gameObject.CompareTag("Portal2"))
            {
                Debug.Log("hi");
                StartCoroutine("TeleportsTo1");
                justTeleported = true;
                StartCoroutine("JustTeleported");
            }
        }
    }
    IEnumerator TeleportsTo2()
    {
        
        firstPersonController.disabled = true;
        gameObject.transform.position = new Vector3(portal2.position.x, portal2.position.y + 2, portal2.position.z);
        yield return new WaitForSeconds(0.1f);
        firstPersonController.disabled = false;
        
    }
    IEnumerator TeleportsTo1()
    {

        firstPersonController.disabled = true;
        gameObject.transform.position = new Vector3(portal1.position.x, portal1.position.y + 2, portal1.position.z);
        yield return new WaitForSeconds(0.1f);
        firstPersonController.disabled = false;

    }
    IEnumerator JustTeleported()
    {
        yield return new WaitForSeconds(2f);
        justTeleported = false;

    }
}
