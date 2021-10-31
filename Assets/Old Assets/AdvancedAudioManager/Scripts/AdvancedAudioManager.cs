using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    // All our variables...
    [Header("Sound Settings")]
    public string name;
    public AudioClip clip;
    public bool mute = false;
    public bool bypassEffects = false;
    public bool bypassListenerEffects = false;
    public bool bypassReverbZones = false;
    public bool playOnAwake = true;
    public bool Loop = false;    
    [Range(0, 256)]
    public int priority = 128;
    [Range(0f, 1f)]
    public float volume = 1.0f;
    [Range(-3f, 3f)]
    public float pitch = 1.0f;
    [Range(0f, 0.5f)]
    public float randomVolume = 0.0f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.0f;
    [Range(-1f, 1f)]
    public float stereoPan = 0.0f;
    [Range(0f, 1f)]
    public float spatialBlend = 0.0f;
    [Range(0f, 1.1f)]
    public float reverbZoneMix = 1.0f;
    private AudioSource source;

    // Sets the audio source settings for this sound...
    public void SetSource(AudioSource _source)
    {
        // Sets settings...
        source = _source;
        source.clip = clip;
        source.priority = priority;
        source.panStereo = stereoPan;
        source.spatialBlend = spatialBlend;
        source.reverbZoneMix = reverbZoneMix;
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f)); ;
        source.loop = Loop;
        
        // If play on awake is true...
        if (playOnAwake)
        {
            // Play sound!
            Play();
        }
    }

    // Plays sound...
    public void Play()
    {
        // Sets settings...
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        // Plays sound!
        source.Play();
    }

    // Stops sound...
    public void Stop()
    {
        // Stops sound!
        source.Stop();
    }
}

public class AdvancedAudioManager : MonoBehaviour
{
    // Our Advanced Audio Manager, makes it easy to use from other scripts...
    public static AdvancedAudioManager instance;

    // Runs when starting up...
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
        // If the instance isn't empty...
        if (instance != null)
        {
            // Throw an error that there must be another Advanced Audio Manager...
            Debug.LogError("More than one AdvancedAudioManager in the scene.");
        }

        // Otherwise...
        else
        {
            // Set instance to this script...
            instance = this;
        }

    }

    // All our variables...
    [Header("Advanced Audio Manager")]
    [SerializeField]
    Sound[] sounds;

    // Runs before first frame...
    void Start()
    {
        // Loop through all of our sounds...
        for (int i = 0; i < sounds.Length; i++)
        {
            // Make a new AudioSource for each of them...
            var source = gameObject.AddComponent<AudioSource>();
            // Set up the AudioSource with our settings.
            sounds[i].SetSource(source);
        }
    }

    // Plays a sound defined by a string...
    public void PlaySound(string _name)
    {
        // Loop through all our sounds...
        for (int i = 0; i < sounds.Length; i++)
        {
            // If one's name matches the string we sent in...
            if (sounds[i].name == _name)
            {
                // Play it!
                sounds[i].Play();
                return;
            }
        }

        // Throw an error that there was no sound matching the name we sent in...
        Debug.LogWarning("AdvancedAudioManager: Sound not found in list, " + _name);
    }

    // Stops a sound defined by a string...
    public void StopSound(string _name)
    {
        // Loop through all our sounds...
        for (int i = 0; i < sounds.Length; i++)
        {
            // If one's name matches the string we sent in...
            if (sounds[i].name == _name)
            {
                // Stop it!
                sounds[i].Stop();
                return;
            }
        }

        // Throw an error that there was no sound matching the name we sent in...
        Debug.LogWarning("AdvancedAudioManager: Sound not found in list, " + _name);
    }
}
