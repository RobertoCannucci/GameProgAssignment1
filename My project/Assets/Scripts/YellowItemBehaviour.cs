using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowItemBehaviour : MonoBehaviour
{
    float timer = 0.0f;
    public float maxTime = 0.2f;
    float speed = 1.0f;
    Vector3 movement = new Vector3(0, 0.1f, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxTime)
        {
            movement = -1.0f * movement;
            timer = 0.0f;
        }
        transform.position = transform.position + movement * speed * Time.deltaTime;
        transform.Rotate(0,0,35*Time.deltaTime);
    }
}
