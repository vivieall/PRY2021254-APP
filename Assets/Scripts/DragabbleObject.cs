using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragabbleObject : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private Canvas canvas;
    //Vuforia Object es un objeto compuesto de 3 objetos en este orden de indices
    //0 -> Canvas
    //1 -> Objeto 3D a mostrar
    //2 -> Objeto 3d a mostrar al completar la funcion
    public GameObject VuforiaObject;

    private GameObject GoalObject;

    private RectTransform rectTransform;
    private Canvas GoalCanvas;

    private Vector2 zeroArea;
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
                    VuforiaObject.transform.GetChild(0).gameObject.SetActive(false);
                    VuforiaObject.transform.GetChild(1).gameObject.SetActive(false);
                    VuforiaObject.transform.GetChild(2).gameObject.SetActive(true);
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
        GoalCanvas = VuforiaObject.transform.GetChild(0).GetComponent<Canvas>();
        GoalObject = VuforiaObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        DL = new Vector3(GoalObject.transform.position.x - rectTransform.rect.width / 2, GoalObject.transform.position.y - rectTransform.rect.height / 2, GoalObject.transform.position.z);
        UR = new Vector3(GoalObject.transform.position.x + rectTransform.rect.width / 2, GoalObject.transform.position.y + rectTransform.rect.height / 2, GoalObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
