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

	private void Awake()
	{
        // Deberia leerse la lista guardada del usuario
        // Ejemplo usando solo strings
        AllListItems = new ArrayList();
        AllListInstancedItems = new ArrayList();
	}

	public void AddItem(string item)
	{
		AddItem_internal(item);
	}

    public bool AddItem_internal(string item)
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
			newItem.transform.SetParent(ContentPanel.transform);
			newItem.transform.localScale = Vector3.one;
			AllListInstancedItems.Add(newItem);
			return true;
		}
		return false;
	}

	public void RemoveItem(GameObject item)
	{
		RemoveItem_internal(item);
	}

    public bool RemoveItem_internal(GameObject item)
	{
		// Nota: ahorita solo funciona para eliminar desde dentro de la lista
		// Se tendría que hacer otra comparación para validar que ambos gameobjects representen el mismo item
        if (AllListInstancedItems.Contains(item)) {
			int i = AllListInstancedItems.IndexOf(item);
			Destroy(item);
			AllListItems.RemoveAt(i);
			AllListInstancedItems.RemoveAt(i);
			// TODO: Eliminar de base de datos / etc.
			return true;
		}
		Debug.Log(item.name + " was not in list");
		return false;
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

	public bool ListContains(GameObject item)
	{
		// Placeholder: No buscar por referencia, es inutil.
		// Cada botón debería tener un identificador
		// y se debería buscar en base a ese identificador
		return AllListInstancedItems.Contains(item);
	}


}
