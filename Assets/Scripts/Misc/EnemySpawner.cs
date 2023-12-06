using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float initialSpawnRate = 5f;
    public float minSpawnRate = 1f; // Minimum spawn rate
    public float spawnRateDecrease = 0.1f;
    public int initialNumberOfEnemies = 1;
    public int maxNumberOfEnemies = 150; // Maximum number of enemies
    public int enemyIncreaseInterval = 30;
    public int phaseInterval = 60;

    private float nextIncreaseTime;
    private float currentSpawnRate;
    private int currentNumberOfEnemies;
    private List<float> spawnWeights;

    private void Start()
    {
        currentSpawnRate = initialSpawnRate;
        currentNumberOfEnemies = initialNumberOfEnemies;
        InitializeSpawnWeights();
        nextIncreaseTime = Time.time + enemyIncreaseInterval;
        InvokeRepeating("SpawnRepeating", currentSpawnRate, currentSpawnRate);
    }

    private void InitializeSpawnWeights()
    {
        spawnWeights = new List<float>();
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            spawnWeights.Add(0f);
        }
        spawnWeights[0] = 1f; // Start with only the first enemy type
    }

    private void Update()
    {
        if (Time.time >= nextIncreaseTime)
        {
            IncreaseDifficulty();
            nextIncreaseTime += enemyIncreaseInterval;
        }

        if (Time.time >= phaseInterval * (spawnWeights.Count - 1) && spawnWeights[spawnWeights.Count - 1] < 1f)
        {
            AdjustSpawnWeights();
        }
    }

    private void AdjustSpawnWeights()
    {
        for (int i = 0; i < spawnWeights.Count - 1; i++)
        {
            if (spawnWeights[i] > 0f)
            {
                spawnWeights[i] = Mathf.Max(spawnWeights[i] - 0.05f, 0f); // Smoother decrease
                spawnWeights[i + 1] = Mathf.Min(spawnWeights[i + 1] + 0.05f, 1f); // Smoother increase
                break;
            }
        }
    }

    void SpawnRepeating()
    {
        CreateEnemiesAroundPoint(currentNumberOfEnemies, PlayerData.instance.PlayerEntity.EntityObject.transform.position, 15f);
        if (currentSpawnRate > minSpawnRate)
        {
            CancelInvoke("SpawnRepeating");
            currentSpawnRate -= spawnRateDecrease;
            InvokeRepeating("SpawnRepeating", currentSpawnRate, currentSpawnRate);
        }
    }

    private void IncreaseDifficulty()
    {
        currentNumberOfEnemies = Mathf.Min(currentNumberOfEnemies + 1, maxNumberOfEnemies);
    }

    public void CreateEnemiesAroundPoint(int num, Vector3 point, float radius)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject enemyPrefab = SelectEnemyBasedOnWeight();
            /* Random angle around the circle */
			var radians = Random.Range(0f, 2f * Mathf.PI);

			/* Get the vector direction */
			var vertical = Mathf.Sin(radians);
			var horizontal = Mathf.Cos(radians);

			var spawnDir = new Vector3(horizontal, 0, vertical);

			/* Get the spawn position */
			var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

			/* Now spawn */
			var enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity) as GameObject;

			/* Adjust height */
			enemy.transform.Translate(new Vector3(0, enemy.transform.localScale.y / 2, 0));
        }
    }

    private GameObject SelectEnemyBasedOnWeight()
    {
        float totalWeight = 0f;
        foreach (var weight in spawnWeights)
        {
            totalWeight += weight;
        }

        float randomPoint = Random.value * totalWeight;

        for (int i = 0; i < spawnWeights.Count; i++)
        {
            if (randomPoint < spawnWeights[i])
                return enemyPrefabs[i];
            randomPoint -= spawnWeights[i];
        }

        return enemyPrefabs[0]; // Default return, should not normally reach here
    }
}
