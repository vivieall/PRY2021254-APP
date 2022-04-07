using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditCategoryNameButton : MonoBehaviour
{
    [SerializeField] private GameObject EditPopup;

    public void ShowPopup()
    {
        EditCategoryNamePopupComponent EditComp = EditPopup.GetComponent<EditCategoryNamePopupComponent>();
        EditComp.ClearAllEvents();
        EditPopup.SetActive(true);
    }
}
