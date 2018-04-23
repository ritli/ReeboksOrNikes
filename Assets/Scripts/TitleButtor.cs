using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtor : MonoBehaviour {

    public Canvas CreditCanvas;


    public void doExitGame()
    {
        Application.Quit();
    }

    public void changeScene()
    {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}

    public void displayCredits()
    {
        CreditCanvas.gameObject.SetActive(true);
    }

    public void CloseCanvas()
    {
        CreditCanvas.gameObject.SetActive(false);
    }
}
