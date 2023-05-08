using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MudTerrain : MonoBehaviour
{
    public GameObject mudAffect;
    public GameObject playerObject;
    public float playerMovSpeed;
    //Script playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        playerMovSpeed = GameObject.Find("Sphere").GetComponent<Test>().moveingspeed;
        //playerMovSpeed = GameObject.Find("PlayerCapsule").GetComponent<FirstPersonController>().MoveSpeed;
        //playerScript = GameObject.GetComponent<FirstPersonController>().MoveSpeed;
        //playerMovSpeed = playerObject.GetComponent<FirstPersonController>.MoveSpeed;
        //playerMovSpeed = GameObject.Find("PlayerCapsule").GetComponent<FirstPersonController>.MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player in on mud");
            Mud();
        }
    }

    void Mud()
    {

    }
}
