using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryHandler : MonoBehaviour {

    public Dialoge dialoge;
    private bool Started = false;
    public bool startDialgoe = false;
    public bool done = false;
    [HideInInspector]
    public bool triggeredByDoor = false;
    DoorLockObject doorParent;

    public bool isDave = false;

    float clickDelay = 0.5f;
    float currentClickTime = 0;

    // Use this for initialization
    void Start()
    {
        doorParent = FindObjectOfType<DoorLockObject>();

        //startDialgoe = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startDialgoe)
        {
            currentClickTime += Time.deltaTime;

            if (currentClickTime > clickDelay)
            {
                if (!Started)
                {
                    Debug.Log("Name   " + dialoge.name);
                    FindObjectOfType<DialogeManager>().startDialoge(dialoge);
                    Started = true;

                    if (isDave)
                    FMODUnity.RuntimeManager.PlayOneShot("event:/JumpFail");


                }
                else if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.E))
                {
                    //Debug.Log("CAlling");
                    int i = FindObjectOfType<DialogeManager>().DisplayNextDialoge();

                    if (isDave)
                        FMODUnity.RuntimeManager.PlayOneShot("event:/JumpFail");

                    if (i == 0)
                    {
                        currentClickTime = 0;
                        Invoke("kill", 0.5f);
                    }
                }
            }


        }
    }
    public void kill()
    {
        if (triggeredByDoor)
        {
            doorParent.EndLevel();
        }

        gameObject.SetActive(false);
    }
}
