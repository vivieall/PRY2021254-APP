using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ListItem : MonoBehaviour
{
	[SerializeField] public ListManager listManager = null;
	[SerializeField] private GameObject addButton = null;
	[SerializeField] private GameObject removeButton = null;
	[SerializeField] public Text text;
	[SerializeField] public Color inListAddColor;

	public void ProcessAdditionToList(SceneUIManager SceneManager) {
		if (listManager != null) {
			addButton.SetActive(false);
			removeButton.SetActive(true);
		}
	}

	public void SetRemoveButtonAction(UnityAction action) {
		Button rB = removeButton.GetComponent<Button>();
		rB.onClick.RemoveAllListeners();
		rB.onClick.AddListener(action);
	}

	public void SetEditMode(bool bIsRemoving = true) { 
		addButton.SetActive(!bIsRemoving); 
		removeButton.SetActive(bIsRemoving); 
	}

	public void PromptAddToList()
	{
		/*if (!listManager.ListContains(gameObject))
			listManager.PromptItemOperation(this, true);*/
	}

	public void PromptRemoveFromList()
	{
		/*if (listManager.ListContains(gameObject))
			listManager.PromptItemOperation(this, false);*/
	}

	public void ResetButtons()
	{
		/*if (ListManager)
		{
			bool inList = ListManager.ListContains(gameObject);
			AddButton.GetComponent<Button>().image.color = inList ? inListAddColor : originalAddColor;
			SetEditMode(false);
		}
		else Debug.Log(name + " does not have a ListManager");*/
	}

	public void setRemoveButtonActiveStatus(bool statusActive) {
		removeButton.SetActive(statusActive);
	}

	public void setAddButtonActiveStatus(bool statusActive) {
		addButton.SetActive(statusActive);
	}
}
