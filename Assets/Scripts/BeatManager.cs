using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Slider slider;
    public Image sliderImage;
    public Player player;

    public delegate void OnBeat(int count);
    public static event OnBeat onBeat;

    public static BeatManager instance;



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
        emitter.Play();

        emitter.SetParameter("LoopStage", 1);

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

    void Update()
    {
        currentBeatTime += Time.deltaTime / beatTime;

        if (currentBeatTime > 1)
        {
            currentBeatTime -= 1;
            currentBeat++;

            currentBeat = currentBeat % 4;

            if (onBeat != null)
            {
                onBeat(currentBeat);
            }
        }

        if (slider)
        {
            slider.value = currentBeatTime;
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
