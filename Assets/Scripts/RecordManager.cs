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
		public string description;
	}

    private LevelRecord[] allLevels;
    
    private string ChildId;
    PersistanceHandler Handler;

    void Start()
    {
        StartCoroutine(GetRecord());
        
        /*Handler = GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>();
        ChildId = "1"; //Handler.GetIdChild();
        Debug.Log("Este ID: "+ ChildId);*/

        //ShowRecord();
    }

    public void ShowRecord()
    {
        GameObject ButtonTemplate = transform.GetChild(0).gameObject;
        GameObject g;
        int N = allLevels.Length;
        for(int i = 0; i < N; i++) {
            g = Instantiate(ButtonTemplate, transform);
            g.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = allLevels[i].level.description;
        }

        Destroy(ButtonTemplate);
    }

    IEnumerator GetRecord()
    {
        string url = "https://teapprendo.herokuapp.com/levelRecords/listByIdChild?idChild=1";
        UnityWebRequest request = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        request.SetRequestHeader("Authorization", token);
        yield return request.SendWebRequest();

        if (request.isNetworkError) {
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
