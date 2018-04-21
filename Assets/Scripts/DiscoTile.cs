using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoTile : MonoBehaviour
{
	public bool startMode;
	public Collider col;

	private bool onOrOff;
	
	void Start ()
	{
		onOrOff = startMode;
	}

	void Update ()
	{
		if (onOrOff)
		{
			Online();
		}
		else
		{
			Offline();
		}
	}

	void Online()
	{
		Collider[] cols = Physics.OverlapBox(transform.position, transform.localScale / 2);

		for (int i = 0; i < cols.Length; i++)
		{
			if (cols[i] == /*player*/ col)
			{
				Debug.Log("Alarm went off!");
			}
		}
	}

	void Offline()
	{

	}
}
