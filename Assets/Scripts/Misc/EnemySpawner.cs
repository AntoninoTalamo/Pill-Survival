using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;

    public void Start()
    {
		InvokeRepeating("SpawnRepeating", 5f, 5f);
    }

	void SpawnRepeating()
    {
		CreateEnemiesAroundPoint(5, PlayerData.instance.PlayerEntity.EntityObject.transform.position, 15f);
    }

    public void CreateEnemiesAroundPoint(int num, Vector3 point, float radius)
	{

		for (int i = 0; i < num; i++)
		{

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
}
