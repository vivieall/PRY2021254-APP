using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ListItemManager : MonoBehaviour
{
	[SerializeField] public ListManager listManager = null;
	[SerializeField] private GameObject addButton = null;
	[SerializeField] private GameObject removeButton = null;
	[SerializeField] public Text text;
	[SerializeField] public Color inListAddColor;

	public void SetRemoveButtonAction(UnityAction action) {
		Button rB = removeButton.GetComponent<Button>();
		rB.onClick.RemoveAllListeners();
		rB.onClick.AddListener(action);
	}

	public void setRemoveButtonActiveStatus(bool statusActive) {
		removeButton.SetActive(statusActive);
	}

	public void setAddButtonActiveStatus(bool statusActive) {
		addButton.SetActive(statusActive);
	}
}
