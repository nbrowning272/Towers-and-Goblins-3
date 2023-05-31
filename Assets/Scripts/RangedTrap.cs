using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTrap : MonoBehaviour
{
    public GameObject ghostObject;
    public GameObject placedObject;
    public int traps = 0;
    public bool ghostTrap = false;
    public PlaceObject placeObject;
    public HUD hud;
    public bool trapInstruction;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ghostObject, new Vector3(14.8f, 12f, 1156f), Quaternion.Euler(0, 0, 0));
        ghostObject = GameObject.FindWithTag("GhostRangedTrap");
        ghostObject.SetActive(false);
        placeObject = GetComponent<PlaceObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!trapInstruction && !placeObject.trapInstruction)
        //{
        //    hud.instructionText = "1 - Lightning Trap\n2- Ranged Trap";
        //}
        //else if (trapInstruction == true)
        //{
        //    hud.instructionText = "LMB - Place Trap";
        //}
        RaycastHit hit;
        if (CheckForTraps())
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 15f))
            {
                if (hit.collider.gameObject.tag == "Plane")
                {
                    if (traps > 0)
                    {
                        ghostObject.transform.position = hit.point;
                        if (Input.GetMouseButtonDown(1) && ghostTrap == true)
                        {
                            Instantiate(placedObject, ghostObject.transform.position, ghostObject.transform.rotation);
                            traps--;
                        }
                    }

                }
            }
        }
    }

    public bool CheckForTraps()
    {
        if (traps > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && ghostTrap == false && placeObject.ghostTrap == false)
            {
                //trapInstruction = true;
                ghostObject.SetActive(true);
                ghostTrap = true;
                return true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && ghostTrap == true && placeObject.ghostTrap == false)
            {
                ghostObject.SetActive(false);
                ghostTrap = false;
                return false;

            }
            else
            {
                return true;
            }

        }
        else
        {
            ghostTrap = false;
            ghostObject.SetActive(false);
            return false;
        }
        
    }
    

}
        
