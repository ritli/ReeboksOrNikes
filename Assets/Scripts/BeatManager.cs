using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatManager : MonoBehaviour {

    [FMODUnity.EventRef]
    public string MusicEvent;

    FMODUnity.StudioEventEmitter emitter;

    float currentBpm;
    float beatTime;
    float currentBeatTime;
    int currentBeat = 0;

    public Slider slider;
    public Image sliderImage;

	void Start () {
        sliderImage = slider.GetComponent<Image>();

        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        emitter.Event = MusicEvent;
        emitter.Play();
        SetBPM(109);
    }

    void SetBPM(float bpm)
    {
        beatTime = 60 / bpm;
        currentBpm = bpm;
    }

	void Update () {
        currentBeatTime += Time.deltaTime / beatTime;
        currentBeatTime = currentBeatTime % 1;

        slider.value = currentBeatTime;

	}

    IEnumerator OnBeat()
    {

    }
}
