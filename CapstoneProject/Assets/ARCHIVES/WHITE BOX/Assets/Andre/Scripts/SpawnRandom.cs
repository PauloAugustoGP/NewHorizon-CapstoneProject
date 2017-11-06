using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpawnRandom : MonoBehaviour
{
    public int spawn;

    public GameObject[] enemyPrefab; //list of enemies we want to spawn to spawn random and random wander no Node points
    public List<Transform> enemySpawnPoint;    //list of spawn points to choose from 
                                               // [SerializeField] private int enemysToSpawn = 3; //number of enemies to spawn

    //***For spawning zombies***
    public int numEnemiesToSpawn = 3;
    public AndreEnemy theEnemyPrefab;
   

    private int numEnemies; //keep track of enemies we spawned

    public void SpawnEnemies()
    {
        numEnemies = 0;
        //List<Transform> temp = enemySpawnPoint; -for Waves
        //spawn enemies
        List<Transform> temp = enemySpawnPoint;
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            //spawn zombies
            int ran = Random.Range(0, temp.Count);
            AndreEnemy rb = Instantiate(theEnemyPrefab, temp[ran].position, temp[ran].rotation) as AndreEnemy;
            rb.currentNode = ran;
            rb.OwningSpawner = this;
            temp.RemoveAt(ran);
            numEnemies++;
        }
        
    }
    public void EnemyDied()
    {
        numEnemies--;
        if (numEnemies <= 0)
        {
            //we won
            SceneManager.LoadScene("PlayerWinsScene");
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}