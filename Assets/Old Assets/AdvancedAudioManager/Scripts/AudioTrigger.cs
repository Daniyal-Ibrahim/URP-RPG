using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    // All our variables...
    [Header("Audio Trigger")]
    public string soundToTrigger;
    public string[] objectTagsToTriggerSound;
    private AdvancedAudioManager audioManager;
    public float delay = 0f;

    // Runs before the first frame...
    private void Start()
    {
        // Finds Advanced Audio Manager in our scene...
        audioManager = FindObjectOfType<AdvancedAudioManager>();
    }

    // Checks if we hit something... (3D)
    private void OnTriggerEnter(Collider other)
    {
        // Loop till we've checked all tags...
        for (int i = 0; i < objectTagsToTriggerSound.Length; i++)
        {
            // If the thing we hit had a tag that we specified...
            if (other.gameObject.CompareTag(objectTagsToTriggerSound[i]))
            {
                // Start a coroutine to play the sound...
                StartCoroutine("PlaySound");
            }
        }
    }

    //Checks if we hit something... (2D)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Loop till we've checked all tags...
        for (int i = 0; i < objectTagsToTriggerSound.Length; i++)
        {
            // If the thing we hit had a tag that we specified...
            if (collision.gameObject.CompareTag(objectTagsToTriggerSound[i]))
            {
                // Start a coroutine to play the sound...
                StartCoroutine("PlaySound");
            }
        }
    }

    // Plays a sound...
    IEnumerator PlaySound()
    {
        // Wait however long the delay is set to...
        yield return new WaitForSeconds(delay);
        // Play the sound!
        audioManager.PlaySound(soundToTrigger);
    }
}
