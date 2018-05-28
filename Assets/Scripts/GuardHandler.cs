using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardHandler : MonoBehaviour {

    Pathfinding.AIPath path;
    SpriteRenderer sprite;
    Animator animator;

    Vector2 forwardVector = Vector2.left;


    [Header("Vision Options")]

    public LayerMask obstacleMask;
    public float visionAngle, visionRange, alertVisionRange, idleVisionRange, timeToDetect, stayOnTargetTime;
    float timeInVisionCone, currentStayOnTargetTime;
    public ContactFilter2D filter2D;
    public float killRadius;

    [Header("Patrol Options")]

    public PatrolPointHandler patrolpoints;
    int currentPatrolPoint = 0;
    public float distanceToSwitchPatrolPoint;

    MeshFilter viewMeshFilter;
    Mesh viewMesh;

    [Header("Cone Visualize Options")]
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    bool playerVisible, playerDetected, playerTargetSet;

    MeshRenderer coneMeshRenderer;

    Vector3 spawnPoint;

    SpriteRenderer alertMark;
    float alertmarkOffset;

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector2 v = Quaternion.Euler(0, 0, -visionAngle) * forwardVector * visionRange;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)v);
        v = Quaternion.Euler(0, 0, visionAngle) * forwardVector * visionRange;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)v);


        Gizmos.DrawWireSphere(transform.position, killRadius);
    }
#endif
    void Start() {
        visionRange = idleVisionRange;

        alertMark = transform.Find("Alert").GetComponent<SpriteRenderer>();
        alertmarkOffset = alertMark.transform.localPosition.x;
        alertMark.enabled = false;

        viewMeshFilter = GetComponentInChildren<MeshFilter>();
        coneMeshRenderer = GetComponentInChildren<MeshRenderer>();

        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;


        path = GetComponent<Pathfinding.AIPath>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        animator.SetFloat("SpeedMultiplier", BeatManager.GetCurrentBPM / 60);

        GetComponent<Pathfinding.AIDestinationSetter>().target = patrolpoints.PatrolPoints[0].transform;

        spawnPoint = transform.position;

        BeatManager.onRestart += Restart;
    }

    void Restart()
    {
        transform.position = spawnPoint;
        GetComponent<Pathfinding.AIDestinationSetter>().target = patrolpoints.PatrolPoints[0].transform;
        playerVisible = false;
        playerDetected = false;
    }

    void PatrolUpdate()
    {

        if (Vector2.Distance(BeatManager.GetPlayer.transform.position, transform.position) < killRadius)
        {
            BeatManager.GetPlayer.GetComponentInChildren<FailState>().RespawnPlayer();

            currentStayOnTargetTime = stayOnTargetTime;
            playerDetected = false;
        }

        if (playerVisible)
        {

            if (playerDetected)
            {
                if (!playerTargetSet)
                {
                    currentStayOnTargetTime = 0;
                    BeatManager.GetPlayer.AddChaser();
                    playerTargetSet = true;
                    path.GetComponent<Pathfinding.AIDestinationSetter>().target = BeatManager.GetPlayer.transform;
                    alertMark.enabled = true;

                    FMODUnity.RuntimeManager.PlayOneShot("event:/AngryCat");
                }

                currentStayOnTargetTime = Mathf.Clamp(currentStayOnTargetTime, 0, int.MaxValue) - Time.deltaTime;

            }

        }
        else
        {
            //coneMeshRenderer.material.SetColor("Emission", alertColor);


            currentStayOnTargetTime = Mathf.Clamp(currentStayOnTargetTime, 0, int.MaxValue) + Time.deltaTime;

            if (playerTargetSet && currentStayOnTargetTime > stayOnTargetTime)
            {
                currentStayOnTargetTime = 0;

                playerDetected = false;
                timeInVisionCone = 0;
                BeatManager.GetPlayer.RemoveChaser();
                playerTargetSet = false;
                path.GetComponent<Pathfinding.AIDestinationSetter>().target = patrolpoints.PatrolPoints[currentPatrolPoint].transform;

                alertMark.enabled = false;
            }
        }

        if (!playerDetected && Vector2.Distance(transform.position, patrolpoints.PatrolPoints[currentPatrolPoint].transform.position) < distanceToSwitchPatrolPoint)
        {
            //coneMeshRenderer.material.SetColor("Emission", idleColor);

            currentPatrolPoint = currentPatrolPoint + 1;

            if (currentPatrolPoint == patrolpoints.PatrolPoints.Count)
            {
                currentPatrolPoint = 0;
            }

            path.GetComponent<Pathfinding.AIDestinationSetter>().target = patrolpoints.PatrolPoints[currentPatrolPoint].transform;
        }
    }

    void VisionUpdate()
    {
        if (playerVisible)
        {
            timeInVisionCone += Time.deltaTime;

            if (timeInVisionCone > timeToDetect)
            {
                playerDetected = true;
            }
        }
        else
        {
            /*
            timeInVisionCone -= Time.deltaTime;

            if (timeInVisionCone <= 0)
            {
                playerDetected = false;
            }
            */
        }

        timeInVisionCone = Mathf.Clamp(timeInVisionCone, 0, timeToDetect + 1);

        Vector2 dir = (BeatManager.GetPlayer.transform.position - transform.position).normalized * visionRange;
        float distToPlayer = Vector2.Distance(BeatManager.GetPlayer.transform.position, transform.position);

        if (Vector2.Distance(BeatManager.GetPlayer.transform.position, transform.position) < visionRange && Mathf.Abs(Vector2.Angle(dir, forwardVector.normalized)) < visionAngle)
        {
            if (!Physics2D.Raycast(transform.position + Vector3.up * 0.32f, dir, distToPlayer, obstacleMask))
            {
                playerVisible = true;
            }
            else
            {
                playerVisible = false;
            }
        }
        else
        {
            playerVisible = false;
        }
    }


    Vector2 VectorTo4Dir(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            if (input.x > 0)
            {
                if (input.y > 0.4f)
                {
                    return new Vector2(1, 1).normalized;
                }
                else if (input.y < -0.4f)
                {
                    return new Vector2(1, -1).normalized;
                }

                return Vector2.right;
            }
            else
            {
                if (input.y > 0.4f)
                {
                    return new Vector2(-1, 1).normalized;
                }
                else if (input.y < -0.4f)
                {
                    return new Vector2(-1, -1).normalized;
                }

                return Vector2.left;
            }
        }
        else if (input.y > 0)
        {
            return Vector2.up;
        }
        else
        {
            return Vector2.down;
        }
    }

    void Update() {
        sprite.sortingOrder = -Mathf.FloorToInt(transform.position.y * 10) + 1000;

        PatrolUpdate();
        VisionUpdate();


        if (playerDetected)
        {
            visionRange = Mathf.Lerp(visionRange, alertVisionRange, Time.deltaTime * 2);
            forwardVector = Vector2.Lerp(forwardVector, VectorTo4Dir((BeatManager.GetPlayer.transform.position - transform.position).normalized), Time.deltaTime * 3).normalized;
        }
        else
        {
            visionRange = Mathf.Lerp(visionRange, idleVisionRange, Time.deltaTime * 2);
            
            
            forwardVector = Vector2.Lerp(forwardVector, VectorTo4Dir(path.velocity2D), Time.deltaTime * 7).normalized;
        }

        Debug.DrawLine(transform.position, transform.position + (Vector3)forwardVector);
        Debug.DrawLine(transform.position, transform.position + (Vector3)path.velocity2D, Color.red);


        if (path.velocity2D.x > 0)
        {

            alertMark.flipX = true;
            sprite.flipX = true;
        }
        else
        {
            alertMark.flipX = false;

            sprite.flipX = false;
        }
    }

    private void LateUpdate()
    {
        DrawFOV();
    }


    void DrawFOV()
    {
        int stepCount = Mathf.RoundToInt(visionAngle * meshResolution);
        float stepAngleSize = visionAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++) {

            float angle = Mathf.Atan2(forwardVector.y, forwardVector.x) * Mathf.Rad2Deg - 90 + visionAngle + stepAngleSize * i * 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0) {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;

                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded)) {

                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);

                    if (edge.pointA != Vector3.zero) {
                        viewPoints.Add(edge.pointA);
                    }

                    if (edge.pointB != Vector3.zero) {
                        viewPoints.Add(edge.pointB);
                    }
                }

            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero + Vector3.up * 0.32f;
        for (int i = 0; i < vertexCount - 1; i++) {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2) {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        

        RaycastHit2D[] hit = new RaycastHit2D[10];


      //  Debug.DrawRay(transform.position, dir * visionRange);

        if (Physics2D.Raycast(transform.position + Vector3.up * 0.32f, dir * visionRange, filter2D, hit, visionRange) > 0)
        {
            return new ViewCastInfo(true, hit[0].point, hit[0].distance, globalAngle);    
        }
        else
        {
            return new ViewCastInfo(false, transform.position + Vector3.up * 0.32f + dir * visionRange, visionRange, globalAngle);
        }
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
}

public struct EdgeInfo
{
    public Vector3 pointA;
    public Vector3 pointB;

    public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
    {
        pointA = _pointA;
        pointB = _pointB;
    }
}



