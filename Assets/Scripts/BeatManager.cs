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
    float songPos, songPosInBeats;
    public Slider slider;
    public Image sliderImage;
    public Player player;
    public Image beatImage;

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

        SetBPM(currentBpm);
	}



    public static void SetChased(float value)
    {
        instance.emitter.SetParameter("Chased", value);
    }

    void SetBPM(float bpm)
    {
        beatTime = 60f / bpm;
        currentBpm = bpm;
    }


    private void Update()
    {
        //calculate the position in seconds
        songPos = (float)(AudioSettings.dspTime - songDsp);

        //calculate the position in beats
        songPosInBeats = songPos / beatTime;


        currentBeatTime = songPosInBeats - Mathf.FloorToInt(songPosInBeats);

        if (Mathf.FloorToInt(songPosInBeats) % 4 != currentBeat)
        {
            //currentBeatTime -= 1;
            currentBeat = (currentBeat +1) % 4;

            if (onBeat != null)
            {
                onBeat(currentBeat);

                /*
                Color c = beatImage.color;
                c.a = 1;
                beatImage.color = c;
                */
            }
        }

        if (slider)
        {
            /*
            Color c = beatImage.color;

            c.a -= Time.deltaTime;
            beatImage.color = c;
            */
            slider.value = currentBeatTime;
        }

    }

    void FixedUpdate()
    {
       // currentBeatTime += Time.fixedDeltaTime / beatTime;


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
