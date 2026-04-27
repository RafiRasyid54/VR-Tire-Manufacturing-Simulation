using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    public GameObject button; // drag tombol di sini dari Inspector
    public UnityEvent onPress;
    public UnityEvent onRelease;

    private GameObject _presser;
    AudioSource sound;
    private bool _isPressed;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        _isPressed = false;
    }

    private void OnTriggerEnter(Collider other)

    {
        Debug.Log("Masuk" + other.tag);
        if (!_isPressed && other.CompareTag("Hand"))
        {
            button.transform.localPosition = new Vector3(-0.8009067f, -0.452f, -0.3962055f);
            _presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            _isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            button.transform.localPosition = new Vector3(-0.8009067f, -0.408f, -0.3962055f);
            onRelease.Invoke();
            _isPressed = false;
        }
    }

    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 1, 2);
        sphere.AddComponent<Rigidbody>();
    }
}
