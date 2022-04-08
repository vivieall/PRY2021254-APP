using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Linq;

public class RecreacionManager : MonoBehaviour
{
    [Serializable]
	private class ChildData
	{
        public string specialCategoryName;
	}

    private string IdChild;
    private string NameCategory;

    public void Start()
    {
        IdChild = GameObject.Find("UICamera").GetComponent<SceneUIManager>().getIdChild();
        StartCoroutine(GetChild(IdChild));
    }

    public void ShowName(string NameCategory)
    {
        gameObject.transform.Find("RecreacionButton").transform.Find("Text").GetComponent<Text>().text = NameCategory;
    }

    IEnumerator GetChild(string IdChild)
    {
        string url = "https://teapprendo.herokuapp.com/children/listByIdChild?idChild=" + IdChild;

        UnityWebRequest uwr = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) 
        {
			Debug.Log("Error While Sending: " + uwr.error);
		} 
        else 
        {
            //Debug.Log("Received: " + uwr.downloadHandler.text);
			NameCategory = JsonUtility.FromJson<ChildData>(uwr.downloadHandler.text).specialCategoryName;
            ShowName(NameCategory);
		}
    }

    void Update()
    {
        //Debug.Log("Esperando Cambios");
        IdChild = GameObject.Find("UICamera").GetComponent<SceneUIManager>().getIdChild();
        StartCoroutine(GetChild(IdChild));
    }
}
