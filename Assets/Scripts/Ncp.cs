using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ncp : MonoBehaviour {

    public Dialoge dialoge;
    private bool Started = false;
    private bool startDialgoe = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (startDialgoe)
        {
            if (!Started)
            {
                //Debug.Log("Swag Cat Name   " + dialoge.name);
                FindObjectOfType<DialogeManager>().startDialoge(dialoge);
                Started = true;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<DialogeManager>().DisplayNextDialoge();
            }
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("Hej");
                startDialgoe = true;
            }
        }
    }

}
