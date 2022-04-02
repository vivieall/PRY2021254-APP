using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InformationPopupComponent : MonoBehaviour
{
	[SerializeField] public Text TextOnScreen;

	public void PopupMessage(string text) {
		TextOnScreen.text = text;
		gameObject.SetActive(true);
	}
}