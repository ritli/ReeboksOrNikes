using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    float movePower = 0;
    float movePowerDecayMult = 1;

    public float moveSpeed = 10;
    public float slowMoveSpeed = 2;
    bool canMove = true;

    Rigidbody2D rigidbody;

    void OnBeat(int count)
    {
      //  movePower = 1;
    } 

	void Start () {
        BeatManager.onBeat += OnBeat;
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        InputUpdate();

//         movePower -= movePowerDecayMult * Time.deltaTime;
//         movePower = Mathf.Clamp01(movePower);
	}

    void InputUpdate()
    {
        if (Input.GetButtonDown("Fire1") && canMove)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
            dir = IsOnBeat ? dir * moveSpeed : dir * slowMoveSpeed;

            rigidbody.AddForce(dir, ForceMode2D.Impulse);
        }
    }

    bool IsOnBeat
    {
        get
        {
            float time = BeatManager.GetCurrentBeatTime;

            return (time > 0.7f || time < 0.3f);
        }
    }
}
