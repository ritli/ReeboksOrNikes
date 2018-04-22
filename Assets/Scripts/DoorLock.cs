using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
	public Transform winEffect;

	[HideInInspector] public bool active;
	[HideInInspector] public int wins;

	void Start()
	{
		BeatManager.onBeat += OnBeat;
	}

	void OnBeat(int count)
	{
	}

	private void Update()
	{
		if (active)
		{
			Spin();

			if (Input.GetButtonDown("Jump") && IsOnBeat && (transform.eulerAngles.z < 30f || transform.eulerAngles.z > 330f))
			{
				if (transform.eulerAngles.z < 5 || transform.eulerAngles.z > 355)
				{
					wins += 2;
					Win();
				}
				else
				{
					wins++;
					Win();
				}
			}
		}
		
		if (wins >= 3)
		{
			Open();
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

			return (time > 0.7f || time < 0.3f);
		}
	}
}
