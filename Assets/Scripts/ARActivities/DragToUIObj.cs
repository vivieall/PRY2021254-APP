using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragToUIObj: MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    //Vuforia Object es un objeto compuesto de 3 objetos en este orden de indices
    //0 -> Canvas
    //1 -> Objeto 3D a mostrar/modificar
    public GameObject VuforiaObject;

    private GameObject GoalObject;

    private RectTransform rectTransform;
    private Canvas GoalCanvas;
    private Object robj, wobj;
    private Vector2 zeroArea;
    private Vector3 DL;
    private Vector3 UR;
    private bool done;
    private ActivityManager evSys;
    public void OnBeginDrag(PointerEventData eventData)
    {
        return;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/ GoalCanvas.scaleFactor;
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
                    var rendobj = VuforiaObject.transform.GetChild(1).gameObject.transform;
                    Destroy(rendobj.GetChild(0).gameObject);
                    Instantiate(robj, rendobj);
                    gameObject.SetActive(false);
                    done = true;
                    evSys.CheckCompletion();
                }
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        evSys = GameObject.Find("EventSystem").GetComponent<ActivityManager>();
        robj = Resources.Load("Prefabs/CorrectAnswer");
        wobj = Resources.Load("Prefabs/WrongAnswer");
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

    public bool GetDone()
    {
        return done;
    }
}
