using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuText;
    public GameObject pauseMenuPanel;
    public GameObject pauseMenuButton1;
    public GameObject pauseMenuButton2;
    public GameObject pauseMenuButton3;
    //public GameObject pauseMenuButton4;

    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuText.SetActive(false);
        pauseMenuPanel.SetActive(false);
        pauseMenuButton1.SetActive(false);
        pauseMenuButton2.SetActive(false);
        pauseMenuButton3.SetActive(false);
        //pauseMenuButton4.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                isPaused = false;
                Time.timeScale = 1f;
                pauseMenuText.SetActive(false);
                pauseMenuPanel.SetActive(false);
                pauseMenuButton1.SetActive(false);
                pauseMenuButton2.SetActive(false);
                pauseMenuButton3.SetActive(false);
                //pauseMenuButton4.SetActive(false);
            }
            else
            {
                isPaused = true;
                Time.timeScale = 1f;
                pauseMenuText.SetActive(true);
                pauseMenuPanel.SetActive(true);
                pauseMenuButton1.SetActive(true);
                pauseMenuButton2.SetActive(true);
                pauseMenuButton3.SetActive(true);
                //pauseMenuButton4.SetActive(true);
            }
        }
    }
}