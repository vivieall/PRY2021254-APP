using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotComponent : MonoBehaviour
{
    [SerializeField] public Camera ARCamera = null;
    [SerializeField] public GameObject CameraDisplay = null;
    [SerializeField] public GameObject TakenPhotoDisplay = null;
    [SerializeField] public GameObject ConfirmPopupPanel = null;
    [SerializeField] public GameObject ActiveCameraButtons = null;
    [SerializeField] public GameObject PendingSaveButtons = null;
    [SerializeField] public int screenshotWidth = 1920;
    [SerializeField] public int screenshotHeight = 1080;

    private Texture2D TakenPhoto = null;
    private RawImage TakenPhotoRawImage = null;
    private string outputFolder;

	#region Events
	void Start()
	{
        TakenPhotoRawImage = TakenPhotoDisplay.GetComponent<RawImage>();
        outputFolder = Application.persistentDataPath + "/Screenshots";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
            Debug.Log("Save Path will be : " + outputFolder);
        }
	}
	void OnEnable() { ToggleModes(false); }
    #endregion

    public void ToggleModes(bool bActivatePendingSaveMode)
	{
        ConfirmPopupPanel.SetActive(false);
        PendingSaveButtons.SetActive(bActivatePendingSaveMode);
        TakenPhotoDisplay.SetActive(bActivatePendingSaveMode);
        ActiveCameraButtons.SetActive(!bActivatePendingSaveMode);
        CameraDisplay.SetActive(!bActivatePendingSaveMode);
	}
    public void ActivatePendingSaveMode() { ToggleModes(true); }
    public void RestoreNormalMode() { ToggleModes(false); }
    public void ShowConfirmationPopup() { ConfirmPopupPanel.SetActive(true); } 

    public void TakePhoto()
	{
        RenderTexture cameraRenderTexture = ARCamera.targetTexture;

        // Store the camera view texture data
        RenderTexture.active = cameraRenderTexture;
        TakenPhoto = new Texture2D(cameraRenderTexture.width, cameraRenderTexture.height, TextureFormat.RGB24, false);
        TakenPhoto.ReadPixels(new Rect(0, 0, cameraRenderTexture.width, cameraRenderTexture.height), 0, 0);
        RenderTexture.active = null;

        // Capture the camera view into the TakenPhotoRawImage texture
        ARCamera.targetTexture = TakenPhotoRawImage.texture as RenderTexture;
        ARCamera.Render();
        ARCamera.targetTexture = cameraRenderTexture;
	}

	private string CreateFileName(int width, int height)
	{
		string timestamp = DateTime.Now.ToString("yyyyMMddTHHmmss");
		var filename = string.Format("{0}/screen_{1}x{2}_{3}.{4}", outputFolder, width, height, timestamp, "png");
		return filename;
	}

	public void SavePendingToFile()
    {
        byte[] bytes;
        bytes = TakenPhoto.EncodeToPNG();
        string fileName = CreateFileName(screenshotWidth, screenshotHeight);
        var file = File.Create(fileName);
        file.Write(bytes, 0, bytes.Length);
        file.Close();
        Debug.Log(string.Format("Screenshot Saved {0}, size {1}", fileName, bytes.Length));
    }

}
