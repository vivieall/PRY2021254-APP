using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItemSearcher : MonoBehaviour
{
	[SerializeField] public GameObject ScreenToSearch = null;

	public void SearchTerm(string search)
	{
		string lowerSearch = search.ToLower();
		ListItemManager[] items = ScreenToSearch.GetComponentsInChildren<ListItemManager>(true);
		foreach (ListItemManager lim in items)
		{
			lim.gameObject.SetActive((lim.text.text).ToLower().Contains(lowerSearch));
		}
	}

	public void OnEnable() { SearchTerm(""); }
	public void OnDisable() { SearchTerm(""); }
}
