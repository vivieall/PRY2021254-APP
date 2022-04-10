using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Linq;

public class RecordManager : MonoBehaviour
{
    [Serializable]
	private class LevelRecord
	{
        public int idLevelRecord;
		public Level level;
	}
    [Serializable]
	private class Level
	{
        public int idLevel;
		public string description;
	}

    private LevelRecord[] allLevels;
    private string IdChild;
    private List<GameObject> buttonList;
    [SerializeField] Text messageText;

    public void LoadRecord()
    {
        IdChild = GameObject.Find("UICamera").GetComponent<SceneUIManager>().getIdChild();
        Debug.Log("ID BUSCADO: " + IdChild);
        StartCoroutine(GetRecord(IdChild));
    }

    public void ShowRecord()
    {
        buttonList = new List<GameObject>();
        GameObject ButtonTemplate = gameObject.transform.Find("Panel").transform.Find("PanelMenu").transform.Find("ZonaVertical").transform.Find("ZonaFillasBotton").gameObject;
        GameObject g;
        int N = allLevels.Length;
        if(N > 0)
        {
            for(int i = 0; i < N; i++) 
            {
                g = Instantiate(ButtonTemplate, transform.Find("Panel").transform.Find("PanelMenu").transform.Find("ZonaVertical"));
                g.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Levels/" + allLevels[i].level.idLevel.ToString());
                buttonList.Add(g.gameObject);
            }
            messageText.text = "Niveles iniciados";
        }
        else {
            messageText.text = "No se ha registrado actividad";
        }
        ButtonTemplate.SetActive(false);
    }

    public void DestroyRecord()
    {
        GameObject ButtonTemplate = gameObject.transform.Find("Panel").transform.Find("PanelMenu").transform.Find("ZonaVertical").transform.Find("ZonaFillasBotton").gameObject;
        ButtonTemplate.SetActive(true);
        foreach(GameObject go in buttonList){
            Destroy(go);
        }
        buttonList.Clear();
    }

    IEnumerator GetRecord(string IdChild)
    {
        string url = "https://teapprendo.herokuapp.com/levelRecords/listByIdChild?idChild=" + IdChild;
        UnityWebRequest request = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        request.SetRequestHeader("Authorization", token);
        yield return request.SendWebRequest();

        if (request.isNetworkError) 
        {
			Debug.Log("Error While Sending: " + request.error);
		} 
        else 
        {
			allLevels = JsonHelper.FromJson<LevelRecord>(fixJson(request.downloadHandler.text));
            ShowRecord();
		}
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}
