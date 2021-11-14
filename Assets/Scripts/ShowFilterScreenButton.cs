using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShowFilterScreenButton : MonoBehaviour
{
    [SerializeField] ARFaceManager FaceManager;
    [SerializeField] GameObject FacePrefab;
    [SerializeField] GameObject SelectFilterScreenRoot;

    public void SendPrefabToFaceManager()
	{
        ShowFilterScreenButton[] screenButtons = SelectFilterScreenRoot.GetComponentsInChildren<ShowFilterScreenButton>();

        if (FacePrefab)
		{
            FaceManager.facePrefab = FacePrefab;
		}
        else Debug.Log(gameObject.name + ": Button has null face prefab - can't send to face manager");
	}
    
}
