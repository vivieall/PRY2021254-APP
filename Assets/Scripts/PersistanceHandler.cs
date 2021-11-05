using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistanceHandler : MonoBehaviour
{
    //Scene Canvas utilizara la Serializacion de los Hijos dependiendo de los indices de las vistas
    //private SceneUIManager SceneCamera;
    private static PersistanceHandler handlerInstance;

    private int UIState;

    void Start()
    {
        UIState = 0;
        //SceneCamera = GameObject.Find("UICamera").GetComponent<SceneUIManager>();
        DontDestroyOnLoad(gameObject);
        if (handlerInstance == null)
        {
            handlerInstance = this;
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }
    void Update() { }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PrevScene()
    {
        UIState = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void MainMenu()
    {
        UIState = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public int GetState()
    {
        return UIState;
    }

}