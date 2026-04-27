using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PhysicalButton : MonoBehaviour
{
    [Header("Button Setup")]
    public Transform pressTransform;
    public float pressThreshold = 0.01f;
    public UnityEvent onPressed;
    public UnityEvent onReleased;

    [Header("Cube Spawn")]
    public GameObject cubePrefab;
    public Transform cubeSpawnPoint;
    public int cubeCount = 10;

    private Vector3 initialPosition;
    private bool isPressed = false;
    private List<GameObject> spawnedCubes = new List<GameObject>();

    void Start()
    {
        initialPosition = pressTransform.localPosition;
    }

    void Update()
    {
        float displacement = initialPosition.y - pressTransform.localPosition.y;

        if (!isPressed && displacement >= pressThreshold)
        {
            isPressed = true;
            Debug.Log("Tombol DITEKAN");
            onPressed.Invoke();
            ClearCubes();     // Bersihkan cube lama
            SpawnCubes();     // Respawn cube baru
        }

        if (isPressed && displacement < pressThreshold * 0.5f)
        {
            isPressed = false;
            Debug.Log("Tombol DILEPAS");
            onReleased.Invoke();
        }
    }

    private void SpawnCubes()
    {
        if (cubePrefab == null || cubeSpawnPoint == null)
        {
            Debug.LogWarning("Prefab Cube / SpawnPoint tidak diassign!");
            return;
        }

        for (int i = 0; i < cubeCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            GameObject cube = Instantiate(cubePrefab, cubeSpawnPoint.position + offset, Quaternion.identity);
            spawnedCubes.Add(cube);
        }
    }

    private void ClearCubes()
    {
        foreach (GameObject cube in spawnedCubes)
        {
            if (cube != null) Destroy(cube);
        }
        spawnedCubes.Clear();
    }
}
