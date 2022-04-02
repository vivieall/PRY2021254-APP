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
	private bool waitingForRemoteResponse = false;

	public void ConfirmOperation(string text, UnityAction confirm, UnityAction decline) {
		this.SetConfirmationText(text);
		this.ClearAllEvents();
		this.OnAccept.AddListener(confirm);
		this.OnDecline.AddListener(decline);
		this.gameObject.SetActive(true);
	}

	public void ExecuteOnAccept() {
		OnAccept.Invoke();
		if (!waitingForRemoteResponse) {
			gameObject.SetActive(false);
		}
	}


	public void ExecuteOnDecline() {
		OnDecline.Invoke();
		if (!waitingForRemoteResponse) {
			gameObject.SetActive(false);
		}
	}

	public void SetConfirmationText(string text) {
		TextOnScreen.text = text;
	}

	public void ClearAllEvents() {
		OnAccept.RemoveAllListeners();
		OnDecline.RemoveAllListeners();
	}

	public void SetLoadingState(bool state) {
		waitingForRemoteResponse = state;

		if (state) {
            TextOnScreen.text = "Cargando...";
		}

		Button[] buttons = this.GetComponentsInChildren<Button>(true);

		foreach(Button button in buttons) {
			button.gameObject.SetActive(!state);
		}

		if (!waitingForRemoteResponse) {
			gameObject.SetActive(false);
		}
	}
}
