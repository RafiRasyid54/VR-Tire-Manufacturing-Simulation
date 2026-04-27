using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;     // Prefab cube
    public Transform spawnPoint;      // Posisi spawn
    public SnapZoneHandler targetSnapZone;   // Reference ke SnapZone yang akan dicek

    public void SpawnCube()
    {
        // Cek jika SnapZone terisi
        if (targetSnapZone != null && targetSnapZone.HasSnappedObject())
        {
            Debug.Log("Tidak bisa spawn cube karena SnapZone sudah terisi");
            return;
        }

        if (cubePrefab != null && spawnPoint != null)
        {
            // Spawn cube dengan rotasi default
            GameObject newCube = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity);

            // Pastikan Rigidbody ada dan gunakan gravitasi
            Rigidbody rb = newCube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
            }
        }
        else
        {
            Debug.LogWarning("Cube prefab atau spawn point belum diset di Inspector.");
        }
    }

    // Method untuk mengecek apakah bisa spawn (bisa dipanggil dari UI)
    public bool CanSpawn()
    {
        return targetSnapZone == null || !targetSnapZone.HasSnappedObject();
    }
}