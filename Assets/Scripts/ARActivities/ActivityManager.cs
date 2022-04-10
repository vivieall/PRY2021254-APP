using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Linq; 


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
    private int Fails=0;
    private int Success= 0;
    private Transform OverlayChildren;


    private GameObject CurrActivity;
    private ArrayList CurrGameobjs;
    private SoundManager soundManager;
    PersistanceHandler Handler;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ActivityNum);
        //Handler is searched when the scene is loads, PersistantObject is an object that carries on from the master scene
        Handler = GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>();
        ActivityNum = Handler.GetActivityNum();
        CurrActivity = ActivityHandler(ActivityNum);
        CurrGameobjs = ActivityObjectsHandler(ActivityNum);
        CurrActivity.SetActive(true);
        soundManager = FindObjectOfType<SoundManager>();
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
                Fails += 1;
                Debug.Log(Fails);
                if (Fails >= 3)
                {
                    TriggerFail();
                }
                return;
            };
        }
        TriggerSuccess();
    }

    public void AddFail()
    {
        Fails += 1;
        Debug.Log(Fails);

        if (Fails>=3)
        {
            TriggerFail();
        }
        else
        {
            //soundManager.IsLooping(true);
            //soundManager.SelectAudio(1, 0.5f);//incorrect?
            StartCoroutine(ErrorLoop());
        }
    }

    IEnumerator ErrorLoop()
    {
        soundManager.IsLooping(true);
        soundManager.SelecAudioLoop(1,0.5f);
        yield return new WaitForSeconds(3.0f);
        soundManager.IsLooping(false);
    }

    public void AddSuccess()
    {
        Success += 1;
        Debug.Log(Success);
        if (Success >= 3)
        {
            TriggerSuccess();
        }
        else
        {
            soundManager.SelectAudio(0, 0.5f);//correct?
        }
    }

    public void TriggerSuccess()
    {
        print("Activity is done");
        print("El nivel es: "+ Handler.GetNivel());
        CallRecordLevelApi(Int32.Parse(Handler.GetIdChild()), Handler.GetNivel(), delegate (levelRecord response)
        {
            //correcto
        });   
        ActivityHandler(ActivityNum).SetActive(false);
        CompletelvlScreen.SetActive(true);
        soundManager.SelectAudio(2, 0.5f);//victory?
    }

    public void TriggerFail()
    {
        print("Activity has failed");
        ActivityHandler(ActivityNum).SetActive(false);
        FailedlvlScreen.SetActive(true);
        soundManager.SelectAudio(3, 0.5f);//try_again?
    }

    public void ResetActivity()
    {
        print("Activity reset");
        ActivityHandler(ActivityNum).SetActive(true);
        if (FailedlvlScreen.activeSelf)
        {
            FailedlvlScreen.SetActive(false);
        }
        Fails = 0;
        Success = 0;
        OverlayChildren= OverlayOne.transform.Find("GameObjects");
        for (int i = 0; i < OverlayOne.transform.Find("GameObjects").transform.childCount; i++)
        {
            OverlayChildren.transform.GetChild(i).GetComponent<DragToUIObj>().Reset();
        }
    }

    public int GetCurrActivityNum()
    {
        return ActivityNum;
    }

    private class levelRecord
    {
        public int idChild;
        public int idLevel;
        public bool successful;
    }
    
    private void CallRecordLevelApi(int idChild, int idLevel, Action<levelRecord> response)
    {
        levelRecord lr = new levelRecord();
        lr.idChild = idChild;
        lr.idLevel = idLevel;
        lr.successful = true;
        string json = JsonUtility.ToJson(lr);
        StartCoroutine(PostRecordLevel("https://teapprendo.herokuapp.com/levelRecords/create", json, response));
    }

    IEnumerator PostRecordLevel(string url, string json, Action<levelRecord> response)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response(JsonUtility.FromJson<levelRecord>(uwr.downloadHandler.text));
        }
    }
}