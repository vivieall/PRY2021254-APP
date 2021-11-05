using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    //Esto ya no serviría porque ya no se tiene  MyAdmin
    public string RegisterHostUrl = "http://localhost/asdasd/register.php";
    public string LoginHostUrl = "http://localhost/asdasd/register.php";
    public Text m_SendText;

    #region REGISTERUSER
    public void SubmitRegister(string user, string email, string pass, Action<Response> response)
    {
        StartCoroutine(Co_CreateUser(user, email, pass, response));
    }

    IEnumerator Co_CreateUser(string user, string email, string pass, Action<Response> response)
    {
        Seguridad form = new Seguridad();
        form.secureForm.AddField("userName", user);
        form.secureForm.AddField("email", email);
        form.secureForm.AddField("pass", pass);

        WWW www = new WWW(RegisterHostUrl, form.secureForm);
        yield return www;
        Debug.Log(www.text);

        response(JsonUtility.FromJson<Response>(www.text));
    }

    #endregion




    #region LOGINUSER

    public void LoginUser(string user, string pass, Action<Response> response)
    {
        StartCoroutine(Co_LoginUser(user, pass, response));
    }

    IEnumerator Co_LoginUser(string user, string pass, Action<Response> response)
    {
        Seguridad form = new Seguridad();
        form.secureForm.AddField("userName", user);
        form.secureForm.AddField("pass", pass);

        WWW www = new WWW(LoginHostUrl, form.secureForm);
        yield return www;
        Debug.Log(www.text);

        response(JsonUtility.FromJson<Response>(www.text));
    }
    #endregion


    public void CallRegister(string user, string pass, string email, string names, string lastnames, string birthday,
        string id, Action<Response> response)
    {
        StartCoroutine(Register(user, pass, email, names, lastnames, birthday, id, response));
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


    public void CallLogin(string user, string pass)
    {
        StartCoroutine(Login(user, pass));
    }

    IEnumerator Login(string user, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("password", pass);
        form.AddField("username", user);
        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
        yield return www;

        Debug.Log(www.text);
        //response(JsonUtility.FromJson<Response>(www.text));
    }
}

[System.Serializable]
public class Response
{
    public bool       done = false;
    public string     message = "";
}