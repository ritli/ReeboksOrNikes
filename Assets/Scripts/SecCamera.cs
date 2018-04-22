using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecCamera : MonoBehaviour
{
	public List<Vector3> rotations;

	private int beatCounter;
	private int listNumber;
	private float dangerLevel;
	private Color coneColor;

	void Start()
	{
		BeatManager.onBeat += OnBeat;
		listNumber = 0;
		transform.eulerAngles = rotations[listNumber];
		coneColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
	}

	void Update()
	{
		if (dangerLevel > 0 && !GetComponent<Animator>().GetBool("PlayerInView"))
		{
			dangerLevel -= Time.deltaTime * 0.5f;
			if (dangerLevel < 0)
			{
				dangerLevel = 0;
			}
		}

		coneColor.a = dangerLevel;
		if (coneColor.a < 0.25f)
		{
			coneColor.a = 0.25f;
		}
		transform.GetChild(0).GetComponent<SpriteRenderer>().color = coneColor;
	}

	void OnBeat(int count)
	{
		beatCounter++;
		if (beatCounter >= 8)
		{
			SwitchRotation();
			beatCounter = 0;
		}
	}

	void SwitchRotation()
	{
		listNumber++;
		if (listNumber >= rotations.Count)
		{
			listNumber = 0;
		}
		transform.eulerAngles = rotations[listNumber];
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		GetComponent<Animator>().SetBool("PlayerInView", true);
		dangerLevel += Time.deltaTime;
		if (dangerLevel >= 3)
		{
			BeatManager.GetPlayer.GetComponentInChildren<FailState>().RespawnPlayer();
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		GetComponent<Animator>().SetBool("PlayerInView", false);
	}
}
