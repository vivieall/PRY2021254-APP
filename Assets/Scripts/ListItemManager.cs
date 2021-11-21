using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItemManager : MonoBehaviour
{
	[SerializeField] public ListManager ListManager = null;
	[SerializeField] private GameObject AddButton = null;
	[SerializeField] private GameObject RemoveButton = null;
	[SerializeField] public int Id;
	[SerializeField] public Text text;
	[SerializeField] public Color inListAddColor;
	private Color originalAddColor;

	private void Awake() {
		// Id = Random.Range(int.MinValue, int.MaxValue);
		Button buttonComp = AddButton.GetComponent<Button>();
		originalAddColor = buttonComp.image.color;
	}
	private void OnEnable() { ResetButtons(); }

	public void SetEditMode(bool bIsRemoving = true) { 
		AddButton.SetActive(!bIsRemoving); 
		RemoveButton.SetActive(bIsRemoving); 
	}

	public void PromptAddToList()
	{
		if (!ListManager.ListContains(gameObject))
			ListManager.PromptItemOperation(this, true);
	}

	public void PromptRemoveFromList()
	{
		if (ListManager.ListContains(gameObject))
			ListManager.PromptItemOperation(this, false);
	}

	public void ResetButtons()
	{
		if (ListManager)
		{
			bool inList = ListManager.ListContains(gameObject);
			AddButton.GetComponent<Button>().image.color = inList ? inListAddColor : originalAddColor;
			SetEditMode(false);
		}
		else Debug.Log(name + " does not have a ListManager");
	}
}
