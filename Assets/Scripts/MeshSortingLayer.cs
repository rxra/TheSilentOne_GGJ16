using UnityEngine;
using System.Collections;

public class MeshSortingLayer : MonoBehaviour {

	public string layerName = "Default";

	void Awake()
	{
		GetComponent<Renderer>().sortingLayerName = layerName;
	}

}
