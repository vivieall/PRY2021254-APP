using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RegisterChildConfirmationPopupComponent : MonoBehaviour
{
    public UnityEvent OnAccept;
    
    public void ExecuteOnAccept() { 
        OnAccept.Invoke();
        gameObject.SetActive(false); 
    }
        
    public void ClearAllEvents()
	{
		OnAccept.RemoveAllListeners();
	} 
}
