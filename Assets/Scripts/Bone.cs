using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<Player>().PickedUpBone();
			gameObject.SetActive(false);
		}
	}
}
