using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListsManager : MonoBehaviour
{
	public struct ItemStruct
	{
		public ListItemManager item;
		public GameObject button;

		public ItemStruct(GameObject _button)
		{
			this.item = GetItemManager(_button);
			this.button = _button;
		}
		public ItemStruct(ListItemManager _item, GameObject _button)
		{
			this.item = _item;
			this.button = _button;
		}

		public override bool Equals(object obj)
		{
			ListItemManager other = obj as ListItemManager;
			return other ? (item.Id == other.Id) : false;
		}
		public override int GetHashCode() { return item.Id; }
	};

	[SerializeField] private GameObject ListUI;
	[SerializeField] private SceneUIManager SceneManager;
    [SerializeField] private ArrayList ListItems = null;
    [SerializeField] private GameObject ContentPanel = null;
	[SerializeField] private GameObject ConfirmPopup = null;
    //[SerializeField] private GameObject ListItemPrefab = null;
	[SerializeField] private string ListName = "Lista Personalizada";
	[SerializeField] private bool allowDuplicates = false;
	[SerializeField] private bool bConfirmCreateList = false;

	private ListItemManager PendingItem = null;
	private bool PendingOperationType; // true for add, false for delete
	private bool bIsInEditMode = false; 
	private bool bWasListCreated = false;

	public static ListItemManager GetItemManager(GameObject go) { return go ? go.GetComponent<ListItemManager>() : null; }
	private void Awake() { if (ListItems == null) ListItems = new ArrayList(); }
	public void OnEnable() { SetEditMode(false); }

	#region UI_Interactions_CreateList
	public void PromptCreateList()
	{
		if (bConfirmCreateList && !bWasListCreated)
		{
			ConfirmPopupComponent confirmComp = ConfirmPopup.GetComponent<ConfirmPopupComponent>();
			confirmComp.SetConfirmationText("¿Desea crear una " + ListName + "?");
			confirmComp.ClearAllEvents();
			confirmComp.OnAccept.AddListener(ConfirmCreateList);
			confirmComp.OnDecline.AddListener(DenyCreateList);
			ConfirmPopup.SetActive(true); 
		}
		else SceneManager.ShowUI(ListUI);
	}

	public void ConfirmCreateList() { 
		bWasListCreated = true; 
		ConfirmPopup.SetActive(false); 
		SceneManager.ShowUI(ListUI); 
	}

	public void DenyCreateList() { ConfirmPopup.SetActive(false); }
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

        if (allowDuplicates || !ListItems.Contains(item)) {
			GameObject newItem = Instantiate(go);
			ListItemManager lim = GetItemManager(newItem);
			if (lim.ListManager == null)
				Debug.Log("LIM LIST MANAGER NULL");
			lim.ListManager = this;
			newItem.transform.SetParent(ContentPanel.transform);
			newItem.transform.localScale = Vector3.one;
			ListItems.Add(new ItemStruct(lim, newItem));
			item.ResetButtons();
			return true;
		}
		return false;
	}
	#endregion

	#region RemoveItems
	public void RemoveItem(GameObject item) { RemoveItem_internal(GetItemManager(item)); }
	public void RemoveItem(ListItemManager item) { RemoveItem_internal(item); }

    public bool RemoveItem_internal(ListItemManager item)
	{
        if (item && ListItems.Contains(item)) {
			int i = ListItems.IndexOf(item);
			Destroy(item.gameObject);
			ListItems.RemoveAt(i);
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
			if (itemStruct.item.Id == lim.Id)
				return true;
		}
		return false;
	}
	#endregion

}
