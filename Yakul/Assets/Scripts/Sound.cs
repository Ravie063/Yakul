using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public string BGMOrSFX;
    public AudioClip clip;
    [Range(0,1)]
    public float volume = 1;
    [Tooltip("Frequency of sound, use this to slow down or " +
        "speed up the sound")]
    [Range(.1f,3)]
    public float pitch = 1;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
