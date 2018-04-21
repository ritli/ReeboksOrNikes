using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    float movePower = 0;
    float movePowerDecayMult = 1;

    public float moveSpeed = 10;
    public float slowMoveSpeed = 2;
    bool canMove = true;

    SpriteRenderer sprite;
    Rigidbody2D rigidbody;
    Animator animator;

    void OnBeat(int count)
    {
      //  movePower = 1;
    } 

	void Start () {
        BeatManager.onBeat += OnBeat;
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        animator.SetFloat("SpeedMultiplier", BeatManager.GetCurrentBPM / 60);
    }

    void Update () {
        InputUpdate();
	}

    void InputUpdate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;

        if (Input.GetButtonDown("Fire1") && canMove)
        {
            dir = IsOnBeat ? dir * moveSpeed : dir * slowMoveSpeed;

            rigidbody.AddForce(dir, ForceMode2D.Impulse);
            animator.Play("Jump");
        }

        if (dir.x > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
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
