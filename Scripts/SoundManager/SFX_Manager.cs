using UnityEngine;
using UnityEngine.Audio;
using System;

public class SFX_Manager : MonoBehaviour {

    public Sound[] sounds;

    public static SFX_Manager instance; // to avoid multiple instances of this gameobject in scenes

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // if scenes are changed and sound is playing, dont destroy and restart again. Using Prefab SFX_Manager(GameObject)

        //add an audio source in the inspector for every sound on Awake
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
        Debug.Log("played: " + name);
    }


    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        s.source.Stop();
        Debug.Log("stopped: " + name);
    }
}
