using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ListManager : MonoBehaviour
{
	[SerializeField] public GameObject ListUI;
	[SerializeField] public SceneUIManager SceneManager;
    [SerializeField] private List<ListItemManager> items = new List<ListItemManager>();
    [SerializeField] public GameObject ContentPanel = null;
	[SerializeField] public GameObject InformationPopup = null;
	[SerializeField] public Button deleteButton = null;
	[SerializeField] public Button editButton = null;
	[SerializeField] public Button saveButton = null;
	[SerializeField] public InputField editNameInputField = null;
	[SerializeField] public string Name = "Lista Personalizada";
	[SerializeField] private bool isFavoritesList;
	private bool isEditing = false;
    private int id;
    public int Id { get; set; }

	private void OnEnable() {
		if (isEditing) {
			SwitchEditMode();
		}
	}

	public void SwitchEditMode() {
		isEditing = !isEditing;
		foreach(ListItemManager listItem in ContentPanel.GetComponentsInChildren<ListItemManager>()) {
			listItem.setRemoveButtonActiveStatus(isEditing);
		}

		if (!isFavoritesList) {
			float offset = editNameInputField.gameObject.GetComponent<RectTransform>().rect.height + 10;
			RectTransform rect = gameObject.GetComponent<RectTransform>();
			rect.offsetMax = new Vector2(rect.offsetMax.x, rect.offsetMax.y + (isEditing ? -offset : offset));
		}

		if (isEditing) {
			if (!isFavoritesList) {
				editNameInputField.gameObject.SetActive(true);
			}

			editButton.gameObject.SetActive(false);
			saveButton.gameObject.SetActive(true);
		} else {
			if (!isFavoritesList) {
				editNameInputField.gameObject.SetActive(false);
				editNameInputField.text = "";
			}

			editButton.gameObject.SetActive(true);
			saveButton.gameObject.SetActive(false);
		}
	}

	public ListItemManager Add(ListItemManager listItem)
	{
		ListItemManager listItemInstance = Instantiate(listItem);
		listItemInstance.transform.SetParent(ContentPanel.transform);
		listItemInstance.GetComponent<RectTransform>().sizeDelta = listItem.GetComponent<RectTransform>().sizeDelta;
		listItemInstance.transform.localScale = 1.3F * Vector3.one;
		listItemInstance.listManager = this;
		listItemInstance.setAddButtonActiveStatus(false);
		listItemInstance.setRemoveButtonActiveStatus(isEditing);
		items.Add(listItemInstance);
		return listItemInstance;
	}

	public ListItemManager Add(LevelButtonListItem levelButtonListItem)
	{
		ListItemManager listItem = Add(levelButtonListItem.GetComponent<ListItemManager>());
		if (isFavoritesList && levelButtonListItem.levelUIComponent != null) {
			levelButtonListItem.levelUIComponent.LikeButton.interactable = false;
			levelButtonListItem.levelUIComponent.LikeLabel.text = "¡Agregado a favoritos!";
		}
		if (isFavoritesList) {
			listItem.SetRemoveButtonAction(() => SceneManager.DeleteFavoriteLevel(listItem.GetComponentInParent<LevelButtonListItem>()));
		} else {
			listItem.SetRemoveButtonAction(() => SceneManager.DeleteLevelFromCustomList(this, listItem.GetComponentInParent<LevelButtonListItem>()));
		}
		return listItem;
	}

	public void Refresh() {
		foreach(ListItemManager listItem in ContentPanel.GetComponentsInChildren<ListItemManager>()) {
			listItem.gameObject.SetActive(false);
		}

		foreach(ListItemManager listItem in items) {
			listItem.gameObject.SetActive(true);
		}
	}

	public void DeleteList() {
		Debug.Log("DELETING LIST");
	}

	public void Remove(ListItemManager listItem) {
		items.Remove(listItem);
		Destroy(listItem.gameObject);
	}

	public void Remove(LevelButtonListItem levelButtonListItem) {
		if (isFavoritesList && levelButtonListItem.levelUIComponent != null) {
			levelButtonListItem.levelUIComponent.LikeButton.interactable = true;
			levelButtonListItem.levelUIComponent.LikeLabel.text = "¿Te gusta el nivel? ¡Agrégalo a favoritos!";
		}

		Remove((ListItemManager) levelButtonListItem);
	}

	public bool isEmpty(){
		if (items == null) {
			return true;
		}
		return items.Count == 0;
	}

	public void RemoveAll() {
		ListItemManager[] children = ContentPanel.GetComponentsInChildren<ListItemManager>(true);
		foreach(ListItemManager listItem in children) {
			Destroy(listItem.gameObject);
		}

		items.Clear();
	}
}
