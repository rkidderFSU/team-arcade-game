using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public GameManager manager;
    AudioSource audioSource;
    public AudioClip splashSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(splashSound, 1f);
            manager.GameOver();
        }
    }
}
