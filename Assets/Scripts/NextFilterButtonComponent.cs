using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class NextFilterButtonComponent : MonoBehaviour
{
    [SerializeField] public ARFaceManager FaceManager;
    [SerializeField] public GameObject ARFaceFilterContainer;
    private int CurrentFaceIndex;
    private ArrayList FacePrefabs;

	private void Awake()
	{
		FacePrefabs = new ArrayList();
	}

	public void ChangeFilter(int DeltaIndex)
	{
        if (FacePrefabs.Count > 0)
		{
            int mod = FacePrefabs.Count;
            CurrentFaceIndex += DeltaIndex;
            CurrentFaceIndex = ((CurrentFaceIndex % mod) + mod) % mod;
            FaceManager.facePrefab = FacePrefabs[CurrentFaceIndex] as GameObject;
            ARFaceFilterContainer.SetActive(false);
            ARFaceFilterContainer.SetActive(true);
            Debug.Log("new filter index = " + CurrentFaceIndex.ToString() + " - " + FaceManager.facePrefab.name);
		}
	}

    public void ReadFilterScreenButtonData(ShowFilterScreenButton[] filterButtons,  ShowFilterScreenButton senderButton)
	{
        FacePrefabs.Clear();
        CurrentFaceIndex = -1;
        int i = 0;
        foreach (ShowFilterScreenButton filterButton in filterButtons)
		{
            if (filterButton.FacePrefab)
			{
				FacePrefabs.Add(filterButton.FacePrefab);
                if (filterButton == senderButton)
				{
                    CurrentFaceIndex = i;
                    FaceManager.facePrefab = senderButton.FacePrefab;
				}
                i++;
			}
		}
        if (FacePrefabs.Count == 0)
		{
            Debug.Log(gameObject.name + ":" + name + ": ReadFilterScreenButtonData didnt get a single valid face prefab");
            CurrentFaceIndex = 0;
            return;
		}
        if (CurrentFaceIndex == -1)
		{
            Debug.Log(gameObject.name + ":" + name + ": ReadFilterScreenButtonData did not find a valid sender");
            CurrentFaceIndex = 0;
		}
	}
}
