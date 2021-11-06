using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ARClickableComponent : MonoBehaviour
{
    [SerializeField] public UnityEvent OnSingleTap;
    [SerializeField] public UnityEvent OnDoubleTap;

    public void OnPress()
    {
        Debug.Log(gameObject.name + "OnAnyPress called.");
    }

    public void SinglePress()
    {
        string msg = "SinglePress called from " + (gameObject ? gameObject.name : "DestroyedGameObject");
        if (OnSingleTap != null)
        {
            msg += " - INVOKING OnSingleTap";
            OnSingleTap.Invoke();
        }
        Debug.Log(msg);
    }

    public void DoublePress()
    {
        string msg = "DoublePress called from " + (gameObject ? gameObject.name : "DestroyedGameObject");
        if (OnDoubleTap != null)
        {
            msg += " - INVOKING OnDoubleTap";
            OnDoubleTap.Invoke();
        }
        Debug.Log(msg);
    }
}
