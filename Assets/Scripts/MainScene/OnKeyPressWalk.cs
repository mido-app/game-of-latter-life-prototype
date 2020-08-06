using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnKeyPressWalk : MonoBehaviour
{

    float speed = 1;
    float vx = 0;
    float vy = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        vx = 0;
        vy = 0;
        if (Input.GetKey("right")) {
            vx = speed;
        } else if (Input.GetKey("left")) {
            vx = -1 * speed;            
        } else if (Input.GetKey("up")) {
            vy = speed;
        } else if (Input.GetKey("down")) {
            vy = -1 * speed;
        }
    }

    void FixedUpdate() {
        this.transform.Translate(vx / 50, vy / 50, 0);
    }
}
