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

    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    [SerializeField] GameObject fireFly;

    private float radius = 15f;

    public bool catching = false;
    public bool catchBlocked = false;


    void Start()
    {
        targetPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        ghostSpot = GameObject.FindGameObjectWithTag("GhostSpot");
    }

    // Update is called once per frame
    void Update()
    {
        //pos += transform.right * Time.deltaTime * 0.1f;
        
        DistanceCalculation();     
    }


    private void DistanceCalculation()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        //print("Dist to " + this.name + ": " + dist);

        if (dist <= radius && !catchBlocked && !catching)
        {
            //catching = true;
            
            if (Input.GetMouseButtonDown(1))
            {
                //Following();
                GetGhost();
                //catchBlocked = true;
                catching = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                ReleaseGhost();
                //catchBlocked = false;
            } 

        }

        /*if (dist <= radius && catchBlocked && !catching)
        {

            if (Input.GetMouseButtonDown(0))
            {
                ReleaseGhost();
                catchBlocked = false;
                catching = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                ReleaseGhost();
                catchBlocked = false;
                catching = false;
            }

        }*/


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

    /*private void Following()
    {
        print("Come here!");

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 5);

    }*/

    private void GetGhost()
    {
        //print("Hab dich!");
        this.transform.parent = ghostSpot.transform;
        transform.position = ghostSpot.transform.position;
        transform.localScale = new Vector3 (0.03f, 0.04f, 0.03f);
        //GetComponent<MeshRenderer>().enabled = false;
        //Instantiate(fireFly, ghostSpot.transform.position, ghostSpot.transform.rotation);
        //fireFly.transform.parent = ghostSpot.transform;
        //fireFly.transform.position = ghostSpot.transform.position;

    }

    private void ReleaseGhost()
    {
        //print("Und Tschüss!");
        transform.parent = null;
        transform.localScale = new Vector3(1.4f, 1.7f, 1.4f);
        //GetComponent<MeshRenderer>().enabled = true;
    }





}
