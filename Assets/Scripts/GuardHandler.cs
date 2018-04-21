using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardHandler : MonoBehaviour {

    Pathfinding.AIPath path;
    SpriteRenderer sprite;
    Animator animator;

    Vector2 forwardVector;
    public LayerMask obstacleMask;

    public float visionAngle, visionRange;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector2 v = Quaternion.Euler(0, 0, -visionAngle) * Vector2.left * visionRange;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)v);
        v = Quaternion.Euler(0, 0, visionAngle) * Vector2.left * visionRange;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)v);

    }

    void Start () {
        path = GetComponent<Pathfinding.AIPath>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        animator.SetFloat("SpeedMultiplier", BeatManager.GetCurrentBPM / 60);
    }
	
	void Update () {
        VisionUpdate();

        if (path.velocity2D.x > 0)
        {
            sprite.flipX = true;
            forwardVector = Vector2.right;
        }
        else
        {
            sprite.flipX = false;
            forwardVector = Vector2.left;
        }
    }

    void VisionUpdate()
    {
        Vector2 dir = (BeatManager.GetPlayer.transform.position - transform.position).normalized * visionRange;

        //print(Vector2.Distance(BeatManager.GetPlayer.transform.position, transform.position));

        if (Vector2.Distance(BeatManager.GetPlayer.transform.position, transform.position) < visionRange)
        {
            if (Mathf.Abs(Vector2.Angle(dir, new Vector2(-1, 0).normalized)) < visionAngle)
            {
                if (!Physics2D.Raycast(transform.position, dir, visionRange, obstacleMask)){
                }
            }
        }
    }

}

