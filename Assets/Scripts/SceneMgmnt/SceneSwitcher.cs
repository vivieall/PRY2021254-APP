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
    
    public void playGame(string parameters)
    {
        //SceneType
        //1->Math 2->Comm 3->PerSoc 4->C&T
        if (parameters.Length==2)
        {
            string temp = parameters.Substring(0,1);
            int scenenum = int.Parse(temp);
            temp = parameters.Substring(1, 1);
            int activitynum = int.Parse(temp);
            Handler.PlayActivity(scenenum, activitynum);
        }
        if (parameters.Length == 3)
        {
            string temp = parameters.Substring(0, 2);
            int scenenum = int.Parse(temp);
            temp = parameters.Substring(2, 1);
            int activitynum = int.Parse(temp);
            Handler.PlayActivity(scenenum, activitynum);
        }
    }

    public void setInfo(GameObject obj)
    {
        SceneUIManager sn = gameObject.GetComponent<SceneUIManager>();
        Handler.SetInfo(sn.getNivel(),sn.getIdChild());
    }

    //SceneType
    //1->Math 2->Comm 3->PerSoc 4->C&T
    public void Back(int scenType)
    {
        Handler.ReturnToUI(scenType);
    }

    public void levelCompleted(int scenType)
    {
        //AGREGAR
        Handler.ReturnToUI(scenType);
    }

    public void BackHome()
    {
        Handler.MainMenu();
    }
}