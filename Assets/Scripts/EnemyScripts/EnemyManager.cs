using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {
    //public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public GameObject enemyParent;
    private int zombieSpawn;
    private int currentLevel;
    private bool levelOver;
    private GameController gameController;

    private const int MAX_ZOMBIE_COUNT = 10;

    public int zombieCounter;

    void Start() {
        zombieCounter = 1;
        currentLevel = 1;
        enemyParent = GameObject.Find("Enemys");
        zombieSpawn = 5;
        levelOver = false;
        gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        GenerateLevel();

    }

    void GenerateLevel() {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Update() {
        if(enemyParent.transform.childCount == 0 && levelOver) {
            zombieCounter = 1;
            zombieSpawn += 5;
            currentLevel++;
            gameController.updateLevel();
            levelOver = false;
            GenerateLevel();

        }
    }

    void Spawn() {
        //if (playerHealth.currentHealth <= 0f) {
        //    return;
        //}
        if (zombieCounter <= zombieSpawn) {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation, enemyParent.transform);
            zombieCounter++;
        } else {
            levelOver = true;
        }
    }
}
