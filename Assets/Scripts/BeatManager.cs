using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.SceneManagement;

public class BeatManager : MonoBehaviour
{

    GameObject spawnPoint;

    [FMODUnity.EventRef]
    public string MusicEvent;

    FMODUnity.StudioEventEmitter emitter;

    public float currentBpm = 110;
    float beatTime;
    float currentBeatTime;
    int currentBeat = 0;
    float songDsp;

    public Slider slider;
    public Image sliderImage;
    public Player player;

    public delegate void OnBeat(int count);
    public static event OnBeat onBeat;

    public static BeatManager instance;
    

    public static void SetLoopStage(float amount)
    {
        instance.emitter.SetParameter("LoopStage", amount);
    }

    public static Player GetPlayer
    {
        get
        {
            return instance.player;
        }
    }

    public static float GetCurrentBeatTime
    {
        get
        {
            return instance.currentBeatTime;
        }
    }


    public static float GetCurrentBPM
    {
        get
        {
            return instance.currentBpm;
        }
    }

    public static void RestartPlayer()
    {
        GetPlayer.transform.position = instance.spawnPoint.transform.position;
		GetPlayer.RespawnBones();
    }

    void Start()
    {
        if (FindObjectsOfType<BeatManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        {
            instance = this;
        }

        if (slider)
        {
            sliderImage = slider.GetComponent<Image>();
        }

        if (FindObjectOfType<Player>())
        {
            player = FindObjectOfType<Player>();
        }

        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        emitter.Event = MusicEvent;
        songDsp = (float)AudioSettings.dspTime;
        emitter.Play();

        //.GetBus("AggressiveBass").setVolume(0);
        SetBPM(currentBpm);
	}



    public static void SetChased(float value)
    {
        instance.emitter.SetParameter("Chased", value);
    }

    void SetBPM(float bpm)
    {
        beatTime = 60 / bpm;
        currentBpm = bpm;
    }

    void FixedUpdate()
    {
        currentBeatTime += Time.fixedDeltaTime / beatTime;

        if (currentBeatTime > 1)
        {
            currentBeatTime -= 1;
            currentBeat++;

            currentBeat = currentBeat % 4;

            if (onBeat != null)
            {
                print(AudioSettings.dspTime);
                onBeat(currentBeat);
            }
        }

        if (slider)
        {
            slider.value = currentBeatTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (transform.childCount != 0)
        {
            spawnPoint = transform.GetChild(0).gameObject;
        }
    }
#endif
}
