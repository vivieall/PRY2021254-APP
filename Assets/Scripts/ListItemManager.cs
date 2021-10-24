using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItemManager : MonoBehaviour
{
	[SerializeField] public Text text;
	[SerializeField] public ListManager ListManager = null;

	public void RemoveFromList()
	{
		if (ListManager)
		{
			ListManager.RemoveItem(gameObject);
		}
		else Debug.Log(name + " is not part of a list");
	}
}
