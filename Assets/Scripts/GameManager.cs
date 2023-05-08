using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float salvage = 0f;
    public Sword sword;
    public float wallHealth = 10f;
    public float maxWallHealth = 10f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (wallHealth < 1)
        {
            SceneManager.LoadScene(sceneName: "GameOverScene");
        }
    }

}
