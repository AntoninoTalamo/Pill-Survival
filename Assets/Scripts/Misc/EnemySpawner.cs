using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] EnemyTypes; // Array to hold different enemy types
    public float StartRate = 1.25f;
    private float rate;
    private float elapsedTime = 0f;

    public void Start()
    {
        rate = StartRate;
        InvokeRepeating("SpawnRepeating", rate, rate);
    }

    void SpawnRepeating()
    {
        elapsedTime += rate;
        int enemyTypeIndex = GetEnemyTypeIndex();
        CreateEnemiesAroundPoint(1, PlayerData.instance.PlayerEntity.EntityObject.transform.position, 15f, enemyTypeIndex);
    }

    private int GetEnemyTypeIndex()
    {
        // Gradually introduce new enemy types based on elapsed time
        int maxIndex = Mathf.FloorToInt(elapsedTime / 60); // New type every 60 seconds
        maxIndex = Mathf.Clamp(maxIndex, 0, EnemyTypes.Length - 1);

        // Randomly select from available types
        return Random.Range(0, maxIndex + 1);
    }

    public void CreateEnemiesAroundPoint(int num, Vector3 point, float radius, int enemyTypeIndex)
    {
        for (int i = 0; i < num; i++)
        {
            float radians = Random.Range(0f, 2f * Mathf.PI);
            float vertical = Mathf.Sin(radians);
            float horizontal = Mathf.Cos(radians);
            Vector3 spawnDir = new Vector3(horizontal, 0, vertical);
            Vector3 spawnPos = point + spawnDir * radius;

            GameObject enemyToSpawn = EnemyTypes[enemyTypeIndex];
            GameObject enemy = ObjectPool.instance.PullObject(enemyToSpawn.name);
            if (enemy != null)
                enemy.transform.position = spawnPos;
        }
    }
}
