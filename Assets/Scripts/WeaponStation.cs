using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStation : MonoBehaviour
{
    public string wText = "";
    [SerializeField] public Text weaponText;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        weaponText.text = wText;
    }

    // Update is called once per frame
    void Update()
    {
        weaponText.text = wText;
    }

}
