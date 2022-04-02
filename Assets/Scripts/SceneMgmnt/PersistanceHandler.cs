using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneUIManager;

public class PersistanceHandler : MonoBehaviour {
    private static PersistanceHandler handlerInstance;
    private int UIState;
    private int ActivityNum;
    private int Nivel;
    private GameObject NivelGameObject;
    private string IdChild;
    private int childIdx;
    public int ChildIdx { get; set; }
    private LoginResponse loginResponse;
    public LoginResponse LoginResponse { get; set; }
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
    public void SetInfo(int nivel, string idChild){
        Nivel = nivel;
        IdChild = idChild;
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

    public int GetNivel(){
        return Nivel;
    }
    public string GetIdChild()
    {
        return IdChild;
    }
}