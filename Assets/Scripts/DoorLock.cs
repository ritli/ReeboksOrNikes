using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
	public Transform winEffect;

	[HideInInspector] public bool active;

	private int wins;

	void Start()
	{
		BeatManager.onBeat += OnBeat;
	}

	void OnBeat(int count)
	{
		if (Input.GetButtonDown("Jump") && IsOnBeat && (transform.eulerAngles.z < 25f || transform.eulerAngles.z > 335f))
		{
			if (transform.eulerAngles.z < 3 || transform.eulerAngles.z > 358)
			{
				wins += 2;
				Win();
			}
			else
			{
				wins++;
				Win();
			}

			if (wins >= 3)
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
		//Fade
		active = false;
	}

	bool IsOnBeat
	{
		get
		{
			float time = BeatManager.GetCurrentBeatTime;

			return (time > 0.6f || time < 0.4f);
		}
	}
}
