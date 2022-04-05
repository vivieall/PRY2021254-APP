using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListLevelManager : MonoBehaviour
{
    private List<GameObject> ListLevels;

    void Start()
    {
        //ListLevels = new List<GameObject>();
        //GetLevels();
    }

    public void GetLevels()
    {
        ListLevels.Add(transform.GetChild(0).gameObject);
        ListLevels.Add(transform.GetChild(1).gameObject);        ListLevels.Add(transform.GetChild(2).gameObject);
        GameObject g;
        for(int i=0; i<3; i++) 
        {
            g = Instantiate(ListLevels[i],transform);
        }
        foreach(GameObject go in ListLevels)
            Destroy(go);
    }
}
