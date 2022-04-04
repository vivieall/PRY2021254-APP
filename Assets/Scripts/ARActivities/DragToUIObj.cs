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
    [SerializeField] private string CorrectAnswer;

    public GameObject CurrentObject;

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
        //nombre = VuforiaObject.transform.GetChild(GameObjectNumber).gameObject.transform.GetChild(0).name;
        EvaluateOverlap();
        rectTransform.anchoredPosition = zeroArea;
    }

    //public void EvaluateOverlap()
    //{
    //    Debug.Log(GoalCanvas.isActiveAndEnabled);
    //    if (GoalCanvas.isActiveAndEnabled)
    //    {
    //        if (rectTransform.position.x >= DL.x && rectTransform.position.x <= UR.x)
    //        {
    //            if (rectTransform.position.y >= DL.y && rectTransform.position.y <= UR.y)
    //            {
    //                SetDone(true);
    //                SwitchState();
    //                //gameObject.SetActive(false);
    //                evSys.CheckCompletion();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (rectTransform.position.x >= DL.x && rectTransform.position.x <= UR.x)
    //        {
    //            if (rectTransform.position.y >= DL.y && rectTransform.position.y <= UR.y)
    //            {
    //                SwitchState();
    //                //gameObject.SetActive(false);
    //                evSys.CheckCompletion();
    //            }
    //        }
    //    }
    //}

    public void EvaluateOverlap()
    {
        if (GoalCanvas.isActiveAndEnabled)
        {
            if (rectTransform.position.x >= DL.x && rectTransform.position.x <= UR.x)
            {
                if (rectTransform.position.y >= DL.y && rectTransform.position.y <= UR.y)
                {
                    var renderers = VuforiaObject.transform.GetChild(GameObjectNumber).gameObject.GetComponentsInChildren<Renderer>(true);
                    foreach (var item in renderers)
                    {
                        Debug.Log(item.enabled);
                    }
                    if (renderers[0] == true)
                    {
                        nombre = VuforiaObject.transform.GetChild(GameObjectNumber).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).name;
                        Debug.Log(nombre);
                        //if (nombre=="Bottle"+GameObjectNumber.ToString())
                        //if (nombre != "CorrectAnswer" || nombre != "WrongAnser")
                        if (nombre == CorrectAnswer)
                        {
                            SetDone(true);
                            SwitchState(true);
                            //gameObject.SetActive(false);
                            evSys.CheckCompletion();
                        }
                        else
                        {
                            SetDone(false);
                            SwitchState(false);
                            Debug.Log("not found!");
                        }


                        //if (nombre == "RenderObject"+ GameObjectNumber.ToString())
                        //{
                        //    SetDone(true);
                        //    SwitchState();
                        //    //gameObject.SetActive(false);
                        //    evSys.CheckCompletion();
                        //}
                        //else
                        //{
                        //    //.GetComponentsInChildren<Renderer>(true)
                        //    //SwitchState();
                        //    //gameObject.SetActive(false);
                        //    //evSys.CheckCompletion();
                        //    Debug.Log("not found!");
                        //}

                    }
                }

            }
        }
    }

    void Start()
    {
        CorrectAnswer= VuforiaObject.transform.GetChild(GameObjectNumber).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).name;
        evSys = GameObject.Find("EventSystem").GetComponent<ActivityManager>();
        rectTransform = GetComponent<RectTransform>();
        zeroArea = GetComponent<RectTransform>().anchoredPosition;
        GoalCanvas = VuforiaObject.transform.GetChild(0).GetComponent<Canvas>();
        Debug.Log(GoalCanvas);
        GoalObject = VuforiaObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        DL = new Vector3(GoalObject.transform.position.x - rectTransform.rect.width / 2, GoalObject.transform.position.y - rectTransform.rect.height / 2, GoalObject.transform.position.z);
        UR = new Vector3(GoalObject.transform.position.x + rectTransform.rect.width / 2, GoalObject.transform.position.y + rectTransform.rect.height / 2, GoalObject.transform.position.z);
        InitializeResource(1,1);
    }

    void InitializeResource(int actnum,int objnum)
    {
        string aux = "Act" + actnum + "_Obj"+objnum;
        var ActivityObj = Resources.Load(aux);
        robj = Resources.Load("Prefabs/CorrectAnswer");
        wobj = Resources.Load("Prefabs/WrongAnswer");
    }

    private void SwitchState(bool result)
    {

        //var rendobj = VuforiaObject.transform.GetChild(1).gameObject.transform; //esto cambie por gameobjectnumber y que agarre a su hijo -hamill
        var rendobj = VuforiaObject.transform.GetChild(GameObjectNumber).gameObject.transform.GetChild(0).gameObject.transform;

        //Destroys current 3d object
        Destroy(rendobj.GetChild(0).gameObject);

        //if true, show 3d checkmark, else show 3d activity object
        if (result) {
            //Instantiates new 3d object with the prev object transform properties
            Instantiate(robj, rendobj);
        } else
        {
            Instantiate(wobj, rendobj);
        }
    }

    //private void SwitchState()
    //{
    //    //Switches done state of object
    //    //done = !done;
    //    //Switches the ImageTarget# UI
    //    //VuforiaObject.transform.GetChild(0).gameObject.SetActive(!done);  //comentado para que no desaparesca
    //    //Grabs current 3d object transform properties  

    //    //var rendobj = VuforiaObject.transform.GetChild(1).gameObject.transform; //esto cambie por gameobjectnumber y que agarre a su hijo -hamill
    //    var rendobj = VuforiaObject.transform.GetChild(GameObjectNumber).gameObject.transform.GetChild(0).gameObject.transform;

    //    //Destroys current 3d object
    //    Destroy(rendobj.GetChild(0).gameObject);


    //    //if true, show 3d checkmark, else show 3d activity object
    //    if (done)
    //    {
    //        //Instantiates new 3d object with the prev object transform properties
    //        Instantiate(robj, rendobj);
    //    }
    //    else
    //    {
    //        Instantiate(wobj, rendobj);
    //    }
    //}


    // Update is called once per frame
    void Update(){}

    public void Reset()
    {
        switch (evSys.GetCurrActivityNum())
        {
            default:
                break;
        }
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
