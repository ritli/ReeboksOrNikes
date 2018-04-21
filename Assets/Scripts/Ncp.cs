using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ncp : MonoBehaviour {

    public Dialoge dialoge;
    private bool Started = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space"))
        {
            if (!Started)
            {
                //Debug.Log("Swag Cat Name   " + dialoge.name);
                FindObjectOfType<DialogeManager>().startDialoge(dialoge);
                Started = true;
            }
            else
            {
                FindObjectOfType<DialogeManager>().DisplayNextDialoge();
            }
        }
    }

    
}
