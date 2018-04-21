using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
	public int successToClear;
	public float speed;

	void Activate()
	{

	}

	void Spin()
	{
		transform.RotateAround((Vector2)transform.position + (Vector2.right * 5f), transform.position, -1f * speed);
	}

	void CreateNewGoal()
	{

	}
}
