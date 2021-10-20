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
}

[System.Serializable]
public class Response
{
    public bool       done = false;
    public string     message = "";
}