using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtor : MonoBehaviour {

    public Canvas CreditCanvas;


    public void doExitGame()
    {
        Application.Quit();
    }

    public void changeScene()
    {
        //SceneManager.LoadScene("SceneName", LoadSceneMode.single);
        Debug.Log("Change scene");
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
