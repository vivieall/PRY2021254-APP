using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    PersistanceHandler Handler;
    void Start()
    {
        Handler = GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>();
    }
    //SceneType
    //1->Math 2->Comm 3->PerSoc 4->C&T
    public void playGame(string parameters)
    //1->Math 2->Comm 3->PerSoc 4->C&T
    {
        string temp = parameters.Substring(0,1);
        int scenenum = int.Parse(temp);
        temp = parameters.Substring(1, 1);
        int activitynum = int.Parse(temp);
        Handler.PlayActivity(scenenum, activitynum);
    }
    //SceneType
    //1->Math 2->Comm 3->PerSoc 4->C&T
    public void Back(int scenType)
    {
        Handler.ReturnToUI(scenType);
    }
    public void BackHome()
    {
        Handler.MainMenu();
    }
}