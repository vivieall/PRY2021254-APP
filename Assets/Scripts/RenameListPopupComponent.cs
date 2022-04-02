using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RenameListPopupComponent : MonoBehaviour
{
	[SerializeField] public InputField nameInput;
	[SerializeField] public Button save;

	public void TextChanged() {
		save.interactable = nameInput.text.Length > 0;
	}

	public void SetSaveButtonAction(UnityAction action) {
		Button rB = save.GetComponent<Button>();
		rB.onClick.RemoveAllListeners();
		rB.onClick.AddListener(action);
	}
}