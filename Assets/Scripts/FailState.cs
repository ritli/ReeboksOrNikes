using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailState : MonoBehaviour
{
	public Transform respawn;
	public GameObject fadeScreen;

	private int losses;
	private float lerpValue;
	private bool hitable;
	private bool fadeOut;
	private bool fadeIn;
	private Color fadeColor;
	private Color newColor;

	private void Start()
	{
		BeatManager.onBeat += OnBeat;
		fadeColor = fadeScreen.GetComponent<Image>().color;
		newColor = fadeColor;
	}

	void OnBeat(int count)
	{
		if (fadeOut && (count == 0 || count == 2))
		{
			newColor.a += 0.25f;
			fadeScreen.GetComponent<Image>().color = newColor;
			if (newColor.a > 1)
			{
				newColor.a = 1f;
				fadeOut = false;
				fadeIn = true;
			}
		}

		if (fadeIn && (count == 0 || count == 2))
		{
			newColor.a -= 0.25f;
			fadeScreen.GetComponent<Image>().color = newColor;
			if (newColor.a < 0)
			{
				newColor.a = 0f;
				fadeIn = false;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy") && hitable)
		{
			fadeOut = true;
			hitable = false;
		}
	}
}
