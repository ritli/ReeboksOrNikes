using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointHandler : MonoBehaviour {
    public bool AddPatrolPoint, RemovePatrolPoint;

    public List<GameObject> PatrolPoints;
    public Color DebugColor = Color.white;


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (AddPatrolPoint)
        {
            PatrolPoints.Add(new GameObject("Patrol Point :" + name + " " + PatrolPoints.Count));
            PatrolPoints[PatrolPoints.Count - 1].transform.parent = transform;
            AddPatrolPoint = false;
        }
        if (RemovePatrolPoint)
        {
            DestroyImmediate(PatrolPoints[PatrolPoints.Count - 1].gameObject);
            PatrolPoints.RemoveAt(PatrolPoints.Count-1);
            RemovePatrolPoint = false;
        }


    }

    private void OnDrawGizmos()
    {
        if (PatrolPoints.Count > 1)
        {
            for (int i = 1; i < PatrolPoints.Count; i++)
            {
                Gizmos.color = DebugColor;

                Gizmos.DrawLine(PatrolPoints[i - 1].transform.position, PatrolPoints[i].transform.position);

                if (i == PatrolPoints.Count - 1)
                {
                    Gizmos.DrawLine(PatrolPoints[i].transform.position, PatrolPoints[0].transform.position);
                    Gizmos.DrawCube(PatrolPoints[0].transform.position, Vector3.one * 0.2f);

                }

                Gizmos.DrawCube(PatrolPoints[i].transform.position, Vector3.one* 0.2f);

            }
        }
    }

#endif



    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
