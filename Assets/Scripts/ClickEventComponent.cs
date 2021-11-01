using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickEventComponent : MonoBehaviour
{
    // Los "listeners" se agregan desde el editor
    [SerializeField] public UnityEvent OnSingleTap;
    [SerializeField] public UnityEvent OnDoubleTap;
    [SerializeField] public GameObject CameraRef;

    float FirstPressTime = 0.0f; // Tiempo en que se hizo el primer press.
    float TimeWindowForDoublePress = 0.2f; // Tiempo en segundos que puede pasar entre primer y segundo press.
    bool bIsSecondPress = false; // Auxiliar para guardar el estado de clicks.
    private GameObject lastHitObject = null;

    public void Update()
	{
        bool isTapping = Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Began);
        if (Input.GetMouseButtonDown(0) || isTapping)
		{
            Vector3 touchLocation;
            if (isTapping)
			{
                Vector2 pos2D = Input.GetTouch(0).position;
                touchLocation = new Vector3(pos2D.x, pos2D.y, 0.0f);
			}
            else
			{
                touchLocation = Input.mousePosition;
			}
            Ray ray = Camera.main.ScreenPointToRay(touchLocation);
            RaycastHit hitResult;
            if (Physics.Raycast(ray, out hitResult))
			{
                GameObject hitObject = hitResult.transform.gameObject;
                // Si se toca un objeto nuevo, no puede ser un doble-click
                bIsSecondPress &= (lastHitObject == hitObject);
                lastHitObject = hitObject;
                OnPress();
			}
		}
	}

    // TODO: Arreglar colisiones, el click no se detecta. Es necesario usar raycasts al parecer.
    public void OnPress()
    {
        if (!bIsSecondPress) // Primer tap/click
        {
            // Invoke sirve para llamar un metodo luego de un tiempo T
            // CancelInvoke se utiliza para cancelar un Invoke pendiente
            Invoke("SinglePress", TimeWindowForDoublePress);
            bIsSecondPress = true;
            FirstPressTime = Time.time;
        }
        else if (Time.time - FirstPressTime < TimeWindowForDoublePress) // Segundo tap/click se hizo a tiempo
        {
            CancelInvoke("SinglePress"); 
            DoublePress();
        }
    }

    void SinglePress()
    {
        bIsSecondPress = false; 
        Debug.Log("SinglePress called from " + (gameObject ? gameObject.name : "DestroyedGameObject"));
        if (OnSingleTap != null)
        {
            Debug.Log("SinglePress event is not null");
            OnSingleTap.Invoke();
        }
    }

    void DoublePress()
    {
        bIsSecondPress = false;
        Debug.Log("DoublePress called from " + (gameObject ? gameObject.name : "DestroyedGameObject"));
        if (OnDoubleTap != null)
        {
            Debug.Log("DoublePress event is not null");
            OnDoubleTap.Invoke();
        }
    }
}
