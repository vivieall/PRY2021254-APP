using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragToUIObj: MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    //Vuforia Object es un objeto compuesto de 2 objetos en este orden de indices
    //0 -> Canvas
    //1 -> Objeto 3D a mostrar/modificar DENTRO DEL RENDEROBJECT
    [SerializeField] public GameObject VuforiaObject;
    [SerializeField] private int GameObjectNumber;

    public GameObject CurrentObject;
    private GameObject CurrentObject2;

    private GameObject GoalObject;
    private string nombre;
    private RectTransform rectTransform;
    private Canvas GoalCanvas;
    private Object robj, wobj;
    private Vector2 zeroArea;
    private Vector3 DL;
    private Vector3 UR;
    private bool done = false;
    private ActivityManager evSys;

    private Object fluff;
    private Transform rendobj;
    private Transform rendobj2;


    private SoundManager soundManager;

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
                    Debug.Log(CurrentObject2.name);
                    Debug.Log(CurrentObject.name);

                    if (CurrentObject == CurrentObject2)
                    {
                        SetDone(true);
                        SwitchState(true);
                        evSys.AddSuccess();
                    }
                    else
                    {
                        SwitchState(false);
                        Debug.Log("not found!");
                        evSys.AddFail();
                    }
                }

            }
        }
    }

    void Start()
    {
        evSys = GameObject.Find("EventSystem").GetComponent<ActivityManager>();
        rectTransform = GetComponent<RectTransform>();
        zeroArea = GetComponent<RectTransform>().anchoredPosition;
        GoalCanvas = VuforiaObject.transform.GetChild(0).GetComponent<Canvas>();
        GoalObject = VuforiaObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        DL = new Vector3(GoalObject.transform.position.x - rectTransform.rect.width / 2, GoalObject.transform.position.y - rectTransform.rect.height / 2, GoalObject.transform.position.z);
        UR = new Vector3(GoalObject.transform.position.x + rectTransform.rect.width / 2, GoalObject.transform.position.y + rectTransform.rect.height / 2, GoalObject.transform.position.z);
        soundManager = FindObjectOfType<SoundManager>();
        InitializeResource(1,1);
    }

    public void InitializeResource(int actnum,int objnum)
    {
        string aux = "Act" + actnum + "_Obj"+objnum;
        var ActivityObj = Resources.Load(aux);
        robj = Resources.Load("Prefabs/CorrectAnswer");
        wobj = Resources.Load("Prefabs/WrongAnswer");
    }
    public void SetAnswer(GameObject correct)
    {
        //Debug.Log(correct.name);
        CurrentObject2 = correct;
    }

    public GameObject GetAnswer()
    {
        return CurrentObject;
    }

    private void SwitchState(bool result)
    {
        rendobj = CurrentObject.gameObject.transform.GetChild(0).gameObject.transform;

        //if true, show 3d checkmark, else show 3d activity object
        if (result) {
            //Instantiates new 3d object with the prev object transform properties
            rendobj.GetChild(0).gameObject.SetActive(false);
            rendobj2 = CurrentObject2.transform.GetChild(0).gameObject.transform;
            fluff = Instantiate(robj, rendobj2);
            //soundManager.SelectAudio(0, 0.5f);//correct?

            this.gameObject.SetActive(false);
        } else
        {
            Debug.Log(rendobj.name);
            StartCoroutine(Fade());
            //soundManager.SelectAudio(1, 0.5f);//incorrect?
        }
    }

    IEnumerator Fade()
    {
        rendobj2 = CurrentObject2.transform.GetChild(0).gameObject.transform;
        CurrentObject2.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        fluff=Instantiate(wobj, rendobj2);
        rectTransform.anchoredPosition = zeroArea;
        Destroy(fluff, 3);
        yield return new WaitForSeconds(3.0f);  
        Debug.Log("Bai");
        CurrentObject2.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    void Update(){}

    public void Reset()
    {
        this.gameObject.SetActive(true);
        switch (evSys.GetCurrActivityNum())
        {
            default:
                break;
        }
        Destroy(fluff);
        SetDone(false);
    }

    public bool GetDone()
    {
        return done;
    }


    /// <summary>
    /// Pone la variable done como el valor dado
    /// </summary>
    public void SetDone(bool valor)
    {
        done = valor;
    }
}
