using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;
    
    private AudioSource controlAudio;

    private void Awake()
    {
        controlAudio = GetComponent<AudioSource>();
    }

    public void SelectAudio(int index, float volume)
    {
        controlAudio.PlayOneShot(audios[index], volume);
    }

    public void IsLooping(bool state)
    {
        controlAudio.loop=state;
    }

    public void SelecAudioLoop(int index, float volume)
    {
        controlAudio.clip = audios[index];
        controlAudio.volume = volume;
        controlAudio.Play();
    }
}
