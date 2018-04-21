using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerTile : MonoBehaviour
{
	public bool startMode;
	public Collider col;
	
	private Animator anim;
	private GameObject player;

	void OnBeat(int count)
	{
		if (count == 1)
		{
			anim.SetBool("onOrOff", !anim.GetBool("onOrOff"));
		}
	}

	void Start ()
	{
		BeatManager.onBeat += OnBeat;
		anim = GetComponent<Animator>();
		anim.SetBool("onOrOff", startMode);
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
	{
		if (anim.GetBool("onOrOff"))
		{
			Online();
		}
	}

	void Online()
	{
		Collider[] cols = Physics.OverlapBox(transform.position, transform.localScale / 2);

		for (int i = 0; i < cols.Length; i++)
		{
			if (cols[i] == player.GetComponent<Collider>())
			{
				Debug.Log("Alarm went off!");
			}
		}
	}
}
