using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButtonComponent : MonoBehaviour
{
    [SerializeField] public AudioSource AudioPlayer;
    [SerializeField] public Image ImageComponent;
    [SerializeField] public Sprite IsPausedImage;
    [SerializeField] public Sprite IsPlayingImage;
    [SerializeField] public AudioListener Audio_Listener;

    // Start is called before the first frame update
    void Start()
    {
        if (!AudioPlayer)
            AudioPlayer = Camera.main.GetComponent<AudioSource>();

        if (!ImageComponent || !IsPausedImage || !IsPlayingImage || !AudioPlayer)
		{
            Debug.Log(gameObject.name + "MusicButtonComponent is missing references.");
            return;
		}
        UpdateButtonState();
        Audio_Listener = Camera.main.GetComponent<AudioListener>();
    }

    public void UpdateButtonState()
	{
        //ImageComponent.overrideSprite = (AudioListener.volume != 0.0f) ? IsPlayingImage : IsPausedImage;
        //ImageComponent.overrideSprite = AudioPlayer.isPlaying ? IsPlayingImage : IsPausedImage;

        if (AudioListener.volume != 0.0f || AudioPlayer.isPlaying)
        {
            ImageComponent.overrideSprite = IsPlayingImage;
        }
        else if (AudioListener.volume == 0.0f || !AudioPlayer.isPlaying)
        {
            ImageComponent.overrideSprite = IsPausedImage;
        }
    }

    public void Toggle()
	{
        if (AudioListener.volume == 1.0f)
        {
            AudioListener.volume = 0.0f;
        }
        else
        {
            AudioListener.volume = 1.0f;
        }
        UpdateButtonState();
    }

    public void ToggleMusic()
    {
        if (AudioPlayer.isPlaying)
        {
            AudioPlayer.Pause();
            AudioListener.volume = 0.0f;
        }
        else
        {
            AudioPlayer.Play();
            AudioListener.volume = 1.0f;
        }
        UpdateButtonState();
    }

// Update proper button image when changing windows / views
void OnEnable()
	{
		UpdateButtonState();
	}

}
