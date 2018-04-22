using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class one_line_Npc : MonoBehaviour {

    Animator animator;

    public bool hasDialoge = false;
    private GameObject bubble;

    private bool activeDialoge;


    // Use this for initialization
    void Start ()
    {
        bubble = transform.GetChild(0).gameObject;

        animator = GetComponent<Animator>();

        animator.SetFloat("SpeedMultiplier", BeatManager.GetCurrentBPM / 60);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(activeDialoge)
        {
            StartCoroutine(LateCall());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            Debug.Log("in collision");
            if (hasDialoge)
            { 
                bubble.SetActive(true);
                activeDialoge = true;
            }
        }
    }

    IEnumerator LateCall()
    {

        yield return new WaitForSeconds(3);

        bubble.gameObject.SetActive(false);
        activeDialoge = false;
        //Do Function here...
    }
}
