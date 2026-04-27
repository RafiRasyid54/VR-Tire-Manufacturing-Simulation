using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverTrigger : MonoBehaviour
{
    public Spawner spawner;

    private bool hasTriggered = false;

    // Fungsi yang bisa dipanggil saat tuas diaktifkan
    public void Activate()
    {
        if (hasTriggered) return;

        hasTriggered = true;
        spawner.StartSpawning();
    }

    // Fungsi opsional untuk mereset kondisi tuas
    public void ResetLever()
    {
        hasTriggered = false;
    }
}
