using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class VideoButtonComponent : MonoBehaviour
{
    [SerializeField] public Image ImageComponent;
    [SerializeField] public Sprite IsPausedImage;
    [SerializeField] public Sprite IsPlayingImage;
    [SerializeField] public VideoPlayer Video_Player;

    // Start is called before the first frame update
    void Start()
    {
        UpdateButtonState();
    }

    public void UpdateButtonState()
    {
        if (Video_Player.isPlaying)
        {
            ImageComponent.overrideSprite = IsPausedImage;
        }
        else if(Video_Player.isPaused)
        {
            ImageComponent.overrideSprite = IsPlayingImage;
        }
    }

    public void Toggle()
    {
        if (Video_Player.isPlaying==true)
        {
            Video_Player.Pause();
            
        }
        else
        {
            Video_Player.Play();
        }
        UpdateButtonState();
    }

    // Update proper button image when changing windows / views
    void OnEnable()
    {
        UpdateButtonState();
    }

}
