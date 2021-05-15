using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float frequency;
    private float magnitude = 0.5f;

    private GameObject player;
    private GameObject ghostSpot;

    private Vector3 targetPos;
    private Vector3 startPos;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    [SerializeField] private GameObject fireFly;
    [SerializeField] private GameObject grGhost;

    [SerializeField] Image dekoElement;
    [SerializeField] Transform camPos;
    private float ghostPlayerDistance;

    private float radius = 15f;

    public bool catching = false;
    public bool catchBlocked = false;


    private void Start()
    {
        startPos = transform.position;   
        player = GameObject.FindGameObjectWithTag("Player");
        ghostSpot = GameObject.FindGameObjectWithTag("GhostSpot");

        dekoElement.color = new Color(1f, 1f, 1f);
    }

    private void Update()
    {
        //targetPos = transform.position;

        DistanceCalculation();     
    }


    private void DistanceCalculation()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        //print("Dist to " + this.name + ": " + dist);

        if (dist <= radius)
        {            
            if (Input.GetMouseButton(1) && !catchBlocked)
            {
                GetGhost();
                catching = true;
                grGhost.GetComponent<GrumblingGhost>().ghostCatched = true;

                grGhost.GetComponent<GrumblingGhost>().catchedGhost = gameObject;
                //Following();
            }
            if (Input.GetMouseButton(0) && catchBlocked)
            {
                ReleaseGhost();
                grGhost.GetComponent<GrumblingGhost>().catchedGhost = null;
                //catchBlocked = false;
            }
        }
        else
        {
            catching = false;
            //RandomMovement();
        }
    }

    private void RandomMovement()
    {
        transform.position = targetPos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    private void Following()
    {
        //print("Come here!");

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 5);
        ghostPlayerDistance = Vector3.Distance(transform.position, player.transform.position);

    }

    private void GetGhost()
    {
        //print("Hab dich!");

        float timeFactor = 2.5f;
        ghostPlayerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (ghostPlayerDistance > 2f)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, timeFactor * Time.deltaTime);
            //Debug.Log("I'm moving");
        }

        if (ghostPlayerDistance < 2f)
        {
            transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            this.transform.parent = ghostSpot.transform;
            transform.position = ghostSpot.transform.position;
            //Debug.Log("I'm moving no more");
            catchBlocked = true;
            dekoElement.color = new Color(0.4f, 0.4f, 1f);
        }

    }

    private void ReleaseGhost()
    {
        float timeFactor = 0.001f;

        //print("Und Tschüss!");
        transform.parent = null;
        ghostPlayerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (ghostPlayerDistance < 1f)
        {
            Vector3 pos1 = new Vector3(transform.position.x + camPos.transform.forward.x * 0.3f, transform.position.y, transform.position.z + camPos.transform.forward.z * 0.3f);
            Vector3 pos2 = new Vector3(transform.position.x + camPos.transform.forward.x * 0.8f, transform.position.y, transform.position.z + camPos.transform.forward.z * 0.8f);
            transform.position = Vector3.Lerp(pos1, pos2, timeFactor * Time.deltaTime);
        }

        if (ghostPlayerDistance >= 0.8f)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            Vector3 pos1 = new Vector3(transform.position.x + camPos.transform.forward.x * 0.8f, transform.position.y, transform.position.z + camPos.transform.forward.z * 0.8f);
            Vector3 pos2 = new Vector3(transform.position.x + camPos.transform.forward.x * 2.2f, transform.position.y, transform.position.z + camPos.transform.forward.z * 2.2f);
            transform.position = Vector3.Lerp(pos1, pos2, timeFactor * Time.deltaTime);
        }

        if (ghostPlayerDistance >= 2.2f)
        {
            transform.localScale = new Vector3(1.4f, 1.7f, 1.4f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            catchBlocked = false;
            dekoElement.color = new Color(1, 1, 1);
        }

    }

    public void Respawn()
    {
        grGhost.GetComponent<GrumblingGhost>().ghostCatched = false;

        catchBlocked = false;

        print(" :(( ");
        transform.parent = null;
        transform.position = startPos;
        transform.localScale = new Vector3(1.4f, 1.7f, 1.4f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        dekoElement.color = new Color(1, 1, 1);

    }
}
