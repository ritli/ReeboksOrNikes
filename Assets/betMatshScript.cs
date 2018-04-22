using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betMatshScript : MonoBehaviour {

    Animator animator;

    void Start () {
        animator = GetComponent<Animator>();

        animator.SetFloat("SpeedMultiplier", BeatManager.GetCurrentBPM / 60);
    }
	
}
