using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorLockObject : MonoBehaviour
{
	public int bonesToAdvance;

	private DoorLock gameplayUI;
	private bool playerIsClose;

    public GameObject dialog;
    private bool levelClear = false;
    private GameObject player;

	public bool finalLevel;

    private void Start()
	{
		gameplayUI = GameObject.FindGameObjectWithTag("DoorLock").GetComponent<DoorLock>();
        player = (GameObject.FindGameObjectWithTag("Player"));
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
                levelClear = true;

                /*if(dialog.GetComponent<StoryHandler>().ac == false)
                {
                    Debug.Log("Change shit");
                }*/
            }
        }
        if (levelClear)
        {
             if (GameObject.FindObjectOfType<DialogeManager>().isActive == false)
             {
				if (!finalLevel)
				{
					SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
				}
				else
				{
					SceneManager.LoadSceneAsync(0);
				}
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
