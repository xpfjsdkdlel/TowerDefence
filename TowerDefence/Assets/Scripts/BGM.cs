using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    public void Init()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    public void playBGM(AudioClip clip)
    {
        if (audioSource.clip == null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
    public void stopBGM()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }
}
