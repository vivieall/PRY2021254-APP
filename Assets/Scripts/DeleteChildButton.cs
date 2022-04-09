using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteChildButton : MonoBehaviour
{
    [SerializeField] private GameObject DeletePopup;

    public void ShowPopup()
    {
        DeleteChildPopupComponent DeleteComp = DeletePopup.GetComponent<DeleteChildPopupComponent>();
        DeleteComp.ClearAllEvents();
        DeletePopup.SetActive(true);
    }
}
