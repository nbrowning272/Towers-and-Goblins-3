using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallStation : MonoBehaviour
{
    public string wText = "";
    [SerializeField] public Text wallText;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        wallText.text = wText;
    }

    // Update is called once per frame
    void Update()
    {
        wallText.text = wText;
    }
}
