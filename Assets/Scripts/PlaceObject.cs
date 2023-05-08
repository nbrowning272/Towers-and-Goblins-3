using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public GameObject ghostObject;
    public GameObject placedObject;
    public int traps = 0;
    public bool ghostTrap = false;
    public RangedTrap rangedTrap;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ghostObject, new Vector3(14.8f, 12f, 1156f), Quaternion.Euler(0, 0, 0));
        ghostObject = GameObject.FindWithTag("Ghost Trap");
        ghostObject.SetActive(false);
        rangedTrap = GetComponent<RangedTrap>();
    }

    // Update is called once per frame
    void Update()
    {
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
            if (Input.GetKeyDown(KeyCode.Alpha1) && ghostTrap == false && rangedTrap.ghostTrap == false)
            {
                ghostObject.SetActive(true);
                ghostTrap = true;
                return true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) && ghostTrap == true && rangedTrap.ghostTrap == false)
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
