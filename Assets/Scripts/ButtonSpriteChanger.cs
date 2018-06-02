using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSpriteChanger : MonoBehaviour, IPointerClickHandler{

    public Sprite noHower;
    public Sprite Hower;

    float hoverPlayTime;

    Image image;

    public bool holdSprite = false;
    public bool toggled;

    // Use this for initialization
    void Start () {

        gameObject.GetComponent<Image>().sprite = noHower;

    }


    public void howerButton()
    {

        gameObject.GetComponent<Image>().sprite = Hower;
    }

    public void noHowerButton()
    {


        if (!toggled || !holdSprite)
        {
            gameObject.GetComponent<Image>().sprite = noHower;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (holdSprite)
        toggled = !toggled;
    }
}
