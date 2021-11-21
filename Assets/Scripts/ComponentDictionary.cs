using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComponentDictionary : MonoBehaviour
{
    [SerializeField] private string[] names;
    [SerializeField] private Component[] data;

    public Component Get(string name)
	{
		for(int i = 0; i < names.Length; i++)
		{
			if (names[i].Equals(name))
				return i < data.Length ? data[i] : null;
			i++;
		}
		return null;
	}
}
