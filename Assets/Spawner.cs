using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject boxPrefab;
    public Transform spawnPoint;
    public int numberOfBoxes = 10;
    public float spawnDelay = 2f;

    public void StartSpawning()
    {
        StartCoroutine(SpawnBoxes());
    }

    private IEnumerator SpawnBoxes()
    {
        for (int i = 0; i < numberOfBoxes; i++)
        {
            Instantiate(boxPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
