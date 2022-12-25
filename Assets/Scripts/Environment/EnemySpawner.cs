using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minX = -10;
    public float maxX = 10;
    public float minY = -10;
    public float maxY = 10;

    public List<GameObject> spawnedEnemies;
    private Map map;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        resetList();
    }

    public void resetList()
    {

    }

    void SpawnEnemy()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        // Keep generating new coordinates until we find a suitable location
        while (Physics2D.OverlapPoint(new Vector2(x, y), LayerMask.GetMask("Obstacle")))
        {
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);
        }

        // Instantiate the enemy prefab at the generated coordinates
        GameObject enemy = Instantiate(enemyPrefab, new Vector2(x, y), Quaternion.identity);
        spawnedEnemies.Add(enemy);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 bottomLeft = new Vector3(minX, minY, 0);
        Vector3 topLeft = new Vector3(minX, maxY, 0);
        Vector3 topRight = new Vector3(maxX, maxY, 0);
        Vector3 bottomRight = new Vector3(maxX, minY, 0);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }
}
