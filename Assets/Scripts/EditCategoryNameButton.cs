using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditCategoryNameButton : MonoBehaviour
{
    [SerializeField] private GameObject EditPopup;

    public void ShowPopup()
    {
        EditCategoryNamePopupComponent EditComp = EditPopup.GetComponent<EditCategoryNamePopupComponent>();
        EditComp.ClearAllEvents();
        EditPopup.transform.Find("NameInput").GetComponent<InputField>().text = "";
        EditPopup.SetActive(true);
    }
}
