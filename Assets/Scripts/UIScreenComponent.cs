using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenComponent : MonoBehaviour
{
	private SceneManager sceneManager;
    private GameObject ParentUI = null;

	private void Start()
	{
		sceneManager = Camera.main.GetComponent<SceneManager>();
		if (!sceneManager)
			Debug.Log(gameObject.name + " " + name + ": NO SCENE MANAGER FOUND");
	}

	public void ShowUIFromParent(GameObject parentUI)
	{
		ParentUI = parentUI;
		gameObject.SetActive(true);
	}

	public void OnBackButtonPress()
	{
		sceneManager.ShowUI_GoBack(ParentUI);
	}
}
