using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChaser : MonoBehaviour
{

    float chaseDelay = 0;
    Transform player;
    float currentLerpTime, lerpTime;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }



    void Update()
    {
        if (chaseDelay > 0.5f)
        {
            float t = currentLerpTime / lerpTime;
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Mathf.Clamp(Vector2.Distance(transform.position, player.transform.position), 0, 50));
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        }

        chaseDelay += Time.deltaTime;

    }
}
