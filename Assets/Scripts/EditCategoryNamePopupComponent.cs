using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Linq;

public class EditCategoryNamePopupComponent : MonoBehaviour
{
    [SerializeField] public GameObject UpdateCategoryNameConfirmationPopup;

    [Serializable]
	private class UpdateCategoryNameData
	{
        public int idChild;
		public string name;
	}
    private string IdChild;

    public void LoadIdChild()
    {
        IdChild = GameObject.Find("UICamera").GetComponent<SceneUIManager>().getIdChild();
    }

    public UnityEvent OnAccept;
    public UnityEvent OnDecline;
    
    public void ExecuteOnAccept() { 
        UpdateData();
        OnAccept.Invoke();
        gameObject.SetActive(false);
        UpdateCategoryNameConfirmationPopup.SetActive(true);
    }
	public void ExecuteOnDecline() { 
        OnDecline.Invoke(); 
        gameObject.SetActive(false); }

	public void ClearAllEvents()
	{
		OnAccept.RemoveAllListeners();
		OnDecline.RemoveAllListeners();
	}

    public void UpdateData()
    {
        string Name = gameObject.transform.Find("NameInput").transform.Find("Text").GetComponent<Text>().text;

        StartCoroutine(UpdateCategoryName(Name));
    }

    IEnumerator UpdateCategoryName(string Name)
    {
        UpdateCategoryNameData nd = new UpdateCategoryNameData();
        nd.idChild = Int32.Parse(IdChild);
        nd.name = Name;
        string json = JsonUtility.ToJson(nd);

        string url = "https://teapprendo.herokuapp.com/children/updateSpecialCategoryName";

        var uwr = new UnityWebRequest(url, "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json); 
        uwr.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }
}
