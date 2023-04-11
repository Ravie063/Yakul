using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagerScript : MonoBehaviour
{
    public Sound[] sounds;

    //public static AudioManagerScript instance;
    private void Awake()
    {
        //if(instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Play("Menu");
        }
        else
        {
            int rand = UnityEngine.Random.Range(0, 2);
            if(rand == 0)
            {
                Play("Theme2");
            }
            else
            {
                Play("Theme");
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, find => find.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " +name + " doesnt exist");
            return;
        }
        else
        {
            s.source.Play();
        }
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, find => find.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " doesnt exist");
            return;
        }
        else
        {
            s.source.Stop();
        }
    }
    public void BGMorSFX(string name)
    {
        foreach(Sound s in sounds)
        {
            if(s.BGMOrSFX == name)
            {
                Volume(s);
            }
        }
    }
    void Volume(Sound s)
    {
        if (s.source.volume == 0)
        {
            s.source.volume = s.volume;
        }
        else
        {
            s.source.volume = 0;
        }
    }
}
