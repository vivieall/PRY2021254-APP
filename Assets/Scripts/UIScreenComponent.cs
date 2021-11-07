using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScreenComponent : MonoBehaviour
{
	private SceneUIManager sceneUIManager;
    private GameObject ParentUI = null;

	private void Start()
	{
		sceneUIManager = Camera.main.GetComponent<SceneUIManager>();
		if (!sceneUIManager)
			Debug.Log(gameObject.name + " " + name + ": NO SCENE MANAGER FOUND");
	}

	public void ShowUIFromParent(GameObject parentUI)
	{
		ParentUI = parentUI;
		gameObject.SetActive(true);
	}

	public void OnBackButtonPress()
	{
		sceneUIManager.ShowUI_GoBack(ParentUI);
	}
}
