using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickEventComponent : MonoBehaviour, IPointerDownHandler
{
    // Los "listeners" se agregan desde el editor
    [SerializeField] public UnityEvent OnSingleTap;
    [SerializeField] public UnityEvent OnDoubleTap;
    [SerializeField] public GameObject CameraRef;

    float FirstTapTime = 0.0f; // Tiempo en que se hizo el primer tap.
    float TimeWindowForDoubleTap = 0.2f; // Tiempo en segundos que puede pasar entre primer y segundo tap.
    bool bIsSecondTap = false; // Auxiliar para guardar el estado de clicks.


    // TODO: Arreglar colisiones, el click no se detecta. Es necesario usar raycasts al parecer.
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!bIsSecondTap) // Primer tap/click
        {
            // Invoke sirve para llamar un metodo luego de un tiempo T
            // CancelInvoke se utiliza para cancelar un Invoke pendiente
            Invoke("SingleTap", TimeWindowForDoubleTap);
            bIsSecondTap = true;
            FirstTapTime = Time.time;
        }
        else if (Time.time - FirstTapTime  < TimeWindowForDoubleTap) // Segundo tap/click se hizo a tiempo
        {
            CancelInvoke("SingleTap"); 
            DoubleTap();
        }
    }

    void SingleTap()
    {
        bIsSecondTap = false; 
        Debug.Log("SingleTap called from " + this.name);
        if (OnSingleTap != null)
        {
            Debug.Log("SingleTap event is not null");
            OnSingleTap.Invoke();
        }
    }

    void DoubleTap()
    {
        bIsSecondTap = false;
        Debug.Log("DoubleTap called from " + this.name);
        if (OnDoubleTap != null)
        {
            Debug.Log("DoubleTap event is not null");
            OnDoubleTap.Invoke();
        }
    }
}
