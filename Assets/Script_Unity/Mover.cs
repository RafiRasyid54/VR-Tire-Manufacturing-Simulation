using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 moveDirection = Vector3.forward;

    void Update()
    {
        // Gerakkan objek terus menerus sesuai arah saat ini
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        // Belok ke kanan saat menyentuh trigger dengan tag "TurnRight"
        if (other.CompareTag("TurnRight"))
        {
            moveDirection = Quaternion.Euler(0, 90, 0) * moveDirection;
        }
        // Belok ke kiri saat menyentuh trigger dengan tag "TurnLeft"
        else if (other.CompareTag("TurnLeft"))
        {
            moveDirection = Quaternion.Euler(0, -90, 0) * moveDirection;
        }
    }
}
