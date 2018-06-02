using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerPlacer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SpriteRenderer[] renderers;

        renderers = FindObjectsOfType<SpriteRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sortingOrder = -Mathf.FloorToInt(renderers[i].transform.position.y * 10) + 1000;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
