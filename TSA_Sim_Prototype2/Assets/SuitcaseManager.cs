using UnityEngine;

public class SuitcaseManager : MonoBehaviour
{
    public GameObject[] suitcasePrefabs;
    public Transform spawnPoint;
    public Transform exitPoint;
    public static GameObject currentSuitcase;
    
    public bool preventDuplicateSpawn = true;
    private int lastSpawnedIndex = -1;

    void Start()
    {
        SpawnNewSuitcase();
    }

    public void SpawnNewSuitcase()
    {
        if (currentSuitcase != null)
        {
            Destroy(currentSuitcase); 
        }

 
        int randomIndex = Random.Range(0, suitcasePrefabs.Length);
        currentSuitcase = Instantiate(suitcasePrefabs[randomIndex], spawnPoint.position, Quaternion.identity);


        bool isDangerous = Random.value < 0.2f; 
        currentSuitcase.tag = isDangerous ? "DangerousItem" : "SafeItem";

        Debug.Log(isDangerous ? "Spawned a Dangerous Suitcase" : "Spawned a Safe Suitcase");
    }

    private void Update()
    {
        if (currentSuitcase != null && currentSuitcase.transform.position.x > exitPoint.position.x)
        {
            SpawnNewSuitcase();
        }
    }
}