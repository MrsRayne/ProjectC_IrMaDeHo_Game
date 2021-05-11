using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    //for trigger events
    [SerializeField] Collider ghostCollider;
    [SerializeField] GameObject ghostFive;
    
    //for material changes
    MeshRenderer graveRend;
    [SerializeField] GameObject grave;
    [SerializeField] Material ghostMaterial;
    [SerializeField] Material graveMaterial;
    //private Color m_oldColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == ghostFive)
        {
            print("Trigger active");
            graveRend = grave.GetComponent<MeshRenderer>();
            graveRend.material = ghostMaterial;
        }

        //print("gefunden!");
        //graveRend = grave.GetComponent<MeshRenderer>();
        //graveRend.material = ghostMaterial;

        //m_oldColor = graveRend.material.color;
        //graveRend.material.color = Color.green;

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ghostFive)
        {
            print("Trigger inactive");
            graveRend = grave.GetComponent<MeshRenderer>();
            graveRend.material = graveMaterial;
        }

        //print("Exciting!");
        //graveRend = grave.GetComponent<MeshRenderer>();
        //graveRend.material = graveMaterial;
        //graveRend = grave.GetComponent<Renderer>();
        //graveRend.material.color = m_oldColor;

    }
}
