using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Modifier> modifiers = new List<Modifier>();
    [SerializeField]private List<GameObject> currentEnemies = new List<GameObject>();
    public List<GameObject> possibleEnemies;
    public int enemyQuantity = 5; // default to five, may get changed by items or difficulty
    [Space(10)]
    public int roomNumber;
    public GameObject doorInstance;
    [Space(10)]
    public GameObject doorPrefab;
    public Transform bottomDoorSpawns;
    public Transform topDoorSpawns;
    public Transform leftDoorSpawns;
    public Transform rightDoorSpawns;
    [SerializeField]private Map map;
    public int upgradesAmount = 2;
    public int downgradesAmount = 2;

    // done every time the room is enabled (when the player enters it)
    private void OnEnable() 
    {
        if (map == null)
            map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        if(doorInstance == null)
            loadDoor();

        if (possibleEnemies == null || possibleEnemies.Count <= 0) // Loads a basic list of enemies the first time the room is entered
        {
            possibleEnemies = new List<GameObject>();
            foreach (GameObject enemy in map.defaultEnemyList)
            {
                possibleEnemies.Add(enemy);
            }
        }
        resetRoom();
    }

    private void resetRoom()
    {
        doorInstance.GetComponent<Door>().isOpened = false;
        foreach (Modifier mod in modifiers)
        {
            Debug.Log("Did an effect : " + mod.name);
            mod.effect();
        }

        loadEnemies();
    }

    private void loadEnemies()
    {
        for(int i = 0; i < enemyQuantity; i++)
        {
            //Debug.Log("Spawned enemy " + i);
            GameObject enemyInstance = Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Count - 1)], transform);
            currentEnemies.Add(enemyInstance);
            enemyInstance.GetComponent<Health>().onDeath += enemyDies;
        }
    }

    private void enemyDies(GameObject e)
    {
        currentEnemies.Remove(e);
        e.GetComponent<Health>().onDeath -= enemyDies;
        if (currentEnemies.Count == 0)
            enemiesCleared();
    }

    private void enemiesCleared()
    {
        float radius = 1f;
        float angleStep = 360f / (upgradesAmount + downgradesAmount);
        List<GameObject> mods = new List<GameObject>();
        for(int i = 0; i < upgradesAmount; i++)
        {
            float angle = angleStep * i;
            float x = map.player.transform.position.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            float y = map.player.transform.position.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            GameObject go = Instantiate(map.upgrades[Random.Range(0, map.downgrades.Length)], new Vector2(x, y), Quaternion.identity);
            go.transform.parent = transform;
            mods.Add(go);
        }

        for (int i = 0; i < downgradesAmount; i++)
        {
            float angle = angleStep * i;
            float x = map.player.transform.position.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            float y = map.player.transform.position.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            GameObject go = Instantiate(map.downgrades[Random.Range(0, map.downgrades.Length)], new Vector2(x, y), Quaternion.identity);
            go.transform.parent = transform;
            mods.Add(go);
        }

        StartCoroutine(pickUpMods(mods));
    }

    IEnumerator pickUpMods(List<GameObject> mods)
    {
        int count = 0;
        while (count != downgradesAmount + upgradesAmount)
        {
            count = 0;
            foreach (GameObject go in mods)
            {
                if (go.GetComponent<Modifier>().isPicked)
                    count++;
            }
            yield return new WaitForEndOfFrame();
        }
        doorInstance.GetComponent<Door>().isOpened = true;
    }

    void loadDoor()
    {
        if (roomNumber == 1 || roomNumber == 8)
            spawnDoor(leftDoorSpawns.transform.GetChild(0).transform, leftDoorSpawns.transform.GetChild(1).transform, 1);
        else if (roomNumber == 2 || roomNumber == 3)
            spawnDoor(bottomDoorSpawns.transform.GetChild(0).transform, bottomDoorSpawns.transform.GetChild(1).transform, 0);
        else if (roomNumber == 4 || roomNumber == 5)
            spawnDoor(rightDoorSpawns.transform.GetChild(0).transform, rightDoorSpawns.transform.GetChild(1).transform, 1);
        else if (roomNumber == 6 || roomNumber == 7)
            spawnDoor(topDoorSpawns.transform.GetChild(0).transform, topDoorSpawns.transform.GetChild(1).transform, 0);
    }

    void spawnDoor(Transform p1, Transform p2, int dir) // dir = 0 for hor; 1 for ver
    {
        doorInstance = Instantiate(doorPrefab, transform);
        if (dir == 0)
        {
            doorInstance.transform.position = new Vector3(Random.Range(p1.position.x, p2.position.x), p1.position.y, p1.position.z);
        }
        else
        {
            doorInstance.transform.position = new Vector3(p1.position.x, Random.Range(p1.position.y, p2.position.y), p1.position.z);
        }
        doorInstance.transform.rotation = p1.rotation;
    }

    private void OnDisable()
    {
        for(int i = 0; i < currentEnemies.Count; i++)
        {
            enemyDies(currentEnemies[0]);
        }
    }
}
