using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShowFilterScreenButton : MonoBehaviour
{
    [SerializeField] ARFaceManager FaceManager;
    [SerializeField] GameObject FacePrefab;

    public void SendPrefabToFaceManager()
	{
        if (FacePrefab)
		{
            FaceManager.facePrefab = FacePrefab;
		}
        else Debug.Log(gameObject.name + ": Button has null face prefab - can't send to face manager");
	}
    
}
