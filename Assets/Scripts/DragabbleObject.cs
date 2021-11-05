using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragabbleObject : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private Vector2 zeroArea;
    public GameObject GoalObject;
    private Canvas GoalCanvas;

    private Vector3 DL;
    private Vector3 UR;

    public void OnBeginDrag(PointerEventData eventData)
    {
        return;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        EvaluateOverlap();
        rectTransform.anchoredPosition = zeroArea;
    }

    public void EvaluateOverlap()
    {
        if (GoalCanvas.isActiveAndEnabled)
        {
            if (rectTransform.position.x >= DL.x && rectTransform.position.x <= UR.x)
            {
                if (rectTransform.position.y >= DL.y && rectTransform.position.y <= UR.y)
                {
                    GoalObject.transform.parent.parent.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                }
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        zeroArea = GetComponent<RectTransform>().anchoredPosition;
        GoalCanvas = GoalObject.transform.parent.GetComponent<Canvas>();
        DL = new Vector3(GoalObject.transform.position.x - rectTransform.rect.width / 2, GoalObject.transform.position.y - rectTransform.rect.height / 2, GoalObject.transform.position.z);
        UR = new Vector3(GoalObject.transform.position.x + rectTransform.rect.width / 2, GoalObject.transform.position.y + rectTransform.rect.height / 2, GoalObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
