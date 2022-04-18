using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChangeAvatarConfirmationPopupComponent : MonoBehaviour
{
    public UnityEvent OnAccept;
    public UnityEvent OnDecline;
    
    public void ExecuteOnAccept() { 
        OnAccept.Invoke();
        gameObject.SetActive(false);
    }
	public void ExecuteOnDecline() { 
        OnDecline.Invoke(); 
        gameObject.SetActive(false); }

	public void ClearAllEvents()
	{
		OnAccept.RemoveAllListeners();
		OnDecline.RemoveAllListeners();
	}
}
