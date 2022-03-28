﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmPopupComponent : MonoBehaviour
{
	[SerializeField] public Text TextOnScreen;
    public UnityEvent OnAccept;
    public UnityEvent OnDecline;

	public void ExecuteOnAccept() {
		Debug.Log("HERE ACCEPT");
		OnAccept.Invoke();
		gameObject.SetActive(false);
	}
	public void ExecuteOnDecline() {
		Debug.Log("HERE DECLINE");
		OnDecline.Invoke();
		gameObject.SetActive(false);
	}

	public void SetConfirmationText(string text) { TextOnScreen.text = text; }

	public void ClearAllEvents()
	{
		OnAccept.RemoveAllListeners();
		OnDecline.RemoveAllListeners();
	}
}
