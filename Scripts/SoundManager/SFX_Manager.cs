using UnityEngine;
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

            //when paused is pressed, AudioListener pauses all sfx sounds, meaning the options buttons have no sounds. 
            //Using ignoreListenerPause to ignore pause.
            //s.source.ignoreListenerPause = true;  

            //every sfx sound gets paused except buttonsound and pause, so sound effect can be heared when button is clicked or escape is pressed
            //did this to ignore female time counter warning
            if (s.name == "buttonSound" )
            {
                s.source.ignoreListenerPause = true;
            }
            if (s.name == "pause")
            {
                s.source.ignoreListenerPause = true;
            }
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

    public void Volume(float _volume)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = _volume;
        }
    }
}
