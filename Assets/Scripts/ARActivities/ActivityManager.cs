using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    [Header("Event Scenes")]
    [SerializeField] private GameObject CompletelvlScreen;
    [SerializeField] private GameObject FailedlvlScreen;
    //Custom Construction should be handled by the respective handlers
    [Header("Activity One")]
    [SerializeField] private GameObject OverlayOne;
    [SerializeField] private GameObject gameobj01;
    [SerializeField] private GameObject gameobj02;
    [SerializeField] private GameObject gameobj03;

    private int ActivityNum;
    private GameObject CurrActivity;
    private ArrayList CurrGameobjs;
    PersistanceHandler Handler;
    // Start is called before the first frame update
    void Start()
    {
        //Handler is searched when the scene is loads, PersistantObject is an object that carries on from the master scene
        Handler = GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>();
        ActivityNum = Handler.GetActivityNum();
        CurrActivity = ActivityHandler(ActivityNum);
        CurrGameobjs = ActivityObjectsHandler(ActivityNum);
        CurrActivity.SetActive(true);
        
    }
    // Update is called once per frame
    void Update(){}
    public GameObject ActivityHandler(int num) {
        GameObject aux;
        switch (num)
        {
            case 1:
                aux = OverlayOne;
                break;
            default:
                return null;
        }
        return aux;
    }
    public ArrayList ActivityObjectsHandler(int num)
    {
        ArrayList aux = new ArrayList();
        switch (num)
        {
            case 1:
                aux.Add(gameobj01);
                aux.Add(gameobj02);
                aux.Add(gameobj03);
                break;
            default:
                return null;
        }
        return aux;
    }
    
    public void CheckCompletion()
    {
        foreach (GameObject gombj in CurrGameobjs)
        {
            if (!gombj.GetComponent<DragToUIObj>().GetDone())
            {
                return;
            };
        }
        TriggerSuccess();
    }

    public void TriggerSuccess()
    {
        print("Activity is done");
        ActivityHandler(ActivityNum).SetActive(false);
        CompletelvlScreen.SetActive(true);
    }

    public void TriggerFail()
    {
        print("Activity has failed");
        ActivityHandler(ActivityNum).SetActive(false);
        FailedlvlScreen.SetActive(true);
    }

    public void ResetActivity()
    {
        print("Activity reset");
        ActivityHandler(ActivityNum).SetActive(true);
        if (FailedlvlScreen.activeSelf)
        {
            FailedlvlScreen.SetActive(false);
        }
    }

    public int GetCurrActivityNum()
    {
        return ActivityNum;
    }
}