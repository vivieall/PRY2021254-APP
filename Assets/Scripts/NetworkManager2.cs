using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;


public class NetworkManager2 : MonoBehaviour
{


    




    public void CallRegister(string user, string pass, string email, string names, string lastnames, string birthday,
        string id, Action<Response> response)
    {
        StartCoroutine(Register(user,pass,email,names,lastnames,birthday,id,response));
    }

    IEnumerator Register(string user, string pass, string email, string names, string lastnames, string birthday,
        string id, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", pass);
        form.AddField("names", names);
        form.AddField("lastnames", lastnames);
        form.AddField("username", user);
        form.AddField("birthday", birthday);
        form.AddField("id", id);
        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;

        Debug.Log(www.text);
        response(JsonUtility.FromJson<Response>(www.text));
    }


    public void CallLogin(string user, string pass, Action<Response> response)
    {
        Debug.Log("??");
        StartCoroutine(Login(user, pass, response));
    }

    IEnumerator Login(string user, string pass, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("password", pass);
        form.AddField("username", user);
        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
        yield return www;

        
        response(JsonUtility.FromJson<Response>(www.text));
    }

}