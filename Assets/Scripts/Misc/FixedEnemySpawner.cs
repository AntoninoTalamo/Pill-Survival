using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedEnemySpawner : MonoBehaviour
{
	public GameObject EnemyToSpawn;
	public float Rate = 5f;

	public void Start()
	{
		InvokeRepeating("SpawnRepeating", Rate, Rate);
	}

	void SpawnRepeating()
	{
		CreateEnemiesAroundPoint(1, PlayerData.instance.PlayerEntity.EntityObject.transform.position, 15f);
	}

	public void CreateEnemiesAroundPoint(int num, Vector3 point, float radius)
	{

		for (int i = 0; i < num; i++)
		{

			/* Random angle around the circle */
			float radians = Random.Range(0f, 2f * Mathf.PI);

			/* Get the vector direction */
			float vertical = Mathf.Sin(radians);
			float horizontal = Mathf.Cos(radians);

			Vector3 spawnDir = new Vector3(horizontal, 0, vertical);

			/* Get the spawn position */
			Vector3 spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

			/* Now spawn */
			GameObject enemy = ObjectPool.instance.PullObject(EnemyToSpawn.name);
			if(enemy != null)
				enemy.transform.position = spawnPos;
		}
	}
}
