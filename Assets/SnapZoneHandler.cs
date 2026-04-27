using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SnapZoneHandler : MonoBehaviour
{
    [Header("Snap Settings")]
    public string snapTag = "Snappable"; // Tag objek yang bisa snap
    public Vector3 snapOffset = Vector3.zero; // Offset posisi setelah snap
    public bool snapRotation = true; // Apakah objek harus dirotasi sesuai snap zone
    public Vector3 rotationOffset = Vector3.zero; // Offset rotasi setelah snap
    
    [Header("Debug")]
    public Color gizmoColor = new Color(0, 1, 0, 0.5f); // Warna untuk debug gizmo
    
    private GameObject snappedObject;
    private Vector3 originalScale;
    private Rigidbody snappedRigidbody;

    private void OnTriggerEnter(Collider other)
    {
        // Jika sudah ada objek yang snapped atau objek tidak memiliki tag yang sesuai, abaikan
        if (snappedObject != null || !other.CompareTag(snapTag)) return;
        
        SnapObject(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        // Jika objek yang keluar adalah objek yang snapped, lepaskan snap
        if (other.gameObject == snappedObject)
        {
            ReleaseObject();
        }
    }

    public void SnapObject(GameObject objectToSnap)
    {
        snappedObject = objectToSnap;
        originalScale = snappedObject.transform.localScale;
        
        // Simpan rigidbody jika ada
        snappedRigidbody = snappedObject.GetComponent<Rigidbody>();
        if (snappedRigidbody != null)
        {
            snappedRigidbody.isKinematic = true;
        }
        
        // Set parent dan posisi
        snappedObject.transform.SetParent(transform);
        snappedObject.transform.localPosition = snapOffset;
        
        // Set rotasi jika diperlukan
        if (snapRotation)
        {
            snappedObject.transform.localEulerAngles = rotationOffset;
        }
        
        // Notifikasi objek bahwa dia telah di-snap (jika memiliki script yang sesuai)
        snappedObject.SendMessage("OnSnapped", this, SendMessageOptions.DontRequireReceiver);
    }

    public void ReleaseObject()
    {
        if (snappedObject == null) return;
        
        // Kembalikan scale asli
        snappedObject.transform.localScale = originalScale;
        
        // Lepaskan parent
        snappedObject.transform.SetParent(null);
        
        // Aktifkan kembali rigidbody jika ada
        if (snappedRigidbody != null)
        {
            snappedRigidbody.isKinematic = false;
        }
        
        // Notifikasi objek bahwa dia telah dilepas (jika memiliki script yang sesuai)
        snappedObject.SendMessage("OnReleased", this, SendMessageOptions.DontRequireReceiver);
        
        snappedObject = null;
        snappedRigidbody = null;
    }

    public bool HasSnappedObject()
    {
        return snappedObject != null;
    }

    public GameObject GetSnappedObject()
    {
        return snappedObject;
    }

    // Untuk debug, gambar gizmo yang menunjukan area snap
    private void OnDrawGizmos()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider == null) return;
        
        Gizmos.color = gizmoColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(collider.center, collider.size);
    }
}