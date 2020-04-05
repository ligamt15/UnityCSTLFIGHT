using System.Collections;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{

    public int WaveSize;
    public GameObject EnemyPrefab;
    public float EnemyInterval;
    public Transform spawnPoint;
    public float StartTime;
    int enemyCount = 0;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", StartTime, EnemyInterval);
        
    }

    void Update()
    {
        if (enemyCount == WaveSize)
        {
            CancelInvoke("SpawnEnemy");

        }
    }

    void SpawnEnemy()
    {
        enemyCount++;
        GameObject Enemy = GameObject.Instantiate(EnemyPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
    }




}
