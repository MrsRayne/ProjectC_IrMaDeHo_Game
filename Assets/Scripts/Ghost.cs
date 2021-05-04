using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float frequency;
    private float magnitude = 0.5f;

    private GameObject player;

    private Vector3 targetPos;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private float radius = 15f;

    public bool catching = false;
    public bool catchBlocked = false;


    void Start()
    {
        targetPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
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

        print("Dist to " + this.name + ": " + dist);

        if (dist <= radius && !catchBlocked)
        {
            catching = true;
            
            if (Input.GetMouseButton(1))
            {
                Following();
            }            
        }
        else
        {
            catching = false;
            RandomMovement();
        }
    }

    private void RandomMovement()
    {
        transform.position = targetPos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    private void Following()
    {
        print("Come here!");

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 5);

    }
}
