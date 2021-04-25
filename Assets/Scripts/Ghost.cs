using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private float moveSpeed = 1f;
    [SerializeField] private float frequency;
    private float magnitude = 0.5f;

    public bool facingright;

    private Vector3 pos, localScale;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;

        //localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        pos += transform.right * Time.deltaTime * 0.1f;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}
