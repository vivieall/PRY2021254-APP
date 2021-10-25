using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItemManager : MonoBehaviour
{
	[SerializeField] public Text text;
	[SerializeField] public ListManager ListManager = null;
	[SerializeField] private GameObject AddButton = null;
	[SerializeField] private GameObject RemoveButton = null;

	public void Start()
	{
		if (ListManager) UpdateButtons();
		else Debug.Log(name + " does not have a ListManager");
	}
	public void AddToList()
	{
		if (ListManager)
		{
			ListManager.AddItem(text.text);
			UpdateButtons();
		}
		else Debug.Log(name + " does not have a ListManager");
	}

	public void RemoveFromList()
	{
		if (ListManager)
		{
			ListManager.RemoveItem(gameObject);
			UpdateButtons();
		}
		else Debug.Log(name + " does not have a ListManager");
	}

	public void UpdateButtons()
	{
		if (ListManager)
		{
			bool inList = ListManager.ListContains(gameObject);
			AddButton.SetActive(!inList);
			RemoveButton.SetActive(inList);
		}
		else Debug.Log(name + " does not have a ListManager");
	}
}
