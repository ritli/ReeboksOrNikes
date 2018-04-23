using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerTile : MonoBehaviour
{
	public bool startMode;
	
	private Animator anim;
	private int secondCount;
	private bool currentMode;

	void OnBeat(int count)
	{
		if (count == 0)
		{
			secondCount++;
			if (secondCount == 2)
			{
				anim.SetBool("onOrOff", !anim.GetBool("onOrOff"));
				secondCount = 0;
			}
		}
	}

	void Start ()
	{
		BeatManager.onBeat += OnBeat;
		anim = GetComponent<Animator>();
		anim.SetBool("onOrOff", startMode);
	}

	void Update ()
	{
		currentMode = anim.GetBool("onOrOff");
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		if (currentMode && collision.CompareTag("Feet"))
		{
			BeatManager.GetPlayer.GetComponentInChildren<FailState>().RespawnPlayer();
		}
	}
}
