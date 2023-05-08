using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class StartGameButton : MonoBehaviour
{

    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            SceneManager.LoadScene("MainMenuScene Designer");
        }
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("DesignerScene");
        Debug.Log("Button pressed");
    }
}