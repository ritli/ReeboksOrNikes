using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteChanger : MonoBehaviour {

    public Sprite noHower;
    public Sprite Hower;

    Image image;

    // Use this for initialization
    void Start () {

        gameObject.GetComponent<Image>().sprite = noHower;

    }
	
    public void howerButton()
    {
        Debug.Log("hower");
        gameObject.GetComponent<Image>().sprite = Hower;
    }

    public void noHowerButton()
    {
        Debug.Log("no hower");
        gameObject.GetComponent<Image>().sprite = noHower;
    }
}
