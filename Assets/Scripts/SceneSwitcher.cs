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
    public void playGame()
    {
        Handler.NextScene();
    }
    public void Back()
    {
        Handler.PrevScene();
    }
    public void BackHome()
    {
        Handler.MainMenu();
    }

}