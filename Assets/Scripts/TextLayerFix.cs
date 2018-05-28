using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLayerFix : MonoBehaviour
{
	void Start ()
	{
		transform.GetChild(0).gameObject.GetComponent<Renderer>().sortingOrder = 2001;
	}
}
