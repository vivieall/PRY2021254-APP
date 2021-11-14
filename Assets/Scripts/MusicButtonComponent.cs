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
    }

    public void UpdateButtonState()
	{
        ImageComponent.overrideSprite = AudioPlayer.isPlaying ? IsPlayingImage : IsPausedImage;
	}

    public void Toggle()
	{
        if (AudioPlayer.isPlaying)
		{
            AudioPlayer.Pause();
		}
        else
		{
            AudioPlayer.Play();
		}
        UpdateButtonState();
	}

	void OnEnable()
	{
		UpdateButtonState();
	}

}
