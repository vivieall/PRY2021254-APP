using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistanceHandler : MonoBehaviour {
    private static PersistanceHandler handlerInstance;
    private int UIState;
    private int ActivityNum;

    void Start() {
        /*UIStates:
         * 0 -> Login Screen
         * 1 -> Return to Seleccionar Categoria
         * 2 -> Return to Matematicas
         * 3 -> Return to Comunicacion
         * 4 -> Return to Personal Social
         * 5 -> Return to Ciencias
         */
        UIState = 0;
        DontDestroyOnLoad(gameObject);
        if (handlerInstance == null) {
            handlerInstance = this;
        }
        else {
            Object.Destroy(gameObject);
        }
    }

    void Update() { }
    public void PlayActivity(int scenenum,int activitynum){
        ActivityNum = activitynum;
        SceneManager.LoadScene(scenenum);
    }
    public void ReturnToUI(int NUM){
        UIState = NUM;
        SceneManager.LoadScene(0);
    }
    public void MainMenu(){
        UIState = 1;
        SceneManager.LoadScene(0);
    }
    public int GetState(){
        return UIState;
    }
    public int GetActivityNum()
    {
        return ActivityNum;
    }
}