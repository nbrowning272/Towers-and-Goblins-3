using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class MudTerrain : MonoBehaviour
{
    //public GameObject mudAffect;
    //public GameObject playerObject;
    //public float playerMovSpeed;
    public FirstPersonController player;

    // Start is called before the first frame update
    void Start()
    {
        //playerMovSpeed = GameObject.Find("PlayerCapsule").GetComponent<FirstPersonController>().MoveSpeed;
        //playerScript = GameObject.GetComponent<FirstPersonController>().MoveSpeed;
        //playerMovSpeed = playerObject.GetComponent<FirstPersonController>.MoveSpeed;
        //playerMovSpeed = GameObject.Find("PlayerCapsule").GetComponent<FirstPersonController>.MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        Debug.Log("Player in on mud");
    //        Mud();
    //    }
    //}

    //void Mud()
    //{

    //}
    //void OnCollisionStay(Collision other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        player.MoveSpeed = 2f;
    //        player.SprintSpeed = 4f;
    //        //Debug.Log("standing on mud");
    //        //Debug.Log(MoveSpeed);
    //        //Debug.Log(SprintSpeed);
    //    }
    //}
    //private void OnCollisionExit(Collision other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        player.MoveSpeed = 6f;
    //        player.SprintSpeed = 8f;
    //        //Debug.Log("standing on mud");
    //        //Debug.Log(MoveSpeed);
    //        //Debug.Log(SprintSpeed);
    //    }
    //}
}
