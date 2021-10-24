using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{

    [SerializeField] private ArrayList AllListItems;
	[SerializeField] private ArrayList AllListInstancedItems;
    [SerializeField] private GameObject ContentPanel;
    [SerializeField] private GameObject ListItemPrefab;
	[SerializeField] private bool allowDuplicates = true;

    void Start()
    {
        // Deberia leerse la lista guardada del usuario
        // Ejemplo usando solo strings
        AllListItems = new ArrayList();
        AllListInstancedItems = new ArrayList();
    }

    public void AddItem(string item)
	{
        if (allowDuplicates || !AllListItems.Contains(item)) {
            AllListItems.Add(item);
			GameObject newItem = Instantiate(ListItemPrefab) as GameObject;
			ListItemManager lim = newItem.GetComponent<ListItemManager>();
			if (lim == null)
			{
				Debug.Log(item + " gave null ListItemManager");
			}
			lim.text.text = item;
			lim.ListManager = this;
			newItem.transform.parent = ContentPanel.transform;
			newItem.transform.localScale = Vector3.one;
			AllListInstancedItems.Add(newItem);
		}
	}

    public void RemoveItem(GameObject item)
	{
        if (AllListInstancedItems.Contains(item)) {
			int i = AllListInstancedItems.IndexOf(item);
			Destroy(item);
			AllListItems.RemoveAt(i);
			AllListInstancedItems.RemoveAt(i);
			// TODO: Eliminar de base de datos / etc.
		}
		else Debug.Log(item.name + " was not in list");
	}

    public void UpdateList(string CurrentSearch)
	{
		foreach(GameObject item in AllListInstancedItems)
		{
			ListItemManager lim = item.GetComponent<ListItemManager>();
			item.SetActive(lim.text.text.Contains(CurrentSearch));
		}
	}

    public void UpdateList(Text CurrentSearch)
	{
		UpdateList(CurrentSearch.text);
	}


}
