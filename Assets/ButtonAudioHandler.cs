using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudioHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler {

    float hoverPlayTime;

    void PlayHoverSound()
    {
        if (Time.time - hoverPlayTime > 0.5f)
        {
            hoverPlayTime = Time.time;

            FMODUnity.RuntimeManager.PlayOneShot("event:/Hover");
        }
    }

    void PlayClickSound()
    {
        if (Time.time - hoverPlayTime > 0.1f)
        {
            hoverPlayTime = Time.time;

            FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayClickSound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();
    }
}
