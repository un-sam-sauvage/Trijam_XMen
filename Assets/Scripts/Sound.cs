using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;
    
    // Clip audio
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    public bool loop;

    // AudioSource du son (caché dans l'inspector)
    [HideInInspector]
    public AudioSource source;
}