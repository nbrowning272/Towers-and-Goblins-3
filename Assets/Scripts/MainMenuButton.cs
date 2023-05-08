using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public Button menuButton;
    // Start is called before the first frame update
    void Start()
    {
        menuButton.onClick.AddListener(TaskOnClick);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            SceneManager.LoadScene("MainMenuScene Designer");
        }
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("MainMenuScene Designer");
    }
}
