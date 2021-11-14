using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShowFilterScreenButton : MonoBehaviour
{
    [SerializeField] public GameObject SelectFilterScreenRoot;
    // [SerializeField] public string NextFilterButtonTag = "NextFilterButton";
    [SerializeField] public NextFilterButtonComponent NextFilterButton;
    [SerializeField] public GameObject FacePrefab;

    public void SendPrefabData()
	{
        // Sadly, you can't find inactive game objects by tag, so scrap this idea for now and just set the NFB manually.

      //  GameObject[] nextFilterButtons = GameObject.FindGameObjectsWithTag(NextFilterButtonTag);
      //  if (FacePrefab && nextFilterButtons.Length > 0)
	  //  {
	  //  	ShowFilterScreenButton[] filterButtons = SelectFilterScreenRoot.GetComponentsInChildren<ShowFilterScreenButton>();
      //      NextFilterButtonComponent nextFilterButton = nextFilterButtons[0].GetComponent<NextFilterButtonComponent>();
	  //  	nextFilterButtons[0].GetComponent<NextFilterButtonComponent>().ReadFilterScreenButtonData(filterButtons, this);
      //      if (nextFilterButton)
      //          nextFilterButton.ReadFilterScreenButtonData(filterButtons, this);
	  //  }
      //  else Debug.Log(gameObject.name + ": error sending face prefabs data");

        if (FacePrefab)
	    {
	    	ShowFilterScreenButton[] filterButtons = SelectFilterScreenRoot.GetComponentsInChildren<ShowFilterScreenButton>(true);
            NextFilterButton.ReadFilterScreenButtonData(filterButtons, this);
	    }
        else Debug.Log(gameObject.name + ": error sending face prefabs data");
	}
    
}
