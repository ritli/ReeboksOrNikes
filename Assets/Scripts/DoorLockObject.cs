using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorLockObject : MonoBehaviour
{
	private DoorLock gameplayUI;
	private bool playerIsClose;

	private void Start()
	{
		gameplayUI = GameObject.FindGameObjectWithTag("DoorLock").GetComponent<DoorLock>();
	}

	void Update()
	{
		if (Input.GetButton("Jump") && playerIsClose)
		{
			gameplayUI.active = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		print("Iz cloze yes");
		if (collision.CompareTag("Player"))
		{
			playerIsClose = true;
			MakeVisible(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			playerIsClose = false;
			MakeVisible(false);
		}
	}

	void MakeVisible(bool visibility)
	{
		Color newColor = gameplayUI.transform.parent.GetComponent<Image>().color;
		if (visibility)
		{
			newColor.a = 1;
		}
		else if (!visibility)
		{
			newColor.a = 0;
		}
		gameplayUI.transform.parent.GetComponent<Image>().color = newColor;
	}
}
