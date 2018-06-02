using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtor : MonoBehaviour {

    public Canvas CreditCanvas;
    bool creditCanvasOpen;
    Vector2 creditsPosition;

    public void Start()
    {
        creditsPosition = CreditCanvas.transform.position;
        CreditCanvas.transform.position = new Vector2(Screen.width, Screen.height);
    }

    public void doExitGame()
    {
        Application.Quit();
    }

    public void changeScene()
    {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}

    private void Update()
    {
        if (creditCanvasOpen)
        {
            CreditCanvas.transform.position = Vector2.Lerp(CreditCanvas.transform.position, creditsPosition, Time.deltaTime * 4);

        }
        else
        {
            CreditCanvas.transform.position = Vector2.Lerp(CreditCanvas.transform.position, creditsPosition - Vector2.down * Screen.height, Time.deltaTime * 8);

        }
    }

    public void displayCredits()
    {
        creditCanvasOpen = true;
        CreditCanvas.gameObject.SetActive(true);
    }

    public void CloseCanvas()
    {
        creditCanvasOpen = false;
    }
}
