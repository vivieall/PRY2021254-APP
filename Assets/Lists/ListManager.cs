using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ListManager : MonoBehaviour
{
	[SerializeField] private GameObject ListUI;
	[SerializeField] private SceneUIManager SceneManager;
    [SerializeField] private List<ListItem> items = new List<ListItem>();
    [SerializeField] private GameObject ContentPanel = null;
	[SerializeField] private GameObject InformationPopup = null;
	[SerializeField] public string Name = "Lista Personalizada";
	[SerializeField] private bool allowDuplicates = false;
	[SerializeField] private bool bConfirmCreateList = false;

	private bool PendingOperationType; // true for add, false for delete
	private bool bIsInEditMode = false;
	private bool bWasListCreated = false;

	public void OnEnable() { /*SetEditMode(false);*/ }

	public ListItem Add(ListItem listItem)
	{
		ListItem listItemInstance = Instantiate(listItem);
		listItemInstance.transform.SetParent(ContentPanel.transform);
		listItemInstance.transform.localScale = 1.3F * Vector3.one;
		listItemInstance.listManager = this;
		listItemInstance.ProcessAdditionToList(SceneManager);
		items.Add(listItemInstance);
		return listItemInstance;
	}

	public void Remove(ListItem listItem) {
		items.Remove(listItem);
		Destroy(listItem.gameObject);
	}

	/*#region UI_Interactions_CreateList
	public void PromptCreateList()
	{
		if (bConfirmCreateList && !bWasListCreated)
		{
			ConfirmPopupComponent confirmComp = ConfirmPopup.GetComponent<ConfirmPopupComponent>();
			confirmComp.SetConfirmationText("¿Desea crear una " + Name + "?");
			confirmComp.ClearAllEvents();
			confirmComp.OnAccept.AddListener(ConfirmCreateList);
			confirmComp.OnDecline.AddListener(DenyCreateList);
			ConfirmPopup.gameObject.SetActive(true); 
		}
		SceneManager.ShowUI(ListUI);
	}

	public void ConfirmCreateList() { 
		bWasListCreated = true; 
		ConfirmPopup.gameObject.SetActive(false); 
		SceneManager.ShowUI(ListUI); 
	}

	public void DenyCreateList() { ConfirmPopup.gameObject.SetActive(false); }
	#endregion

	#region UI_Interactions_Main
	public void PromptItemOperation(ListItemManager item, bool bIsAdding)
	{
		PendingItem = item; 
		PendingOperationType = bIsAdding;
		ConfirmPopupComponent confirmComp = ConfirmPopup.GetComponent<ConfirmPopupComponent>();
		string popupText = bIsAdding ? 
			"¿Desea añadir este nivel a " + ListName + "?" : 
			"¿Desea eliminar este nivel de " + ListName + "?";
		confirmComp.SetConfirmationText(popupText);
		confirmComp.ClearAllEvents();
		confirmComp.OnAccept.AddListener(ConfirmOperation);
		confirmComp.OnDecline.AddListener(DeclineOperation);
		ConfirmPopup.SetActive(true); 
	}
	public void PromptAddItem(ListItemManager item) { PromptItemOperation(item, true); }

	public void PromptRemoveItem(ListItemManager item) { PromptItemOperation(item, false); } 
	public void DeclineOperation() { PendingItem = null; ConfirmPopup.SetActive(false); }

	public void ConfirmOperation() { 
		ConfirmPopup.SetActive(false);
		if (PendingOperationType) // Add
		{
			AddItem(PendingItem); 
			SceneManager.ShowUI(ListUI); 
		}
		else // Remove
		{
			RemoveItem(PendingItem);
			SetEditMode(false);
			InformationPopup.SetActive(true);
		}
	} 

	public void SetEditMode() { bIsInEditMode = !bIsInEditMode; SetEditMode(bIsInEditMode); }
	public void SetEditMode(bool bIsEditMode)
	{
		bIsInEditMode = bIsEditMode;
		foreach(ItemStruct item in ListItems)
			item.item.SetEditMode(bIsInEditMode);
	}
	#endregion

	#region AddItems
	public void AddItem(GameObject go) { AddItem_internal(go); }
	public void AddItem(ListItemManager lim) { AddItem_internal(lim.gameObject); }

    public bool AddItem_internal(GameObject go)
	{
		ListItemManager item = GetItemManager(go);
		if (!item)
			return false;

        if (allowDuplicates || !ListContains(go)) {
			GameObject newItem = Instantiate(item.itemToAdd);
			ListItemManager lim = GetItemManager(go);
			if (lim.ListManager == null)
				Debug.Log("LIM LIST MANAGER NULL");
			lim.ListManager = this;

			foreach(Transform child in newItem.transform) {
				ListItemManager childLim = GetItemManager(child.gameObject);
				if (childLim != null) {
					childLim.ListManager = this;
					childLim.setRemoveButtonActiveStatus(true);
					childLim.setAddButtonActiveStatus(false);
				} 
			}

			newItem.transform.SetParent(ContentPanel.transform);
			newItem.transform.localScale = 1.3F * Vector3.one;
			ListItems.Add(new ItemStruct(lim, newItem));

			SceneUIManager sceneUIManager = Camera.main.GetComponent<SceneUIManager>();
			sceneUIManager.AddFavoriteLevel();
		}

		return false;
	}
	#endregion

	#region RemoveItems
	public void RemoveItem(GameObject item) { RemoveItem_internal(GetItemManager(item)); }
	public void RemoveItem(ListItemManager item) { RemoveItem_internal(item); }

    public bool RemoveItem_internal(ListItemManager item)
	{
        if (item && ListContains(item.gameObject)) {
			int i = ListIndexOf(item.gameObject);
			Destroy(GetItemManager(item.gameObject).itemToAdd);
			ListItems.RemoveAt(i);
			SceneUIManager sceneUIManager = Camera.main.GetComponent<SceneUIManager>();
			sceneUIManager.DeleteFavoriteLevel(item.Id);

			return true;
		}
		return false;
	}
	#endregion

	#region Search
	public void UpdateList(string CurrentSearch)
	{
		foreach(ItemStruct item in ListItems)
			item.button.SetActive(item.item.text.text.Contains(CurrentSearch));
	}

    public void UpdateList(Text CurrentSearch) { UpdateList(CurrentSearch.text); }
	public bool ListContains(GameObject item) { 
		if (ListItems == null)
			ListItems = new ArrayList();
		ListItemManager lim = GetItemManager(item);

		foreach(ItemStruct itemStruct in ListItems)
		{
			if (itemStruct.button.GetComponentInChildren<BotonNivel>().id == lim.itemToAdd.GetComponentInChildren<BotonNivel>().id)
				return true;
		}
		return false;
	}
	public int ListIndexOf(GameObject item) {
		if (ListItems == null)
			ListItems = new ArrayList();
		ListItemManager lim = GetItemManager(item);

		for (int i = 0; i < ListItems.Count; i++) {
			if (((ItemStruct) ListItems[i]).button == lim.itemToAdd)
				return i;
		}

		return -1;
	}
	#endregion*/

}
