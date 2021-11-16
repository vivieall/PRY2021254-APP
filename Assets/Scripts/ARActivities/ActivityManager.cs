using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    [Header("Activity Complete")]
    [SerializeField] private GameObject CompletelvlScreen;
    [Header("Activity One")]
    [SerializeField] private GameObject OverlayOne;
    [SerializeField] private GameObject gameobj01;
    [SerializeField] private GameObject gameobj02;
    [SerializeField] private GameObject gameobj03;
    private bool ActivityisDone = false;
    PersistanceHandler Handler;

    // Start is called before the first frame update
    void Start()
    {
        Handler = GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>();
        int _auxvar = Handler.GetActivityNum();
        print(_auxvar);
        switch (_auxvar)
        {
            case 1:
                OverlayOne.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update(){}

    public void CheckCompletion()
    {
        if (!ActivityisDone)
            if (gameobj01.GetComponent<DragToUIObj>().GetDone() &&
            gameobj02.GetComponent<DragToUIObj>().GetDone() &&
            gameobj03.GetComponent<DragToUIObj>().GetDone())
            {
                ActivityisDone = true;
                print("Game is done");
                OverlayOne.SetActive(false);
                CompletelvlScreen.SetActive(true);
            }
    }
}