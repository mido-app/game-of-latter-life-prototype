using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnKeyPressWalk : MonoBehaviour
{

    float speed = 1;
    float fixedUpdateTimesPerSeconds = 50;

    float vx = 0;
    float vy = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Do nothing
    }

    // Update is called once per frame
    void Update()
    {
        vx = 0;
        vy = 0;

        if (Input.GetKey("right")) {
            vx = speed;
            this.GetComponent<Animator>().Play("WalkRight");

        } else if (Input.GetKey("left")) {
            vx = -1 * speed;            
            this.GetComponent<Animator>().Play("WalkLeft");

        } else if (Input.GetKey("up")) {
            vy = speed;
            this.GetComponent<Animator>().Play("WalkUp");

        } else if (Input.GetKey("down")) {
            vy = -1 * speed;
            this.GetComponent<Animator>().Play("WalkDown");
        }
    }

    void FixedUpdate() {
        this.transform.Translate(vx / fixedUpdateTimesPerSeconds, vy / fixedUpdateTimesPerSeconds, 0);
    }
}
