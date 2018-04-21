using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoTile : MonoBehaviour
{
	public bool startMode;
	public Collider col;
	
	private Animator anim;
	private bool animBool;
	private GameObject player;

	void OnBeat(int count)
	{
		if (count == 3)
		{
			animBool = !animBool;
		}
	}

	void Start ()
	{
		BeatManager.onBeat += OnBeat;
		anim = GetComponent<Animator>();
		animBool = anim.GetBool("onOrOff");
		animBool = startMode;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
	{
		if (animBool)
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
