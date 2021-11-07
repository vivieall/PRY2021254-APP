using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCameraRenderTargetClickManager : MonoBehaviour
{
    // TODO: Translate comments to english
    [SerializeField] public Camera UICameraRef;
    [SerializeField] public Camera ARCameraRef;
    [SerializeField] public RectTransform DisplayRectTransform;

    float FirstPressTime = 0.0f; // Tiempo en que se hizo el primer press.
    float TimeWindowForDoublePress = 0.2f; // Tiempo en segundos que puede pasar entre primer y segundo press.
    bool bIsSecondPress = false; // Auxiliar para guardar el estado de clicks.
    private ARClickableComponent LastClickedComponent = null; // Ultimo objeto presionado.

    public void Update()
	{
        if (!ARCameraRef)
		{
            Debug.Log(name + " ARCameraRenderTargetClickManager has no CameraRef");
            return;
		}
        else if (ARCameraRef.targetTexture && (!UICameraRef || !DisplayRectTransform))
		{
            Debug.Log(name + " ARCameraRenderTargetClickManager is rendering to texture but UICameraRef or DisplayRectTransform are not set.");
            return;
		}

        bool isTapping = Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Began);
        if (Input.GetMouseButtonDown(0) || isTapping)
		{
            Debug.Log(gameObject.name + " ARCameraRenderTargetClickManager has click down.");
            Vector3 touchLocation;
            if (isTapping)
			{
                Vector2 pos2D = Input.GetTouch(0).position;
                touchLocation = new Vector3(pos2D.x, pos2D.y, 0.0f);
			}
            else touchLocation = Input.mousePosition;

			if (ARCameraRef.targetTexture && DisplayRectTransform) // Rendering to texture, need to get the hit texture in UI camera and then raycast from AR camera
			{
                if (RectTransformUtility.RectangleContainsScreenPoint(DisplayRectTransform, touchLocation, UICameraRef))
				{
					Vector2 localPoint;
					RectTransformUtility.ScreenPointToLocalPointInRectangle(DisplayRectTransform, touchLocation, UICameraRef, out localPoint);
                    Vector2 normalizedPoint = Rect.PointToNormalized(DisplayRectTransform.rect, localPoint);
                    RaycastToGameObject(ARCameraRef.ViewportPointToRay(normalizedPoint));
				}
			}
            else // Using raw AR Camera, not rendering to some UI
			{
                RaycastToGameObject(ARCameraRef.ScreenPointToRay(touchLocation));
			}

		}
	}

    private void RaycastToGameObject(Ray ray)
	{
		RaycastHit hitResult;
		if (Physics.Raycast(ray, out hitResult))
		{
			GameObject hitObject = hitResult.transform.gameObject;
			ARClickableComponent clickable = hitObject.GetComponent<ARClickableComponent>();
			if (clickable) {
				clickable.OnPress();
                // If a new object is touched, it can't be a double-press.
				bIsSecondPress &= (LastClickedComponent == clickable);
                LastClickedComponent = clickable;
			    OnPress();
			}
			Debug.Log(name + " has raycast result: " + hitObject.name + "- " + (clickable ? "Has ARClickable" : "No ARClickable"));
		}
        else Debug.Log("Raycast didn't hit any GameObjects.");
	}

    // TODO: Arreglar colisiones, el click no se detecta. Es necesario usar raycasts al parecer.
    public void OnPress()
    {
        if (!LastClickedComponent) return;
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
        Debug.Log("SinglePress called from " + (gameObject ? gameObject.name : "DestroyedGO") +
                  " - Hit GO is " + (LastClickedComponent ? LastClickedComponent.gameObject.name : "DestroyedGO"));
        LastClickedComponent.SinglePress();
    }

    void DoublePress()
    {
        bIsSecondPress = false; 
        Debug.Log("DoublePress called from " + (gameObject ? gameObject.name : "DestroyedGO") +
                  " - Hit GO is " + (LastClickedComponent ? LastClickedComponent.gameObject.name : "DestroyedGO"));
        LastClickedComponent.DoublePress();
    }
}
