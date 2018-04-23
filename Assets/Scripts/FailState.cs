using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailState : MonoBehaviour
{
    public Texture fadeTexture;

	private int losses;
	private float lerpValue;
	private bool hittable = true;
	private bool fadeOut;
	private bool fadeIn;
	private Color fadeColor;
	private Color newColor;

    float toAlpha = 0;
    float currentToAlpha = 0;

    float fadeSpeed = 15;

    private void OnGUI()
    {
        GUI.color = newColor;
        GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), fadeTexture);
    }

    private void Start()
	{
		BeatManager.onBeat += OnBeat;
		newColor = fadeColor;
	}

	void OnBeat(int count)
	{
		if (fadeOut)
		{
            toAlpha += 1.1f;
			if (toAlpha > 1)
			{
				fadeOut = false;
				fadeIn = true;

                BeatManager.RestartPlayer();
			}
		}

		else if (fadeIn)
		{
            BeatManager.GetPlayer.movementDisabled = false;

            toAlpha -= 0.5f;
			if (toAlpha < 0)
			{
                toAlpha = 0f;
				fadeIn = false;
                hittable = true;
            }
		}
	}

    public void RespawnPlayer()
    {
        if (hittable)
        {
            print("Respawning player");
            fadeOut = true;
            hittable = false;

            BeatManager.GetPlayer.movementDisabled = true;
        }
    }

	private void Update()
	{
        currentToAlpha = Mathf.Lerp(currentToAlpha, toAlpha, Time.deltaTime * fadeSpeed);
        newColor.a = currentToAlpha;

        if (Input.GetKeyDown(KeyCode.Y))
		{
			fadeOut = true;
            hittable = false;

            BeatManager.GetPlayer.movementDisabled = true;
        }
	}
}
