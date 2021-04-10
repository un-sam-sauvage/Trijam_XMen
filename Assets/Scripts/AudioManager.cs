using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // tableau de sons
    public Sound[] sounds;
    public AudioMixerGroup audioMixerGroup;
    
    void Awake()
    {
        // Pour chaque son
        foreach (Sound sound in sounds)
        {
            // - Initialision du sound -
            // Ajout du component de type AudioSource dans sound.source
            sound.source = gameObject.AddComponent<AudioSource>();
            // Alimentation du source
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    private Sound GetSoundFromAudioManager(string name) 
    { 
        // Recherche d'un son dans le tableau sounds grâce au nom 
        Sound s = Array.Find(sounds, sound => sound.name == name); // Lambda expression ( ... => ...) 
        return s; 
    } 
 
    public void Play(string name) 
    { 
        Sound s = GetSoundFromAudioManager(name); 
         
        // Lancement du son 
        s.source.Play(); 
    } 
 
    public void Stop(string name) 
    { 
        Sound s = GetSoundFromAudioManager(name); 
        // Arrêt du son 
        s.source.Stop(); 
    } 
}