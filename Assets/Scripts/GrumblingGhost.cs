using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrumblingGhost : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float maxAngle;
    [SerializeField] private float maxRadius;

    [SerializeField] private Transform[] points;

    private bool isInFOV = false;
    private int current = 0;
    private Transform target;
    public GameObject catchedGhost = null;

    public bool ghostCatched;


    private void Start()
    {
        target = new GameObject("AITarget").transform;
        target.position = points[current].position;

    }

    private void Update()
    {
        isInFOV = inFOV(transform, playerTransform, maxAngle, maxRadius);

        if(isInFOV)
        {
            if(ghostCatched)
                catchedGhost.GetComponent<Ghost>().ReleaseGhost();
            print("Detected");
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= 0)
        {
            current++;
            if (current == points.Length)
                current = 0;

            target.position = points[current].position;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 5f);
        transform.LookAt(target);
    }

    

    public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = new Collider[100];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count +1; i++)
        {
            if(overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0; // Not affected by the high 

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                    if(angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if(Physics.Raycast(ray, out hit, maxAngle))
                        {
                            if (hit.transform == target)
                                return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    
}
