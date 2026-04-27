using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float speed = 1.0f;
    public GameObject[] cubes;

    void Update()
    {
        // Gerakkan konveyor
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Gerakkan setiap kubus
        foreach (GameObject cube in cubes)
        {
            cube.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}