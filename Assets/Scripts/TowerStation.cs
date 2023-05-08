using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStation : MonoBehaviour
{
    public string tText = "";
    [SerializeField] public Text towerText;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        towerText.text = tText;
    }

    // Update is called once per frame
    void Update()
    {
        towerText.text = tText;
    }
}
