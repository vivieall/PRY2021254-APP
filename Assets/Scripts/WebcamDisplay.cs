using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamDisplay : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.RawImage rawImageComponent;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        foreach (WebCamDevice webcam in devices)
		{
            Debug.Log("Webcam available: " + webcam.name);
		}

        WebCamTexture tex = new WebCamTexture(devices[0].name);
        rawImageComponent.texture = tex;
        tex.Play();
    }
}
