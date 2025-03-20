using UnityEngine;
using System.Collections.Generic;

public class XRayRandomizer : MonoBehaviour
{
    public GameObject[] safeItems;
    public GameObject[] dangerousItems;
    public Transform spawnArea;

    public int minItems = 3;
    public int maxItems = 6;
    public int maxDangerousItems = 2;

    public float spawnWidth = 5f;  // Width of the spawn area
    public float spawnHeight = 3f; // Height of the spawn area
    public float minSpawnDistance = 1f; // Minimum distance between spawned items

    private List<GameObject> spawnedItems = new List<GameObject>();

    void Start()
    {
        GenerateRandomItems();
    }

    void GenerateRandomItems()
    {
        ClearItems();
        int totalItems = Random.Range(minItems, maxItems + 1);
        int dangerousItemCount = Mathf.Min(Random.Range(0, maxDangerousItems + 1), totalItems);
        int safeItemCount = totalItems - dangerousItemCount;

        List<GameObject> itemsToSpawn = new List<GameObject>();
        itemsToSpawn.AddRange(GetRandomObjects(dangerousItems, dangerousItemCount));
        itemsToSpawn.AddRange(GetRandomObjects(safeItems, safeItemCount));

        foreach (GameObject itemPrefab in itemsToSpawn)
        {
            Vector2 randomPosition = GetValidRandomPosition();
            GameObject newItem = Instantiate(itemPrefab, randomPosition, Quaternion.identity, spawnArea);
            spawnedItems.Add(newItem);
        }
    }

    List<GameObject> GetRandomObjects(GameObject[] sourceArray, int count)
    {
        List<GameObject> selected = new List<GameObject>();
        List<GameObject> available = new List<GameObject>(sourceArray);

        for (int i = 0; i < count && available.Count > 0; i++)
        {
            int index = Random.Range(0, available.Count);
            selected.Add(available[index]);
            available.RemoveAt(index);
        }

        return selected;
    }

    Vector2 GetValidRandomPosition()
    {
        int maxAttempts = 10; // Maximum attempts to find a valid position
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(spawnArea.position.x - spawnWidth / 2, spawnArea.position.x + spawnWidth / 2),
                Random.Range(spawnArea.position.y - spawnHeight / 2, spawnArea.position.y + spawnHeight / 2)
            );

            // Check if this position is far enough from existing items
            bool valid = true;
            foreach (GameObject item in spawnedItems)
            {
                if (Vector2.Distance(randomPosition, item.transform.position) < minSpawnDistance)
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
                return randomPosition;
        }

        // If all attempts fail, return a random position anyway (fallback)
        return new Vector2(
            Random.Range(spawnArea.position.x - spawnWidth / 2, spawnArea.position.x + spawnWidth / 2),
            Random.Range(spawnArea.position.y - spawnHeight / 2, spawnArea.position.y + spawnHeight / 2)
        );
    }

    void ClearItems()
    {
        foreach (GameObject item in spawnedItems)
        {
            Destroy(item);
        }
        spawnedItems.Clear();
    }
}
