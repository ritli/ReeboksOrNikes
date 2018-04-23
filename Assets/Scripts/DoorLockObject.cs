using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorLockObject : MonoBehaviour
{
	public int bonesToAdvance;

	private DoorLock gameplayUI;
	private bool playerIsClose;

    private GameObject dialog;

    private void Start()
	{
		gameplayUI = GameObject.FindGameObjectWithTag("DoorLock").GetComponent<DoorLock>();
        dialog = GameObject.Find("Dialog_Cate");
    }

	void Update()
	{
		if (Input.GetButtonDown("Jump") && playerIsClose)
		{
			gameplayUI.active = true;
			if (gameplayUI.wins >= 3)
			{
                dialog.GetComponent<StoryHandler>().startDialgoe = true;
				gameplayUI.active = false;
				MakeVisible(false);
                /*if(dialog.GetComponent<StoryHandler>().ac == false)
                {
                    Debug.Log("Change shit");
                }*/
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && BeatManager.GetPlayer.bones >= bonesToAdvance)
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
