using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmPopupComponent : MonoBehaviour
{
	[SerializeField] public Text TextOnScreen;
    public UnityEvent OnAccept;
    public UnityEvent OnDecline;

	public void ConfirmOperation(string text, UnityAction confirm, UnityAction decline) {
		this.SetConfirmationText(text);
		this.ClearAllEvents();
		this.OnAccept.AddListener(confirm);
		this.OnDecline.AddListener(decline);
		this.gameObject.SetActive(true);
	}

	public void ExecuteOnAccept() {
		OnAccept.Invoke();
		gameObject.SetActive(false);
	}


	public void ExecuteOnDecline() {
		OnDecline.Invoke();
		gameObject.SetActive(false);
	}

	public void SetConfirmationText(string text) {
		TextOnScreen.text = text;
	}

	public void ClearAllEvents() {
		OnAccept.RemoveAllListeners();
		OnDecline.RemoveAllListeners();
	}
}
