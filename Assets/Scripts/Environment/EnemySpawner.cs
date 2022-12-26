using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float minX = -10;
    public float maxX = 10;
    public float minY = -10;
    public float maxY = 10;

    public List<GameObject> spawnedEnemies = new List<GameObject>();
    private Map map;

    public delegate void enemiesDeadAction();
    public event enemiesDeadAction onEnemiesDead;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
    }

    public void SpawnEnemy(GameObject enemyToSpawn)
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        // Get the enemy's collider size from the prefab
        CircleCollider2D enemyCollider = enemyToSpawn.GetComponent<CircleCollider2D>();

        // Keep generating new coordinates until we find a suitable location
        while (true)
        {
            // Perform a CircleCast from the enemy's position to check if it hits any obstacles
            RaycastHit2D hit = Physics2D.CircleCast(new Vector2(x, y), enemyCollider.radius, Vector2.zero, 0, LayerMask.GetMask("Obstacle"));
            if (!hit)
            {
                // No obstacles were hit
                break;
            }

            // Generate new coordinates and try again
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);
        }

        // Instantiate the enemy prefab at the generated coordinates
        GameObject enemyInstance = Instantiate(enemyToSpawn, new Vector2(x, y), Quaternion.identity, transform);
        spawnedEnemies.Add(enemyInstance);
        enemyInstance.GetComponent<Health>().onDeath += enemyDies;
    }

    private void enemyDies(GameObject e)
    {
        spawnedEnemies.Remove(e);
        e.GetComponent<Health>().onDeath -= enemyDies;
        if (spawnedEnemies.Count == 0)
            if (onEnemiesDead != null)
                onEnemiesDead();
    }

    private void OnDisable()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            spawnedEnemies[i].GetComponent<Health>().onDeath -= enemyDies;
            Destroy(spawnedEnemies[i]);
        }
        spawnedEnemies.Clear();
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
