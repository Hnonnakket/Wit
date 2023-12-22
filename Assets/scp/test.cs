using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float speed = 5f ;
    Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputy = Input.GetAxis("Vertical");
        movement = new Vector3 (inputX,0,inputy)* Time.deltaTime;
        transform.position += movement;
    }
}
