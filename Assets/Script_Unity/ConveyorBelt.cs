using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ConveyorBelt : MonoBehaviour
{
    [Header("Conveyor Settings")]
    public float conveyorSpeed = 2f;
    public Vector3 moveDirection = Vector3.forward;
    private bool isRunning = true;

    private Renderer rend;
    private Vector2 textureOffset = Vector2.zero;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogWarning("Renderer component not found on ConveyorBelt object!");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isRunning || collision.rigidbody == null) return;

        if (!collision.gameObject.CompareTag("Movable")) return;

        Vector3 worldDirection = transform.TransformDirection(moveDirection.normalized);
        collision.rigidbody.AddForce(worldDirection * conveyorSpeed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        if (!isRunning || rend == null || rend.material == null) return;

        textureOffset.x = Mathf.Repeat(textureOffset.x + conveyorSpeed * Time.deltaTime, 1);
        rend.material.mainTextureOffset = textureOffset;
    }

    public void ToggleConveyor()
    {
        isRunning = !isRunning;
    }

    public void SetConveyorState(bool isActive)
    {
        isRunning = isActive;
    }
}
