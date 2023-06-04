using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    public Button thisButton;
    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        //SceneManager.LoadScene("SceneNameHere");
        SceneManager.LoadScene("NewHowToPlayScene");
        Debug.Log("button pressed");
    }
}
