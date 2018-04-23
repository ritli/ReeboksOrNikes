using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryHandler : MonoBehaviour {

    public Dialoge dialoge;
    private bool Started = false;
    public bool startDialgoe = false;
    public bool done = false;

    // Use this for initialization
    void Start()
    {
        //startDialgoe = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (startDialgoe)
        {
            if (!Started)
            {
                Debug.Log("Name   " + dialoge.name);
                FindObjectOfType<DialogeManager>().startDialoge(dialoge);
                Started = true;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("CAlling");
                int i = FindObjectOfType<DialogeManager>().DisplayNextDialoge();
                
                 if(i == 0)
                 {
                    kill();
                 }
            }
        }
    }
    public void kill()
    {

        Debug.Log("Killing");
        gameObject.SetActive(false);
    }
}
