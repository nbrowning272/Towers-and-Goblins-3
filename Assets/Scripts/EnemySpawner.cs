using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] Spawners;
    public List<Goblin> Goblins = new List<Goblin>();
    public int currentWave;
    public int waveValue;
    public List<GameObject> goblinsToSpawn = new List<GameObject>();
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    private float waitTimer = 10;
    public GameObject blueGoblin;
    

    void Start()
    {
        GenerateWave();
        Instantiate(blueGoblin, new Vector3 (1000,1000,1000), Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //for (int i = 0; i < goblinsToSpawn.Count; i++)
        //{
        int randSpawner = Random.Range(0, Spawners.Length);
            if (spawnTimer <= 0)
            {
                //spawn an enemy
                if (goblinsToSpawn.Count > 0)
                {
                    Instantiate(goblinsToSpawn[0], Spawners[randSpawner].position, Quaternion.identity);
                    goblinsToSpawn.RemoveAt(0); // Remove goblin from list after it's spawned
                    spawnTimer = spawnInterval; // Reset time before spawn

                }
                else
                {
                    waveTimer = 0; // If list of goblins is exhausted set wave timer to zero
                    if (waveTimer <= 0 && currentWave < 5 && GameObject.FindGameObjectsWithTag("Goblin").Length == 1)
                    {
                        waitTimer = waitTimer -= Time.deltaTime;
                        if (waitTimer <= 0)
                        {
                            currentWave++;
                            waveDuration = 60;
                            GenerateWave();
                             waitTimer = 10;
                        }
                    }
            }
            }
              
            else
            {
                spawnTimer -= Time.fixedDeltaTime; 
                waveTimer -= Time.fixedDeltaTime;
            }
            
       // }
    }

    // Prepares wave and begins it
    public void GenerateWave()
    {
        waveValue = currentWave * 10; // Used to increase difficulty alongside the increase of level number.
        GenerateGoblins();
        spawnInterval = waveDuration / goblinsToSpawn.Count; // gives a fixed time between each enemy and spaces out the
                                                             // spawning of goblins to last the entire wave time.
        waveTimer = waveDuration;
    }
    public void GenerateGoblins()
    {
        // Create a temporary list of goblins to spawn
        // In a loop get a random goblin
        // test if the wave has space for it based on its 'cost'
        // if it does, add it to the list, and deduct the cost from the overall value of the wave
        // once there is no space left exit the loop
        List<GameObject> generatedGoblins = new List<GameObject>();
        while (waveValue > 0)
        {
            int randGoblinId = Random.Range(0, Goblins.Count);
            int randGoblinCost = Goblins[randGoblinId].cost;

            if (waveValue - randGoblinCost >= 0)
            {
                generatedGoblins.Add(Goblins[randGoblinId].goblinPrefab);
                waveValue -= randGoblinCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }

        }
        goblinsToSpawn.Clear();
        goblinsToSpawn = generatedGoblins;
    }
    [System.Serializable]
    public class Goblin
    {
        public GameObject goblinPrefab;
        public int cost;

    }
}
