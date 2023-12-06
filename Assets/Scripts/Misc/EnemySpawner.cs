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
        ScaleDifficulty();
        int enemyTypeIndex = GetEnemyTypeIndex();
        CreateEnemiesAroundPoint(1, PlayerData.instance.PlayerEntity.EntityObject.transform.position, 15f, enemyTypeIndex);
    }

    // Scale the difficulty by adjusting the spawn rate
    private void ScaleDifficulty()
    {
        // Adjust the rate more gradually
        if (elapsedTime < 300) // Scale up to 5 minutes
        {
            float rateDecrease = (StartRate - 0.5f) * (elapsedTime / 300); // More gradual decrease
            rate = StartRate - rateDecrease;
            CancelInvoke("SpawnRepeating");
            InvokeRepeating("SpawnRepeating", rate, rate);
        }
    }

    // Determine which enemy type to spawn based on elapsed time
    private int GetEnemyTypeIndex()
    {
        float timeFactor = elapsedTime / 300; // Scale factor based on time
        for (int i = EnemyTypes.Length - 1; i >= 0; i--)
        {
            if (Random.Range(0f, 1f) < timeFactor * (i + 1) / EnemyTypes.Length)
            {
                return i;
            }
        }
        return 0;
    }

    public void CreateEnemiesAroundPoint(int num, Vector3 point, float radius, int enemyTypeIndex)
    {
        for (int i = 0; i < num; i++)
        {
            float radians = Random.Range(0f, 2f * Mathf.PI);

			/* Get the vector direction */
			float vertical = Mathf.Sin(radians);
			float horizontal = Mathf.Cos(radians);

			Vector3 spawnDir = new Vector3(horizontal, 0, vertical);

			/* Get the spawn position */
			Vector3 spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            // Use enemyTypeIndex to spawn different enemy types
            GameObject enemyToSpawn = EnemyTypes[enemyTypeIndex];
            GameObject enemy = ObjectPool.instance.PullObject(enemyToSpawn.name);
            if (enemy != null)
                enemy.transform.position = spawnPos;
        }
    }
}