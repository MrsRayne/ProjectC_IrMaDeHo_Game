using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                //Following();
                GetGhost();
                grGhost.GetComponent<GrumblingGhost>().ghostCatched = true;

                grGhost.GetComponent<GrumblingGhost>().catchedGhost = gameObject;
                
            }

            if (Input.GetMouseButton(0) && catchBlocked)
            {
                ReleaseGhost();
                grGhost.GetComponent<GrumblingGhost>().catchedGhost = null;
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

        /*if (ghostPlayerDistance <= 2)
        {
            GetGhost();
        }*/


    }

    private void GetGhost()
    {
        //print("Hab dich!");

        float timeFactor = 2.5f;
        ghostPlayerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (ghostPlayerDistance > 2f)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, timeFactor * Time.deltaTime);
            Debug.Log("I'm moving");
        }

        if(ghostPlayerDistance < 2f)
        {
            transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            this.transform.parent = ghostSpot.transform;
            transform.position = ghostSpot.transform.position;
            Debug.Log("I'm moving no more");
            catchBlocked = true;
        }



    }

    private void ReleaseGhost()
    {
        //print("Und Tschüss!");
        /*transform.parent = null;
        transform.localScale = new Vector3(1.4f, 1.7f, 1.4f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        //transform.position = transform.position + camPos.transform.forward * 2.5f;
        //transform.position = new Vector3 (transform.position.x + camPos.transform.forward.x * 2.5f, transform.position.y, transform.position.z + camPos.transform.forward.z * 2.5f);
        Vector3 pos1 = new Vector3 (player.transform.position.x + camPos.transform.forward.x * 1.1f, player.transform.position.y, player.transform.position.z + camPos.transform.forward.z * 1.1f);
        Vector3 pos2 = new Vector3 (transform.position.x + camPos.transform.forward.x * 2.5f, transform.position.y, transform.position.z + camPos.transform.forward.z * 2.5f);
        transform.position = Vector3.Lerp(pos1, pos2, Time.deltaTime);

        catchBlocked = false;*/

        //////////////////////////////////

        float timeFactor = 0.001f;
        //float moveSpeed = 0.001f;
        //float speedReduction = 2000;

        transform.parent = null;
        ghostPlayerDistance = Vector3.Distance(transform.position, player.transform.position);

        //moveSpeed = Mathf.Max(moveSpeed - speedReduction, 0f);

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
    }
}
