using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
	public GameObject dudePrefab;

	private float difficultyMultiplier = 0.98f;
	private float spawnMinTimer = 2f;
	private float spawnMaxTimer = 4f;

	// Use this for initialization
	void Start () 
	{
		dudePrefab = (GameObject)Resources.Load ("Dude");
		StartCoroutine (SpawnEnemies());
	}

	IEnumerator SpawnEnemies()
	{
		while(true)
		{
			yield return new WaitForSeconds (Random.Range (spawnMinTimer, spawnMaxTimer));
			Instantiate (dudePrefab, SelectRandomPoint(), Quaternion.identity);

			spawnMinTimer *= difficultyMultiplier;
			spawnMaxTimer *= difficultyMultiplier;
		}
	}

	Vector3 SelectRandomPoint()
	{
		Vector2 randomPoint = Random.insideUnitCircle * 1.5f;

		float x = randomPoint.x + transform.position.x;
		float y = 0.1f;
		float z = randomPoint.y + transform.position.z;

		return new Vector3 (x, y, z);
	}
}
