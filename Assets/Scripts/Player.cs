using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    float movePower = 0;
    float movePowerDecayMult = 1;

    public float moveSpeed = 10;
    public float slowMoveSpeed = 2;
    bool canMove = true;

    SpriteRenderer sprite;
    Rigidbody2D rigidbody;
    Animator animator;

	[HideInInspector] public int bones;
	public GameObject UIBone;

    public int chaserCount = 0;
    bool chaserCountUpdated = false;
    public bool movementDisabled = false;
    bool audioPrimed = false;


    public void AddChaser()
    {

        chaserCountUpdated = true;
        chaserCount++;
    }

    public void RemoveChaser()
    {
        chaserCountUpdated = true;

        chaserCount--;

        chaserCount = Mathf.Clamp(chaserCount, 0, int.MaxValue);
    }

	void OnBeat(int count)
    {
        //  movePower = 1;
    }

    void Start()
    {
        BeatManager.onBeat += OnBeat;
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        animator.SetFloat("SpeedMultiplier", BeatManager.GetCurrentBPM / 60);
    }

    void Update()
    {
        if (chaserCount > 0)
        {
            if (chaserCountUpdated)
            {
                chaserCountUpdated = false;
                BeatManager.SetChased(1);

            }
        }
        else if (chaserCountUpdated)
        {
            chaserCountUpdated = false;

            BeatManager.SetChased(0);
        }

        InputUpdate();
		if (Input.GetKeyDown(KeyCode.H))
		{
			PickedUpBone();
		}
    }

    void InputUpdate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;

        if (!movementDisabled)
        {
            if (Input.GetButtonDown("Fire1") && canMove)
            {
                canMove = false;

                if (IsOnBeat)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Jump");
                    dir = dir * moveSpeed;
                }
                else
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/JumpFail");
                    dir = dir * slowMoveSpeed;
                }

                Invoke("ResetCanJump", 30 / BeatManager.GetCurrentBPM);
                rigidbody.AddForce(dir, ForceMode2D.Impulse);
                animator.Play("Jump");
            }
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

    void ResetCanJump()
    {
        canMove = true;
    }

    bool IsOnBeat
    {
        get
        {
            float time = BeatManager.GetCurrentBeatTime;

            return (time > 0.7f || time < 0.3f);
        }
    }

	public void PickedUpBone()
	{
		GameObject newBone;
		bones++;
		newBone = Instantiate(UIBone, UIBone.transform.parent);
		newBone.transform.position += new Vector3(100*bones, 0, 0);
	}
}
