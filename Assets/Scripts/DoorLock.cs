using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
	public int winsToClear;
	public float speed;
	public Transform winEffect;

	[HideInInspector] public bool active;

	private int wins;
	private bool starter;

	void Start()
	{
		BeatManager.onBeat += OnBeat;
		//speed *= BeatManager.GetCurrentBPM / 60;
	}

	void OnBeat(int count)
	{
		if (!starter)
		{
			active = true;
			starter = true;
		}

		if (Input.GetButtonDown("Jump") && IsOnBeat && (transform.eulerAngles.z < 20f || transform.eulerAngles.z > 340f))
		{
			if (transform.eulerAngles.z < 3 || transform.eulerAngles.z > 358)
			{
				wins += 2;
				Win();
				print("SUPER WIN!");
			}
			else
			{
				wins++;
				Win();
				print("WIN!");
			}

			if (wins >= winsToClear)
			{
				Open();
			}
		}
	}

	private void Update()
	{
		if (active)
		{
			Spin();
		}
	}

	void Spin()
	{
		float newRotateValue = transform.eulerAngles.z;
		newRotateValue = BeatManager.GetCurrentBeatTime * 360;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newRotateValue);
	}

	void Win()
	{
		winEffect.GetComponent<Animator>().SetTrigger("Win");
	}

	void Open()
	{

	}

	bool IsOnBeat
	{
		get
		{
			float time = BeatManager.GetCurrentBeatTime;

			return (time > 0.7f || time < 0.3f);
		}
	}
}
